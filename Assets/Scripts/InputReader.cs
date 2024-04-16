using System;
using Framework;
using UnityEngine.InputSystem;

namespace KittyFarm
{
    public class InputReader: Singleton<InputReader>
    {
        public static event Action<InputAction.CallbackContext> Move;
        
        private static readonly PlayerInputActions inputActions = new ();
        
        static InputReader()
        {
            inputActions.Gameplay.Move.started += OnMove;
            inputActions.Gameplay.Move.performed += OnMove;
            inputActions.Gameplay.Move.canceled += OnMove;
        }

        public static void DisableInput() => inputActions.Gameplay.Disable();
        public static void EnableInput() => inputActions.Gameplay.Enable();

        private static void OnMove(InputAction.CallbackContext context)
        {
            Move?.Invoke(context);
        }
    }
}