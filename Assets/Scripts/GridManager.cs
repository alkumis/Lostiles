using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private enum CheckingType
    {
        Colour,
        Grounding
    }

    public GridTiles gridTiles;

    private void OnDisable()
    {
        gridTiles.tileList = new SortedList<int, SortedList<int, GridTile>>();
    }

    public void AddToGridTiles(int column, int row, GridTile gridTile)
    {
        if (gridTiles.tileList.ContainsKey(column))
        {
            gridTiles.tileList[column].Add(row, gridTile);
            CheckMatches(column, row, gridTile.Colour);
            return;
        }

        else
        {
            gridTiles.tileList.Add(column, new SortedList<int, GridTile>() { { row, gridTile } });
            CheckMatches(column, row, gridTile.Colour);
            return;
        }
    }

    private void CheckMatches(int column, int row, Colour colourToCheck)
    {
        var matchingRows = new ArrayList
        {
            row
        };

        var matchingColumns = new ArrayList
        {
            column
        };

        matchingRows = GridCheck(Direction.Up, matchingRows, column, row, colourToCheck);
        matchingRows = GridCheck(Direction.Down, matchingRows, column, row, colourToCheck);
        matchingColumns = GridCheck(Direction.Left, matchingColumns, column, row, colourToCheck);
        matchingColumns = GridCheck(Direction.Right, matchingColumns, column, row, colourToCheck);

        var tilesToFloat = new ArrayList();

        if (matchingColumns.Count >= 3 || matchingRows.Count >= 3)
        {
            foreach (int r in matchingRows)
            {
                StartCoroutine(gridTiles.tileList[column][r].Tile.GetComponent<TileMove>().Remove());
            }
            foreach (int c in matchingColumns)
            {
                StartCoroutine(gridTiles.tileList[c][row].Tile.GetComponent<TileMove>().Remove());
            }
            foreach (int r in matchingRows)
            {
                tilesToFloat = GridCheck(Direction.Up, tilesToFloat, column, r, colourToCheck, CheckingType.Grounding);
                gridTiles.tileList[column].Remove(r);
            }
            foreach (int c in matchingColumns)
            {
                tilesToFloat = GridCheck(Direction.Up, tilesToFloat, c, row, colourToCheck, CheckingType.Grounding);
                gridTiles.tileList[c].Remove(row);
            }
        }
    }

    private ArrayList GridCheck(Direction direction, ArrayList list, int column, int row, Colour colourToCheck, CheckingType checkingType = CheckingType.Colour)
    {
        ArrayList positionHolder = (ArrayList)list.Clone();

        bool checking = true;

        while (checking)
        {
            switch (direction)
            {
                case (Direction.Left):
                    column -= 1;
                    break;
                case (Direction.Right):
                    column += 1;
                    break;
                case (Direction.Up):
                    row += 1;
                    break;
                case (Direction.Down):
                    row -= 1;
                    break;
            }

            if (gridTiles.tileList.ContainsKey(column))
            {
                if (gridTiles.tileList[column].ContainsKey(row))
                {
                    switch (checkingType)
                    {
                        case (CheckingType.Colour):
                            if (gridTiles.tileList[column][row].Colour == colourToCheck)
                            {
                                if(direction == Direction.Up || direction == Direction.Down)
                                {
                                    positionHolder.Add(row);
                                }
                                if (direction == Direction.Left || direction == Direction.Right)
                                {
                                    positionHolder.Add(column);
                                }
                            }
                            else
                            {
                                checking = false;
                            }
                            break;

                        case (CheckingType.Grounding):
                            gridTiles.tileList[column][row].Tile.GetComponent<TileMove>().grounded = false;
                            gridTiles.tileList[column].Remove(row);
                            break;
                    }
                }
                else
                {
                    checking = false;
                }
            }
            else
            {
                checking = false;
            }
        }

        return positionHolder;
    }
}
