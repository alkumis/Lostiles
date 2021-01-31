using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class MovementManager : MonoBehaviour
{
    public UnityEvent turnDone;

    public GameObjectRuntimeSet floatingTileRuntimeSet;

    public float moveSideDuration;
    public float moveDownDuration;
    public float moveDownAcceleration;

    public BoolVariable turnAvailable;

    public Ease sideEase;
    public Ease downEase;
    public Ease rushEase;

    public void MoveInput(Direction direction)
    {
        switch (direction)
        {
            case (Direction.Left):
                StartCoroutine(MoveAll(-1));
                break;
            case (Direction.Right):
                StartCoroutine(MoveAll(1));
                break;
            case (Direction.Down):
                StartCoroutine(RushAll());
                break;
        }
    }

    IEnumerator MoveAll(int direction)
    {
        foreach (var floatingTile in floatingTileRuntimeSet.Items)
        {
            floatingTile.GetComponent<TileMove>().MoveSide(direction, moveSideDuration, sideEase);
        }

        yield return new WaitForSeconds(moveSideDuration);

        var floatingTilesToDelete = CheckAndMoveDownAll(moveDownDuration, rushEase);

        yield return new WaitForSeconds(moveDownDuration);

        if (floatingTilesToDelete != null)
        {
            if (floatingTilesToDelete.Count > 0)
            {
                ClearFloatingTiles(floatingTilesToDelete);
            }
        }

        if(!turnAvailable.Value)
        {
            turnDone.Invoke();
        }
    }

    IEnumerator RushAll()
    {
        CheckAndMoveDownAll(moveDownDuration, downEase);

        float tempMoveDownDuration = moveDownDuration;

        while (turnAvailable.Value == false)
        {
            var floatingTilesToDelete = CheckAndMoveDownAll(tempMoveDownDuration, rushEase);

            yield return new WaitForSeconds(tempMoveDownDuration);

            if(floatingTilesToDelete != null)
            {
                if(floatingTilesToDelete.Count > 0)
                {
                    ClearFloatingTiles(floatingTilesToDelete);
                }
            }

            tempMoveDownDuration *= moveDownAcceleration;
        }

        if (!turnAvailable.Value)
        {
            turnDone.Invoke();
        }
    }

    private List<TileMove> CheckAndMoveDownAll(float duration, Ease ease)
    {
        List<TileMove> floatingTilesToDelete = new List<TileMove>();

        foreach (var floatingTile in floatingTileRuntimeSet.Items)
        {
            var moveCheck = floatingTile.GetComponent<TileMove>().MoveCheck();

            if (moveCheck != null)
            {
                floatingTilesToDelete.Add(moveCheck);
            }
        }

        if (floatingTilesToDelete.Count > 0)
        {
            return floatingTilesToDelete;
        }

        foreach (var floatingTile in floatingTileRuntimeSet.Items)
        {
            var moveCheck = floatingTile.GetComponent<TileMove>().MoveDown(duration, ease);

            if (moveCheck != null)
            {
                floatingTilesToDelete.Add(moveCheck);
            }
        }

        if (floatingTilesToDelete.Count > 0)
        {
            return floatingTilesToDelete;
        }

        return null;
    }

    private void ClearFloatingTiles(List<TileMove> floatingTilesToDelete)
    {
        floatingTilesToDelete.Sort(SortByHeight);

        foreach (var floatingTileToDelete in floatingTilesToDelete)
        {
            floatingTileToDelete.AddToGridTiles();
        }

        turnDone.Invoke();
    }

    private int SortByHeight(TileMove tile1, TileMove tile2)
    {
        int tile1Height = (int) tile1.gameObject.transform.position.y;
        int tile2Height = (int)tile2.gameObject.transform.position.y;

        return tile1Height.CompareTo(tile2Height);
    }
}
