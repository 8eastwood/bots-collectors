using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class PickingObjects : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    private float _offsetX = 2f;
    private float _offsetY = 2f;
    private bool _isPickedUp = false;

    public bool IsPickedUp => _isPickedUp;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void PickUp(Transform parent)
    {
        transform.SetParent(parent);
        transform.position = new Vector3(parent.position.x + _offsetX, parent.position.y + _offsetY, parent.position.z);

        _rigidbody.isKinematic = true;
        _boxCollider.enabled = false;
        _isPickedUp = true;
    }

    // public void DropOff()
    // {
    //     Destroy(gameObject);
    // }
}