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

    public GameObjectRuntimeSet gridTileRuntimeSet;

    private GameObjectRuntimeSet gridTilesToCheck;

    bool checkTiles = false;

    private void OnDisable()
    {
        gridTiles.tileList = new Dictionary<int, Dictionary<int, GridTile>>();
    }

    private void Start()
    {
        gridTilesToCheck = ScriptableObject.CreateInstance<GameObjectRuntimeSet>();

        foreach (var tile in gridTileRuntimeSet.Items)
        {
            AddToGridTiles(tile);
        }

        checkTiles = true;
    }

    public void AddToGridTiles(GameObject tile)
    {
        int column = (int)tile.transform.position.x;
        int row = (int)tile.transform.position.y;
        var gridTile = new GridTile(tile.GetComponent<TileMove>().tileColour, tile);

        if (checkTiles)
        {
            gridTilesToCheck.Add(tile);
            Debug.Log("gridTilesToCheck + 1");
        }

        if (gridTiles.tileList.ContainsKey(column))
        {
            gridTiles.tileList[column].Add(row, gridTile);
            return;
        }

        else
        {
            gridTiles.tileList.Add(column, new Dictionary<int, GridTile>() { { row, gridTile } });
            return;
        }
    }

    public void CheckMatches()
    {
        Debug.Log("Check matches is being called");

        Debug.Log("gridTilesToCheck Count = " + gridTilesToCheck.Items.Count);

        if(gridTilesToCheck.Items.Count <= 0)
        {
            Debug.Log("Nothing to check");
            return;
        }

        for (int i = gridTilesToCheck.Items.Count - 1; i >= 0; i--)
        {
            var tile = gridTilesToCheck.Items[i];

            if(tile == null)
            {
                Debug.Log("Tile is destroyed now");
                continue;
            }

            if (!tile.GetComponent<TileMove>().grounded)
            {
                Debug.Log("Tile is floating now");
                continue;
            }

            int column = (int)tile.transform.position.x;
            int row = (int)tile.transform.position.y;
            var gridTile = new GridTile(tile.GetComponent<TileMove>().tileColour, tile);

            Colour colourToCheck = gridTile.Colour;

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
                    var tileToRemove = gridTiles.tileList[column][r].Tile;

                    StartCoroutine(tileToRemove.GetComponent<TileMove>().Remove());
                }
                foreach (int c in matchingColumns)
                {
                    var tileToRemove = gridTiles.tileList[c][row].Tile;

                    StartCoroutine(tileToRemove.GetComponent<TileMove>().Remove());
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
                            var tile = gridTiles.tileList[column][row].Tile;
                            tile.GetComponent<TileMove>().Reactivate();
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
