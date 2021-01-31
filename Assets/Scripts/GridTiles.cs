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
    public SortedList<int, SortedList<int, GridTile>> tileList = new SortedList<int, SortedList<int, GridTile>>();
}
