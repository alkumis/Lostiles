// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;

public class GameObjectEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameObjectEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<GameObject> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(GameObject tile)
    {
        Response.Invoke(tile);
    }
}
