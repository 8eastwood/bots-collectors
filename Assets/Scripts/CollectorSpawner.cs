using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class CollectorSpawner : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Collector _collector;
    [SerializeField] private Scanner _scanner;

    private int _maxCollectors = 3;
    private int _amountOfCollectors = 0;

    private void Start()
    {
        // if (SuppliesBeenFounded())
        // {
        // }
        // StartCoroutine(SpawnCollectors());

        //тут надо запуск корутины сунуть в метод который подпишется на событие сканера, при нахождении припасов вызов события сработает и запустится корутина которая заспавнит грузовики
    }

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

        while (_amountOfCollectors < _maxCollectors)
        {
            yield return wait;

            Instantiate(_collector, _spawnPoint.position, Quaternion.identity);
            _amountOfCollectors++;
        }
    }
}