using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private SupplyManager _supplyManager;
    [SerializeField] private float _scanRadius;

    private static int _supplyPlacementInLayers = 3;

    private Queue<SupplyBox> _collectableSupply;
    private float _delay = 4;
    private bool _isThereSupplyToCollect = false;
    private int _targetLayer = 1 << _supplyPlacementInLayers;

    public event Action SuppliesFounded;

    private void Start()
    {
        StartScan();
    }

    private void OnEnable()
    {
        _supplyManager.NoSuppliesLeft += StartScan;
    }

    private void OnDisable()
    {
        _supplyManager.NoSuppliesLeft -= StartScan;
    }

    private void StartScan()
    {
        StartCoroutine(ScanWithRate());
    }

    private Queue<SupplyBox> ScanForSupplies()
    {
        Collider[] supplies = Physics.OverlapSphere(transform.position, _scanRadius, _targetLayer);

        Queue<SupplyBox> toCollect = new();

        foreach (Collider supply in supplies)
        {
            toCollect.Enqueue(supply.GetComponent<SupplyBox>());
        }

        if (toCollect.Count > 0)
        {
            _supplyManager.GetSuppliesToCollect(toCollect);
            SuppliesFounded?.Invoke();
        }
        else
        {
            _isThereSupplyToCollect = false;
        }

        return toCollect;
    }

    private IEnumerator ScanWithRate()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);
        while (enabled)
        {
            yield return wait;

            if (_isThereSupplyToCollect == false)
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