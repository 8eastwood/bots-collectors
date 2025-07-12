using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class SupplyBox : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private Rigidbody _rigidbody;
    private float _offsetX = 2f;
    private float _offsetY = 2f;
    
    public Action<SupplyBox> OnDestroy;

    public BoxCollider BoxCollider => _boxCollider;
    public Rigidbody Rigidbody => _rigidbody;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnPickUp(Transform parent)
    {
        transform.SetParent(parent);
        transform.position = new Vector3(parent.position.x + _offsetX, parent.position.y + _offsetY, parent.position.z);
        _rigidbody.isKinematic = true;
        _boxCollider.enabled = false;
    }

    public void Destroy()
    {
        transform.SetParent(null);
        OnDestroy?.Invoke(this);
    }
}