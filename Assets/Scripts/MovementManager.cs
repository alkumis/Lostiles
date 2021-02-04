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

    public GridTiles gridTiles;

    public void MoveInput(Direction direction)
    {
        //Debug.Log("Number of floating tiles: " + floatingTileRuntimeSet.Items.Count);

        //Debug.Log("Column count: " + gridTiles.tileList.Keys.Count);

        //foreach (var column in gridTiles.tileList.Keys)
        //{
        //    Debug.Log("Column: " + column);

        //    Debug.Log("Row count: " + gridTiles.tileList[column].Keys.Count);

        //    foreach(var row in gridTiles.tileList[column].Keys)
        //    {
        //        Debug.Log("Colour: " + gridTiles.tileList[column][row].Colour);
        //    }
        //}

        switch (direction)
        {
            case (Direction.Left):
                StartCoroutine(MoveAll(direction));
                break;
            case (Direction.Right):
                StartCoroutine(MoveAll(direction));
                break;
            case (Direction.Down):
                StartCoroutine(RushAll());
                break;
        }
    }

    IEnumerator MoveAll(Direction direction)
    {
        MoveSideAll(direction);

        yield return new WaitForSeconds(moveSideDuration);

        DownCheckAll();

        if (!turnAvailable.Value)
        {
            MoveDownAll(moveDownDuration, downEase);

            yield return new WaitForSeconds(moveDownDuration);

            DownCheckAll();
        }

        turnDone.Invoke();
    }

    IEnumerator RushAll()
    {
        MoveDownAll(moveDownDuration, downEase);

        yield return new WaitForSeconds(moveDownDuration);

        DownCheckAll();

        float tempMoveDownDuration = moveDownDuration;

        while (turnAvailable.Value == false)
        {
            MoveDownAll(tempMoveDownDuration, rushEase);

            yield return new WaitForSeconds(tempMoveDownDuration);

            DownCheckAll();

            tempMoveDownDuration *= moveDownAcceleration;
        }
    }

    private void MoveSideAll(Direction direction)
    {
        var tempGridTiles = ScriptableObject.CreateInstance<GridTiles>();
        tempGridTiles.Copy(gridTiles);

        floatingTileRuntimeSet.Sort(direction);

        foreach (var floatingTile in floatingTileRuntimeSet.Items)
        {
            tempGridTiles = floatingTile.GetComponent<TileMove>().MoveSide(direction, moveSideDuration, sideEase, tempGridTiles);
        }
    }

    private void MoveDownAll(float duration, Ease ease)
    {
        floatingTileRuntimeSet.Sort(Direction.Down);

        for (int i = floatingTileRuntimeSet.Items.Count - 1; i >= 0; i--)
        {
            floatingTileRuntimeSet.Items[i].GetComponent<TileMove>().MoveDown(duration, ease);
        }
    }

    private void DownCheckAll()
    {
        bool anyBlocked = false;

        floatingTileRuntimeSet.Sort(Direction.Down);

        for (int i = floatingTileRuntimeSet.Items.Count - 1; i >= 0; i--)
        {
            bool blockedCheck = floatingTileRuntimeSet.Items[i].GetComponent<TileMove>().DownCheck();

            if (blockedCheck)
            {
                anyBlocked = true;
            }
        }

        if (anyBlocked)
        {
            turnDone.Invoke();
        }
    }
}
