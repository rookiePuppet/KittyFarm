using System;
using System.Threading;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace KittyFarm
{
    public class GridCellIndicator : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private CancellationTokenSource cts;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            spriteRenderer.enabled = false;
        }

        public async void ShowAt(Vector2 position)
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