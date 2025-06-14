using System;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;
    [SerializeField] private LayerMask _layerMask;

    private bool isThereSupplyToCollect = false;
    private List<Rigidbody> _collectableSupply;

    public event Action SuppliesToCollect;

    private void Start()
    {
        if (isThereSupplyToCollect == false)
        {
            _collectableSupply = ScanForSupplies();
            SuppliesToCollect?.Invoke();
        }
    }

    private List<Rigidbody> ScanForSupplies()
    {
        Collider[] supplies = Physics.OverlapSphere(transform.position, _scanRadius, _layerMask);

        List<Rigidbody> toCollect = new();

        foreach (Collider supply in supplies)
        {
            if (supply.attachedRigidbody != null)
            {
                toCollect.Add(supply.attachedRigidbody);
                Debug.Log("added");
            }
        }

        isThereSupplyToCollect = true;

        return toCollect;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _scanRadius);
    }
}