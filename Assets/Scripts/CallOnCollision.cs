using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CallOnCollision : MonoBehaviour
{
    public EventTrigger.TriggerEvent callback;

    private void OnTriggerEnter2D(Collider2D col)
    {
        callback.Invoke(null);
    }
}
