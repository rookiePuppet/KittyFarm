using System;
using KittyFarm.Data;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KittyFarm
{
    public class PlayerController : MonoBehaviour
    {
        public event Action<Vector2> Moving;
        public event Action MoveStopped;

        private PlayerDataSO Data => GameDataCenter.Instance.PlayerData;
        public PlayerAnimation Animation { get; private set; }
        public SpeakBubble SpeakBubble { get; private set; }
        private Vector2 MovementInput { get; set; }
        
        private Rigidbody2D rbody;

        private void Awake()
        {
            Animation = GetComponentInChildren<PlayerAnimation>();
            rbody = GetComponent<Rigidbody2D>();
            SpeakBubble = GetComponent<SpeakBubble>();
        }

        private void OnEnable()
        {
            InputReader.Move += OnMove;
            GameDataCenter.BeforeSaveData += DoBeforeSaveData;
        }

        private void OnDisable()
        {
            InputReader.Move -= OnMove;
            GameDataCenter.BeforeSaveData += DoBeforeSaveData;
        }
        
        private void Start()
        {
            InputReader.EnableInput();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                MovementInput = context.ReadValue<Vector2>();
                rbody.velocity = MovementInput * Data.MovementVelocity;

                Moving?.Invoke(MovementInput);
            }
            else if (context.canceled)
            {
                rbody.velocity = Vector2.zero;

                MoveStopped?.Invoke();
            }
        }
        
        private void DoBeforeSaveData()
        {
            Data.LastPosition = transform.position;
        }
    }
}