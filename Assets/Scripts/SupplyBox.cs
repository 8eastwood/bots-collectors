using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SupplyBox : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}