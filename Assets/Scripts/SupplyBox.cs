using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class SupplyBox : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private Rigidbody _rigidbody;
    private float _offsetX = 2f;
    private float _offsetY = 2f;
    // private bool _isPickedUp = false;
    // private bool _isScheduled = false;

    // public bool IsPickedUp => _isPickedUp;
    // public bool IsScheduled => _isScheduled;
    public Rigidbody Rigidbody => _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    // public void SetScheduled()
    // {
    //     _isScheduled = true;
    // }

    public void OnPickUp(Transform parent)
    {
        transform.SetParent(parent);
        transform.position = new Vector3(parent.position.x + _offsetX, parent.position.y + _offsetY, parent.position.z);

        _rigidbody.isKinematic = true;
        _boxCollider.enabled = false;
        // _isPickedUp = true;
    }
}