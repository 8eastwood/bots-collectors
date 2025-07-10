using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private Storage _storage;
    [SerializeField] private float _scanRadius;

    private static int _supplyPlacementInLayers = 3;

    private Queue<SupplyBox> _collectableSupply;
    private Coroutine _scanRoutine;
    private float _delay = 4;
    // private bool _isThereSupplyToCollect = false;
    private int _targetLayer = 1 << _supplyPlacementInLayers;

    public event Action SuppliesFounded;

    private void Start()
    {
        StartScan();
    }

    private void OnEnable()
    {
        _storage.NoSuppliesLeft += StartScan;
    }

    private void OnDisable()
    {
        _storage.NoSuppliesLeft -= StartScan;
    }

    private void StartScan()
    {
        _scanRoutine = StartCoroutine(ScanWithRate());
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
            _storage.GetSuppliesToCollect(toCollect);
            // _isThereSupplyToCollect = true;
            SuppliesFounded?.Invoke();
            StopCoroutine(_scanRoutine);
        }
        // else
        // {
        //     _isThereSupplyToCollect = false;
        // }

        return toCollect;
    }

    private IEnumerator ScanWithRate()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);
        while (enabled)
        {
            yield return wait;
            Debug.Log("scanned");

            // if (!_isThereSupplyToCollect)
            // {
                _collectableSupply = ScanForSupplies();
            // }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _scanRadius);
    }
}