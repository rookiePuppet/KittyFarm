using System;
using Framework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KittyFarm
{
    public class InputReader: Singleton<InputReader>
    {
        public static event Action<InputAction.CallbackContext> Move;
        public static event Action<InputAction.CallbackContext> Click;
        
        public static Vector2 Point { get; private set; }
        
        private static readonly PlayerInputActions inputActions = new ();

        
        static InputReader()
        {
            inputActions.Gameplay.Move.started += OnMove;
            inputActions.Gameplay.Move.performed += OnMove;
            inputActions.Gameplay.Move.canceled += OnMove;
            
            inputActions.Gameplay.Click.started += OnClick;
            inputActions.Gameplay.Click.performed += OnClick;
            inputActions.Gameplay.Click.canceled += OnClick;
            
            inputActions.Gameplay.Point.performed += OnPoint;
        }

        public static void DisableInput() => inputActions.Gameplay.Disable();
        public static void EnableInput() => inputActions.Gameplay.Enable();

        private static void OnMove(InputAction.CallbackContext context)
        {
            Move?.Invoke(context);
        }

        private static void OnClick(InputAction.CallbackContext context)
        {
            Click?.Invoke(context);
        }

        private static void OnPoint(InputAction.CallbackContext context)
        {
            Point = context.ReadValue<Vector2>();
        }
    }
}