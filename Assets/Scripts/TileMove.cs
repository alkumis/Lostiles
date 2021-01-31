using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class TileMove : MonoBehaviour
{
    public Colour tileColour;

    public float fadeDuration;

    public UnityEvent<int, int, GridTile> GridAdd;

    public bool grounded = false;

    public IntVariable activeFloatingTiles;
    public IntVariable tilesToEndMove;

    public GridTiles gridTiles;

    public GameObjectRuntimeSet floatingTileRuntimeSet;

    private void Awake()
    {
        activeFloatingTiles.ApplyChange(1);

        if (!grounded)
        {
            floatingTileRuntimeSet.Add(this.gameObject);
        }

        if (grounded)
        {
            AddToGridTiles();
        }
    }

    private void OnDisable()
    {
        floatingTileRuntimeSet.Remove(this.gameObject);
    }

    public void AddToGridTiles()
    {
        grounded = true;
        activeFloatingTiles.ApplyChange(-1);
        floatingTileRuntimeSet.Remove(this.gameObject);
        int column = (int)transform.position.x;
        Debug.Log("Column " + column);
        int row = (int)transform.position.y;
        Debug.Log("Row " + row);
        var thisTile = new GridTile(tileColour, this.gameObject);
        Debug.Log("Colour " + tileColour);

        GridAdd.Invoke(column, row, thisTile);
    }

    public void MoveSide(int direction, float duration, Ease ease)
    {
        int targetColumn = (int)transform.position.x + direction;
        int targetRow = (int)transform.position.y;

        if (!gridTiles.tileList.ContainsKey(targetColumn) || gridTiles.tileList[targetColumn].ContainsKey(targetRow))
        {
            return;
        }

        Move(targetColumn, targetRow, duration, ease);
    }

    public TileMove MoveCheck()
    {
        Debug.Log("MoveCheck");
        int targetColumn = (int)transform.position.x;
        int targetRow = (int)transform.position.y - 1;

        if (gridTiles.tileList[targetColumn].ContainsKey(targetRow) && !grounded)
        {
            return this;
        }

        return null;
    }

    public TileMove MoveDown(float duration, Ease ease)
    {
        Debug.Log("MoveDown");
        int targetColumn = (int)transform.position.x;
        int targetRow = (int)transform.position.y - 1;

        Move(targetColumn, targetRow, duration, ease);

        if (gridTiles.tileList[targetColumn].ContainsKey(targetRow - 1) && !grounded)
        {
            return this;
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
