using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;

    public UnityEvent<Direction> Move;

    public BoolVariable turnAvailable;

    Vector2 touchStartPos = Vector2.zero;
    float touchStartTime = 0;

    Vector2 touchEndPos = Vector2.zero;
    float touchEndTime = 0;

    const float swipeThreshold = 0.17f;
    const float timeLimit = 0.3f;

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
    }

    private void Start()
    {
        playerControls.TileControls.MoveLeft.performed += _ => CheckThenMove(Move, Direction.Left);
        playerControls.TileControls.MoveRight.performed += _ => CheckThenMove(Move, Direction.Right);
        playerControls.TileControls.Rush.performed += _ => CheckThenMove(Move, Direction.Down);
        playerControls.TileControls.TouchContact.started += _ => StartTouchPrimary(_);
        playerControls.TileControls.TouchContact.canceled += _ => EndTouchPrimary(_);
    }

    private void CheckThenMove(UnityEvent<Direction> toInvoke, Direction direction)
    {
        if (turnAvailable.Value)
        {
            turnAvailable.SetValue(false);
            toInvoke.Invoke(direction);
        }
    }

    public void NextTurn()
    {
        turnAvailable.SetValue(true);
    }

    void StartTouchPrimary(InputAction.CallbackContext context)
    {
        Debug.Log("LOSTILES: TOUCH HAS STARTED");
        touchStartPos = playerControls.TileControls.TouchPosition.ReadValue<Vector2>();
        touchStartTime = (float) context.startTime;
    }

    void EndTouchPrimary(InputAction.CallbackContext context)
    {
        Debug.Log("LOSTILES: TOUCH HAS ENDED");
        touchEndPos = playerControls.TileControls.TouchPosition.ReadValue<Vector2>();
        touchEndTime = (float) context.time;

        Vector2 touchPosDelta = touchEndPos - touchStartPos;
        float touchTimeDelta = touchEndTime - touchStartTime;

        if (touchTimeDelta < timeLimit)
        {
            if (VerticalMove() > swipeThreshold && VerticalMove() > HorizontalMove())
            {
                if (touchPosDelta.y < 0)
                {
                    CheckThenMove(Move, Direction.Down);
                }
            }

            else if (HorizontalMove() > swipeThreshold && HorizontalMove() > VerticalMove())
            {
                if (touchPosDelta.x > 0)
                {
                    CheckThenMove(Move, Direction.Right);
                }

                else if (touchPosDelta.x < 0)
                {
                    CheckThenMove(Move, Direction.Left);
                }
            }
        }

        touchStartPos = Vector2.zero;
        touchStartTime = 0;

        touchEndPos = Vector2.zero;
        touchEndTime = 0;
    }

    float VerticalMove()
    {
        return touchEndPos.y - touchStartPos.y;
    }

    float HorizontalMove()
    {
        return touchEndPos.x - touchStartPos.x;
    }
}