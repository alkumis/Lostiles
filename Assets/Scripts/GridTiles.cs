// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GridTiles : ScriptableObject
{
    public Dictionary<int, Dictionary<int, GridTile>> tileList = new Dictionary<int, Dictionary<int, GridTile>>();

    public void Copy(GridTiles gridTilesToCopy)
    {
        foreach (var column in gridTilesToCopy.tileList.Keys)
        {
            foreach (var row in gridTilesToCopy.tileList[column].Keys)
            {
                if (!tileList.ContainsKey(column))
                {
                    tileList.Add(column, new Dictionary<int, GridTile>() { { row, gridTilesToCopy.tileList[column][row] } });
                }
                else
                {
                    tileList[column].Add(row, gridTilesToCopy.tileList[column][row]);
                }
            }
        }
    }

}
