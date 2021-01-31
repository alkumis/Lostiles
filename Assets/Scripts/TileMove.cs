using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class TileMove : MonoBehaviour
{
    public Colour tileColour;

    public float moveSideDuration;
    public float moveDownDuration;
    public float moveDownAcceleration;
    public float fadeDuration;

    public Ease sideEase;
    public Ease downEase;

    public UnityEvent turnDone;
    public UnityEvent<int, int, GridTile> GridAdd;

    public bool grounded = false;

    public BoolVariable turnAvailable;

    public IntVariable activeFloatingTiles;
    public IntVariable tilesToMove;

    public GridTiles gridTiles;

    private void Awake()
    {
        activeFloatingTiles.ApplyChange(1);

        if (grounded)
        {
            AddToGridTiles();
        }
    }

    private void AddToGridTiles()
    {
        grounded = true;
        activeFloatingTiles.ApplyChange(-1);
        int column = (int)transform.position.x;
        int row = (int)transform.position.y;
        var thisTile = new GridTile(tileColour, this.gameObject);

        GridAdd.Invoke(column, row, thisTile);
    }

    public void MoveInput(Direction direction)
    {
        if (grounded)
        {
            return;
        }

        switch (direction)
        {
            case (Direction.Left):
                StartCoroutine(MoveSide(-1));
                break;
            case (Direction.Right):
                StartCoroutine(MoveSide(1));
                break;
            case (Direction.Down):
                StartCoroutine(Rush());
                break;
        }
    }

    IEnumerator MoveSide(int direction)
    {
        int targetColumn = (int)transform.position.x + direction;
        int row = (int)transform.position.y;

        if (!gridTiles.tileList.ContainsKey(targetColumn) || gridTiles.tileList[targetColumn].ContainsKey(row))
        {
            targetColumn -= direction;
        }
        else
        {
            Vector3 targetX = new Vector3(targetColumn, row, transform.position.z);
            Move(targetX, moveSideDuration, sideEase);
        }

        yield return new WaitForSeconds(moveSideDuration);


        int targetRow = row - 1;

        if (gridTiles.tileList[targetColumn].ContainsKey(targetRow) && !grounded)
        {
            AddToGridTiles();
        }
        else
        {
            Vector3 targetY = new Vector3(targetColumn, targetRow, transform.position.z);
            Move(targetY, moveDownDuration, downEase);
        }

        yield return new WaitForSeconds(moveDownDuration);

        if (gridTiles.tileList[targetColumn].ContainsKey(targetRow - 1) && !grounded)
        {
            AddToGridTiles();
        }

        if (turnAvailable.Value == false)
        {
            tilesToMove.ApplyChange(-1);

            if (tilesToMove.Value == 0)
            {
                turnDone.Invoke();
            }
        }
    }

    IEnumerator Rush()
    {
        int column = (int)transform.position.x;
        int targetRow = (int)transform.position.y - 1;
        Vector3 targetY = new Vector3(column, targetRow, transform.position.z);
        Move(targetY, moveDownDuration, Ease.Linear);
        yield return new WaitForSeconds(moveDownDuration);

        float moveDownAccelerated = moveDownDuration;

        while (turnAvailable.Value == false)
        {
            targetRow -= 1;

            if (gridTiles.tileList[column].ContainsKey(targetRow) && !grounded)
            {
                AddToGridTiles();
                if (turnAvailable.Value == false)
                {
                    turnDone.Invoke();
                }
            }
            else
            {
                targetY.y = targetRow;
                Move(targetY, moveDownAccelerated, Ease.Linear);
                yield return new WaitForSeconds(moveDownAccelerated);
            }

            moveDownAccelerated *= moveDownAcceleration;
        }

        if (gridTiles.tileList[column].ContainsKey(targetRow - 1) && !grounded)
        {
            AddToGridTiles();
        }

        if (turnAvailable.Value == false)
        {
            tilesToMove.ApplyChange(-1);

            if (tilesToMove.Value == 0)
            {
                turnDone.Invoke();
            }
        }
    }

    private void Move(Vector3 target, float duration, Ease easing)
    {
        transform.DOMove(target, duration, false)
            .SetEase(easing);
    }

    public IEnumerator Remove()
    {
        this.GetComponent<SpriteRenderer>().DOColor(Color.clear, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        Destroy(this.gameObject);
    }
}
