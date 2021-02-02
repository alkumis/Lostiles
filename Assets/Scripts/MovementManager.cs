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
        var tempGridTiles = new GridTiles();

        foreach (var column in gridTiles.tileList.Keys)
        {
            foreach (var row in gridTiles.tileList[column].Keys)
            {
                if (!tempGridTiles.tileList.ContainsKey(column))
                {
                    tempGridTiles.tileList.Add(column, new Dictionary<int, GridTile>() { { row, gridTiles.tileList[column][row] } });
                }
                else
                {
                    tempGridTiles.tileList[column].Add(row, gridTiles.tileList[column][row]);
                }
            }
        }

        var floatingTileList = floatingTileRuntimeSet.Items;
        floatingTileList = SortFloatingTiles(floatingTileList, direction);

        foreach (var floatingTile in floatingTileList)
        {
            tempGridTiles = floatingTile.GetComponent<TileMove>().MoveSide(direction, moveSideDuration, sideEase, tempGridTiles);
        }

        yield return new WaitForSeconds(moveSideDuration);

        var floatingTilesToAddToGrid = CheckAndMoveDownAll(moveDownDuration, rushEase);

        yield return new WaitForSeconds(moveDownDuration);

        if (floatingTilesToAddToGrid != null)
        {
            if (floatingTilesToAddToGrid.Count > 0)
            {
                AddFloatingTilesToGrid(floatingTilesToAddToGrid);
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
            var floatingTilesToAddToGrid = CheckAndMoveDownAll(tempMoveDownDuration, rushEase);

            yield return new WaitForSeconds(tempMoveDownDuration);

            if(floatingTilesToAddToGrid != null)
            {
                if(floatingTilesToAddToGrid.Count > 0)
                {
                    AddFloatingTilesToGrid(floatingTilesToAddToGrid);
                }
            }

            tempMoveDownDuration *= moveDownAcceleration;
        }

        if (!turnAvailable.Value)
        {
            turnDone.Invoke();
        }
    }

    private List<GameObject> CheckAndMoveDownAll(float duration, Ease ease)
    {
        List<GameObject> floatingTilesToAddToGrid = new List<GameObject>();

        foreach (var floatingTile in floatingTileRuntimeSet.Items)
        {
            var moveCheck = floatingTile.GetComponent<TileMove>().DownCheck();

            if (moveCheck != null)
            {
                floatingTilesToAddToGrid.Add(moveCheck);
            }
        }

        if (floatingTilesToAddToGrid.Count > 0)
        {
            return floatingTilesToAddToGrid;
        }

        foreach (var floatingTile in floatingTileRuntimeSet.Items)
        {
            var moveCheck = floatingTile.GetComponent<TileMove>().MoveDown(duration, ease);

            if (moveCheck != null)
            {
                floatingTilesToAddToGrid.Add(moveCheck);
            }
        }

        if (floatingTilesToAddToGrid.Count > 0)
        {
            return floatingTilesToAddToGrid;
        }

        return null;
    }

    private void AddFloatingTilesToGrid(List<GameObject> floatingTilesToAddToGrid)
    {
        floatingTilesToAddToGrid = SortFloatingTiles(floatingTilesToAddToGrid, Direction.Down);

        foreach (var floatingTileToDelete in floatingTilesToAddToGrid)
        {
            floatingTileToDelete.GetComponent<TileMove>().AddToGridTiles();
        }

        turnDone.Invoke();
    }

    private List<GameObject> SortFloatingTiles(List<GameObject> floatingTilesToAddToGrid, Direction direction)
    {
        if (direction == Direction.Down)
        {
            floatingTilesToAddToGrid.Sort(SortByRows);
            return floatingTilesToAddToGrid;
        }

        else if (direction == Direction.Left)
        {
            floatingTilesToAddToGrid.Sort(SortByColumnsLeft);
            return floatingTilesToAddToGrid;
        }

        else if (direction == Direction.Right)
        {
            floatingTilesToAddToGrid.Sort(SortByColumnsRight);
            return floatingTilesToAddToGrid;
        }

        return null;
    }

    private int SortByRows(GameObject tile1, GameObject tile2)
    {
        int tile1Height = (int) tile1.gameObject.transform.position.y;
        int tile2Height = (int) tile2.gameObject.transform.position.y;

        return tile1Height.CompareTo(tile2Height);
    }

    private int SortByColumnsLeft(GameObject tile1, GameObject tile2)
    {
        int tile1Height = (int)tile1.gameObject.transform.position.x;
        int tile2Height = (int)tile2.gameObject.transform.position.x;

        return tile1Height.CompareTo(tile2Height);
    }

    private int SortByColumnsRight(GameObject tile1, GameObject tile2)
    {
        int tile1Height = (int)tile1.gameObject.transform.position.x;
        int tile2Height = (int)tile2.gameObject.transform.position.x;

        return tile1Height.CompareTo(tile2Height);
    }
}
