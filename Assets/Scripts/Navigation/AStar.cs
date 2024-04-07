using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace KittyFarm.NavigationSystem
{
    public class AStar
    {
        private readonly List<Node> frontier = new();
        private readonly Dictionary<Node, Node> cameFrom = new();
        private readonly Dictionary<Node, int> costSoFar = new();

        private Node startNode;
        private Node endNode;

        private Grid grid;

        private IEnumerable<Node> BacktrackedPath
        {
            get
            {
                var path = new Stack<Node>();

                var currentNode = endNode;
                while (currentNode != startNode)
                {
                    path.Push(currentNode);
                    currentNode = cameFrom[currentNode];
                }

                return path;
            }
        }

        public AStar(Vector2Int gridOriginCoordinate, Vector2Int gridSize)
        {
            grid = new Grid(gridOriginCoordinate, gridSize);
        }

        public void Reset(Vector2Int gridOriginCoordinate, Vector2Int gridSize)
        {
            grid = new Grid(gridOriginCoordinate, gridSize);
        }

        public IEnumerable<Node> FindPath(Vector2Int startPos, Vector2Int endPos)
        {
            if (!grid.IsInBounds(startPos) || !grid.IsInBounds(endPos))
            {
                return new List<Node>();
            }

            startNode = grid.GetNodeFrom(startPos);
            endNode = grid.GetNodeFrom(endPos);

            PrepareForSearch();

            while (frontier.Count > 0)
            {
                frontier.Sort((node1, node2) => node1.Priority < node2.Priority ? -1 : 1);

                var current = frontier[0];
                frontier.RemoveAt(0);

                if (current == endNode) break;

                foreach (var next in grid.GetNeighbors(current))
                {
                    var gCost = costSoFar[current] + 1;
                    if (costSoFar.ContainsKey(next) && gCost >= costSoFar[next]) continue;

                    next.Priority = gCost + Heuristic(next, endNode);
                    frontier.Add(next);

                    cameFrom[next] = current;
                    costSoFar[next] = gCost;
                }
            }

            return BacktrackedPath;
        }

        public async Task<IEnumerable<Node>> FindPathAsync(Vector2Int startPos, Vector2Int endPos,
            CancellationToken? token = null)
        {
            token ??= CancellationToken.None;

            if (!grid.IsInBounds(startPos) || !grid.IsInBounds(endPos))
            {
                return new List<Node>();
            }

            startNode = grid.GetNodeFrom(startPos);
            endNode = grid.GetNodeFrom(endPos);

            PrepareForSearch();
            
            while (frontier.Count > 0)
            {
                CheckTokenCanceled();
                
                frontier.Sort((node1, node2) => node1.Priority < node2.Priority ? -1 : 1);

                var current = frontier[0];
                frontier.RemoveAt(0);

                if (current == endNode) break;

                foreach (var next in grid.GetNeighbors(current))
                {
                    CheckTokenCanceled();
                    
                    var gCost = costSoFar[current] + 1;
                    if (costSoFar.ContainsKey(next) && gCost >= costSoFar[next]) continue;

                    next.Priority = gCost + Heuristic(next, endNode);
                    frontier.Add(next);

                    cameFrom[next] = current;
                    costSoFar[next] = gCost;
                }
                
                await Task.Yield();
            }

            return BacktrackedPath;

            void CheckTokenCanceled()
            {
                if (token.Value != CancellationToken.None && token.Value.IsCancellationRequested)
                {
                    token.Value.ThrowIfCancellationRequested();
                }
            }
        }

        private float Heuristic(Node node1, Node node2) =>
            Mathf.Abs(node1.X - node2.X) + Mathf.Abs(node1.Y - node2.Y);

        private void PrepareForSearch()
        {
            frontier.Clear();
            cameFrom.Clear();
            costSoFar.Clear();

            frontier.Add(startNode);
            cameFrom[startNode] = null;
            costSoFar[startNode] = 0;
        }
    }
}