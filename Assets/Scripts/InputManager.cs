using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;

    public UnityEvent MoveLeft;
    public UnityEvent MoveRight;
    public UnityEvent Rush;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.TileControls.MoveLeft.performed += _ => MoveLeft.Invoke();
        playerControls.TileControls.MoveRight.performed += _ => MoveRight.Invoke();
        playerControls.TileControls.Rush.performed += _ => Rush.Invoke();
    }
}