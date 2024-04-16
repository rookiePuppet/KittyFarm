using System;
using System.Threading;
using System.Threading.Tasks;
using KittyFarm.Data;
using KittyFarm.Service;
using UnityEngine;

namespace KittyFarm.NavigationSystem
{
    // [RequireComponent(typeof(Rigidbody2D))]
    // public class NavAgent : MonoBehaviour
    // {
    //     [SerializeField] private float movementVelocity = 5f;
    //     [SerializeField] private Vector2 offset = new(0.5f, 0.5f);
    //
    //     public bool IsMoving { get; private set; }
    //     public Vector2 Direction => direction;
    //
    //     public event Action MoveStarted;
    //     public event Action MoveStopped;
    //     public event Action<Vector2> Moving;
    //
    //     private AStar aStar;
    //     private new Rigidbody2D rigidbody;
    //     private Vector2 direction;
    //     private CancellationTokenSource tokenSource;
    //
    //     private void Awake()
    //     {
    //         rigidbody = GetComponent<Rigidbody2D>();
    //     }
    //
    //     private void Start()
    //     {
    //         var mapData = GameDataCenter.Instance.CurrentMapData;
    //         aStar = new AStar(mapData.GridOriginCoordinate, mapData.GridSize);
    //     }
    //
    //     public async Task GoToDestination(Vector3 targetPosition)
    //     {
    //         tokenSource?.Cancel();
    //         tokenSource = new CancellationTokenSource();
    //
    //         try
    //         {
    //             var mapService = ServiceCenter.Get<ITilemapService>();
    //             var currentGridPosition = (Vector2Int)mapService.WorldToCell(transform.position);
    //             var destination = (Vector2Int)mapService.WorldToCell(targetPosition);
    //             var path = await aStar.FindPathAsync(currentGridPosition, destination, tokenSource.Token);
    //
    //             MoveStarted?.Invoke();
    //
    //             foreach (var node in path)
    //             {
    //                 await GoToNextWayPoint(node, tokenSource.Token);
    //             }
    //
    //             IsMoving = false;
    //             rigidbody.velocity = Vector2.zero;
    //             MoveStopped?.Invoke();
    //         }
    //         catch (OperationCanceledException)
    //         {
    //         }
    //     }
    //
    //     private async Task GoToNextWayPoint(Node node, CancellationToken? token = null)
    //     {
    //         token ??= CancellationToken.None;
    //
    //         var targetPosition = new Vector2(node.X + offset.x, node.Y + offset.y);
    //         direction = targetPosition - (Vector2)transform.position;
    //         direction.Normalize();
    //
    //         IsMoving = true;
    //         Moving?.Invoke(direction);
    //
    //         while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
    //         {
    //             if (token != CancellationToken.None && token.Value.IsCancellationRequested)
    //                 token.Value.ThrowIfCancellationRequested();
    //
    //             rigidbody.velocity = direction * movementVelocity;
    //
    //             await Task.Yield();
    //         }
    //     }
    // }
}