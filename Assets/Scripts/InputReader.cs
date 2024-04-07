using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KittyFarm
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "InputReader")]
    public class InputReader : ScriptableObject
    {
        public event Action<InputAction.CallbackContext> Move;
        
        private PlayerInputActions inputActions;

        private void OnEnable()
        {
            inputActions = new PlayerInputActions();
            inputActions.Gameplay.Enable();

            inputActions.Gameplay.Move.started += OnMove;
            inputActions.Gameplay.Move.performed += OnMove;
            inputActions.Gameplay.Move.canceled += OnMove;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Move?.Invoke(context);
        }
    }
}