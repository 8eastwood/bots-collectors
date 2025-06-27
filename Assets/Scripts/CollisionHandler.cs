using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public Action<Collector> CollectorReturned;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Collector collector))
        {
            Debug.Log("collector enter");
            CollectorReturned?.Invoke(collector);
        }
    }
}