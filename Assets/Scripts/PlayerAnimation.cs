using System;
using System.Threading.Tasks;
using UnityEngine;

namespace KittyFarm
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        private PlayerController player;
        private Animator animator;

        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int XInput = Animator.StringToHash("XInput");
        private static readonly int YInput = Animator.StringToHash("YInput");
        private static readonly int UseTool = Animator.StringToHash("UseTool");
        private static readonly int ToolType = Animator.StringToHash("ToolType");

        private void Awake()
        {
            player = GetComponentInParent<PlayerController>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            player.Moving += OnMoving;
            player.MoveStopped += OnMoveStopped;
        }

        private void OnDestroy()
        {
            player.Moving -= OnMoving;
            player.MoveStopped -= OnMoveStopped;
        }

        private void OnMoveStopped()
        {
            animator.SetBool(Move, false);
        }

        private void OnMoving(Vector2 direction)
        {
            animator.SetBool(Move, true);
            animator.SetFloat(XInput, direction.x);
            animator.SetFloat(YInput, direction.y);
        }

        public async void PlayUseTool(Vector2 direction, ToolType toolType, Action afterAnimation = null)
        {
            animator.SetBool(UseTool, true);
            animator.SetInteger(ToolType, (int)toolType);

            animator.SetFloat(XInput, direction.x);
            animator.SetFloat(YInput, direction.y);

            await Task.Delay(80);

            animator.SetBool(UseTool, false);
            afterAnimation?.Invoke();
        }
    }
}