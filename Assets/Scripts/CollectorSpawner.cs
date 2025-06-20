using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class CollectorSpawner : PoolHandler<Collector>
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Base _base;
    [SerializeField] private float _delay;

    private Rigidbody _targetSupplyBox;

    private int _amountOfCollectors = 0;

    public void StartSpawnCollectors()
    {
        StartCoroutine(SpawnCollectors());
    }

    private IEnumerator SpawnCollectors()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_amountOfCollectors < PoolMaxSize)
        {
            yield return wait;

            // Instantiate(_collector, _spawnPoint.position, Quaternion.identity);

            TransferSupplyBoxAsTarget();
            GetCollectorFromPool();
            _amountOfCollectors++;
        }
    }

    private void TransferSupplyBoxAsTarget()
    {
        foreach (Rigidbody supply in _base.SuppliesToCollect)
        {
            Debug.Log("таргет получен спавнером");
            _targetSupplyBox = supply;
            // _supply.RemoveScannedSupplies(supply);
        }
    }

    private void GetCollectorFromPool()
    {
        Collector collector = _pool.Get();
        collector.transform.position = _spawnPoint.position;
        collector.RecieveTargetPosition(_targetSupplyBox);
        Debug.Log("вытащили из пула");

        // collector.Removed += ReleaseCollector;
    }
}