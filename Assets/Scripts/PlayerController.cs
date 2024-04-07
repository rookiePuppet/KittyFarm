using System;
using KittyFarm;
using KittyFarm.InventorySystem;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private PlayerInventorySO inventory;

    [SerializeField] private float movementVelocity = 5f;

    public PlayerInventorySO Inventory => inventory;

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
        inputReader.Move += OnMove;
    }

    private void OnDisable()
    {
        inputReader.Move -= OnMove;
    }
    
    private void Start()
    {
        SceneLoader.Instance.LoadMapScene("Plain");

        UIManager.Instance.ShowUI<GameView>();
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