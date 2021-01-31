// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;

public class GridEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GridEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<int, int, GridTile> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(int row, int column, GridTile gridTile)
    {
        Response.Invoke(row, column, gridTile);
    }
}
