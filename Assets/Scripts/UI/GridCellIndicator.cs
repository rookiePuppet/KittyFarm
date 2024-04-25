using System;
using System.Threading;
using KittyFarm.Map;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace KittyFarm.UI
{
    public class GridCellIndicator : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private CancellationTokenSource cts;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
        }

        private void OnEnable()
        {
            GridClickable.TileClicked += OnTileClicked;
        }

        private void OnDisable()
        {
            GridClickable.TileClicked -= OnTileClicked;
        }

        private void OnTileClicked(Vector3Int cellPosition, Vector3 cellCenter)
        {
            ShowAt(cellCenter);
        }
        
        private async void ShowAt(Vector2 position)
        {
            cts?.Cancel();
            cts = new CancellationTokenSource();

            transform.position = position;
            spriteRenderer.enabled = true;

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2f), cts.Token);
                spriteRenderer.enabled = false;
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}