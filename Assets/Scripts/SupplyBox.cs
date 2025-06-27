using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PickingObjects))]
public class SupplyBox : MonoBehaviour
{
    private PickingObjects _pickingObjects;
    private bool _isScheduled = false;
    
    public bool IsScheduled => _isScheduled;
    
    public PickingObjects PickingObjects => _pickingObjects;

    private void Awake()
    {
        _pickingObjects = GetComponent<PickingObjects>();
    }

    public void SetScheduled()
    {
        _isScheduled = true;
    }

    public void TryPickUp(Transform parent)
    {
        PickingObjects.PickUp(parent);
    }

    // public void ReleaseInPool()
    // {
    //     _pool.Release(this);
    // }
}