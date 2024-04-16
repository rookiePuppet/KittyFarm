using System;
using KittyFarm.Data;
using KittyFarm.InventorySystem;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KittyFarm
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float movementVelocity = 5f;

        private PlayerInventorySO Inventory => GameDataCenter.Instance.PlayerInventory;

        public PlayerAnimation Animation { get; private set; }

        private new Rigidbody2D rigidbody;

        private Vector2 MovementInput { get; set; }

        public event Action<Vector2> Moving;
        public event Action MoveStopped;

        private void Awake()
        {
            Animation = GetComponentInChildren<PlayerAnimation>();
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            InputReader.Move += OnMove;
        }

        private void OnDisable()
        {
            InputReader.Move -= OnMove;
        }

        private void Start()
        {
            InputReader.EnableInput();
            Application.targetFrameRate = 60;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                MovementInput = context.ReadValue<Vector2>();
                rigidbody.velocity = MovementInput * movementVelocity;

                Moving?.Invoke(MovementInput);
            }
            else if (context.canceled)
            {
                rigidbody.velocity = Vector2.zero;

                MoveStopped?.Invoke();
            }
        }
    }
}