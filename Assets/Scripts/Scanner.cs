using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;

    private static int _supplyPlacementInLayers = 3;
    
    private List<Rigidbody> _collectableSupply;
    private float _delay = 4;
    private bool isThereSupplyToCollect = false;
    private int _targetLayer = 1 << _supplyPlacementInLayers;

    public event Action SuppliesToCollect;

    private void Start()
    {
        StartCoroutine(StartScanWithRate());
    }
    
    private List<Rigidbody> ScanForSupplies()
    {
        Collider[] supplies = Physics.OverlapSphere(transform.position, _scanRadius, _targetLayer);
    
        List<Rigidbody> toCollect = new();
    
        foreach (Collider supply in supplies)
        {
            if (supply.attachedRigidbody != null)
            {
                toCollect.Add(supply.attachedRigidbody);
            }
        }
    
        if (toCollect.Count > 0)
        {
            isThereSupplyToCollect = true;
            SuppliesToCollect?.Invoke();
        }
        else
        {
            isThereSupplyToCollect = false;
        }
    
        return toCollect;
    }
    
    private IEnumerator StartScanWithRate()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);
        while (enabled)
        {
            yield return wait;

            if (isThereSupplyToCollect == false)
            {
                _collectableSupply = ScanForSupplies();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _scanRadius);
    }
}