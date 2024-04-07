using System.Collections.Generic;
using UnityEngine;

namespace KittyFarm.NavigationSystem
{
    public class Grid
    {
        private Vector2Int Size { get; set; }
        private Node[,] Nodes { get; set; }
        private Vector2Int Origin { get; set; }

        private readonly List<Vector2Int> Directions = new()
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(1, 1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, -1),
            new Vector2Int(-1, 1)
        };

        public Grid(Vector2Int origin, Vector2Int size)
        {
            Origin = origin;
            Size = size;

            Nodes = new Node[size.y, size.x];

            for (var y = 0; y < size.y; y++)
            {
                for (var x = 0; x < size.x; x++)
                {
                    var node = new Node(x + origin.x, y + origin.y);
                    Nodes[y, x] = node;
                }
            }
        }

        public bool IsInBounds(Vector2Int gridCoordinate) =>
            gridCoordinate.x - Origin.x >= 0 && gridCoordinate.x - Origin.x < Size.x &&
            gridCoordinate.y - Origin.y >= 0 && gridCoordinate.y - Origin.y < Size.y;

        public IEnumerable<Node> GetNeighbors(Node node)
        {
            foreach (var direction in Directions)
            {
                var neighbor = new Vector2Int(node.X + direction.x, node.Y + direction.y);
                if (IsInBounds(neighbor))
                {
                    yield return GetNodeFrom(neighbor);
                }
            }
        }

        public Node GetNodeFrom(Vector2Int gridCoordinate) =>
            Nodes[gridCoordinate.y - Origin.y, gridCoordinate.x - Origin.x];
    }
}