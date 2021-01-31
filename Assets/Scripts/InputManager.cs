using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;

    public UnityEvent<Direction> Move;

    public BoolVariable turnAvailable;

    public IntVariable activeFloatingTiles;
    public IntVariable tilesToMove;

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
        turnAvailable.SetValue(true);
        tilesToMove.SetValue(0);
    }

    private void Start()
    {
        playerControls.TileControls.MoveLeft.performed += _ => CheckThenMove(Move, Direction.Left);
        playerControls.TileControls.MoveRight.performed += _ => CheckThenMove(Move, Direction.Right);
        playerControls.TileControls.Rush.performed += _ => CheckThenMove(Move, Direction.Down);
    }

    private void CheckThenMove(UnityEvent<Direction> toInvoke, Direction direction)
    {
        if (turnAvailable.Value)
        {
            turnAvailable.SetValue(false);
            tilesToMove.SetValue(activeFloatingTiles);
            toInvoke.Invoke(direction);
        }
    }

    public void NextTurn()
    {
        turnAvailable.SetValue(true);
    }
}