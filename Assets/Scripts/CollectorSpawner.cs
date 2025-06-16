using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class CollectorSpawner : PoolHandler<Collector>
{
    [SerializeField] private float _delay;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Scanner _scanner;

    private int _amountOfCollectors = 0;

    private void OnEnable()
    {
        _scanner.SuppliesToCollect += StartSpawnCollectors;
    }

    private void OnDisable()
    {
        _scanner.SuppliesToCollect -= StartSpawnCollectors;
    }

    private void StartSpawnCollectors()
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
            
            GetCollectorFromPool();
            _amountOfCollectors++;
        }
    }

    private void GetCollectorFromPool()
    {
        Collector collector = _pool.Get();
        
        collector.transform.position = _spawnPoint.position;

        // collector.Removed += ReleaseCollector;
    }
}