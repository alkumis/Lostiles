// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;


[CreateAssetMenu]
public class GameObjectRuntimeSet : RuntimeSet<GameObject>
{
    public void Sort(Direction direction)
    {
        if (direction == Direction.Down)
        {
            this.Items.Sort(SortByRows);
        }

        else if (direction == Direction.Left)
        {
            this.Items.Sort(SortByColumnsLeft);
        }

        else if (direction == Direction.Right)
        {
            this.Items.Sort(SortByColumnsRight);
        }
    }

    private int SortByRows(GameObject tile1, GameObject tile2)
    {
        int tile1Height = (int)tile1.gameObject.transform.position.y;
        int tile2Height = (int)tile2.gameObject.transform.position.y;

        return tile2Height.CompareTo(tile1Height);
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

        return tile2Height.CompareTo(tile1Height);
    }
}
