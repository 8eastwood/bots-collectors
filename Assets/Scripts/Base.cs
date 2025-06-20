using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Base : MonoBehaviour
{
    [SerializeField] private CollectorSpawner _collectorSpawner;
    [SerializeField] private Scanner _scanner;

    private List<Rigidbody> _suppliesToCollect;
    // private float _rate = 3f;

    public List<Rigidbody> SuppliesToCollect => _suppliesToCollect;

    private void Awake()
    {
        _suppliesToCollect = new List<Rigidbody>();
        // StartCoroutine(TryGetCollectibleSupplies());
    }

    private void OnEnable()
    {
        _scanner.SuppliesFounded += SpawnCollectors;
    }

    private void OnDisable()
    {
        _scanner.SuppliesFounded -= SpawnCollectors;
    }

    // public void RemoveScannedSupplies(Rigidbody supply)
    // {
    //     _suppliesToCollect.Remove(supply);
    // }

    private void SpawnCollectors()
    {
        _collectorSpawner.StartSpawnCollectors();
    }

    // private IEnumerator TryGetCollectibleSupplies()
    // {
    //     WaitForSeconds wait = new WaitForSeconds(_rate);
    //
    //     if (_scanner.IsThereSupplyToCollect)
    //     {
    //         yield return wait;
    //
    //         _suppliesToCollect = _scanner.TransferSupplyToCollect();
    //     }
    // }
}