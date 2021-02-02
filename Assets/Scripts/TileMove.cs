using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class TileMove : MonoBehaviour
{
    public Colour tileColour;

    public float fadeDuration;

    public UnityEvent<GameObject> GridAdd;
    public UnityEvent<int, int, GridTile> CheckMatch;

    public bool grounded = false;

    public IntVariable tilesToEndMove;

    public GridTiles gridTiles;

    public GameObjectRuntimeSet floatingTileRuntimeSet;
    public GameObjectRuntimeSet gridTileRuntimeSet;

    private void Awake()
    {
        if (!grounded)
        {
            floatingTileRuntimeSet.Add(this.gameObject);
        }

        if (grounded)
        {
            gridTileRuntimeSet.Add(this.gameObject);
        }
    }

    private void OnDisable()
    {
        floatingTileRuntimeSet.Remove(this.gameObject);
        gridTileRuntimeSet.Remove(this.gameObject);
    }

    public void AddToGridTiles(bool justStarted = false)
    {
        grounded = true;
        floatingTileRuntimeSet.Remove(this.gameObject);
        gridTileRuntimeSet.Add(this.gameObject);
        int column = (int)transform.position.x;
        int row = (int)transform.position.y;
        var thisTile = new GridTile(tileColour, this.gameObject);

        GridAdd.Invoke(this.gameObject);

        if(!justStarted)
        {
            CheckMatch.Invoke(column, row, thisTile);
        }
    }

    public GridTiles MoveSide(Direction direction, float duration, Ease ease, GridTiles tempGridTiles)
    {
        int modifier = 0;

        switch (direction)
        {
            case (Direction.Left):
                modifier = -1;
                break;
            case (Direction.Right):
                modifier = 1;
                break;
        }

        int targetColumn = (int)transform.position.x + modifier;

        int targetRow = (int)transform.position.y;
        var gridTile = new GridTile(tileColour, this.gameObject);

        Debug.Log("I am " + tileColour + " and I am going to MoveSide to " + targetColumn);

        if (!tempGridTiles.tileList.ContainsKey(targetColumn))
        {
            Debug.Log("I couldn't move because the column does not exist");
            if (!tempGridTiles.tileList.ContainsKey(targetColumn - modifier))
            {
                tempGridTiles.tileList.Add(targetColumn - modifier, new Dictionary<int, GridTile>() { { targetRow, gridTile } });
            }
            else
            {
                tempGridTiles.tileList[targetColumn - modifier].Add(targetRow, gridTile);
            }
            return tempGridTiles;
        }

        else if(tempGridTiles.tileList[targetColumn].ContainsKey(targetRow))
        {
            Debug.Log("I couldn't move because someone is there already");
            tempGridTiles.tileList[targetColumn - modifier].Add(targetRow, gridTile);
            return tempGridTiles;
        }

        Move(targetColumn, targetRow, duration, ease);

        Debug.Log("I moved");

        tempGridTiles.tileList[targetColumn].Add(targetRow, gridTile);
        return tempGridTiles;
    }

    public GameObject DownCheck()
    {
        int targetColumn = (int)transform.position.x;
        int targetRow = (int)transform.position.y - 1;

        Debug.Log("I am " + tileColour + " and I am going to MoveCheck for " + targetRow);

        if (gridTiles.tileList.ContainsKey(targetColumn))
        {
            if (gridTiles.tileList[targetColumn].ContainsKey(targetRow) && !grounded)
            {
                return this.gameObject;
            }
        }

        return null;
    }

    public GameObject MoveDown(float duration, Ease ease)
    {
        int targetColumn = (int)transform.position.x;
        int targetRow = (int)transform.position.y - 1;

        Debug.Log("I am " + tileColour + " and I am going to MoveDown to " + targetRow);

        Move(targetColumn, targetRow, duration, ease);

        if (gridTiles.tileList.ContainsKey(targetColumn))
        {
            if (gridTiles.tileList[targetColumn].ContainsKey(targetRow - 1) && !grounded)
            {
                return this.gameObject;
            }
        }

        return null;
    }

    private void Move(int targetColumn, int targetRow, float duration, Ease easing)
    {
        Vector3 target = new Vector3(targetColumn, targetRow, 0);
        transform.DOMove(target, duration, false)
            .SetEase(easing);
    }

    public IEnumerator Remove()
    {
        this.GetComponent<SpriteRenderer>().DOColor(Color.clear, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        Destroy(this.gameObject);
    }

    public void Reactivate()
    {
        grounded = false;
        floatingTileRuntimeSet.Add(this.gameObject);
    }
}
