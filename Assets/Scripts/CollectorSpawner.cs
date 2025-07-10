using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Storage _storage;
    [SerializeField] private Collector _collectorPrefab;
    [SerializeField] private DropOff _dropOff;
    [SerializeField] private Base _base;
    [SerializeField] private float _delay;

    private Coroutine _spawnCollectorsRoutine;
    private SupplyBox _targetSupplyBox;
    private Transform _spawnPoint;
    private int _amountOfCollectorsToSpawn = 3;
    private int _indexOfCollectors = 0;

    private void OnEnable()
    {
        _base.Reassigned += SendToWork;
    }

    private void OnDisable()
    {
        _base.Reassigned -= SendToWork;
    }

    public void StartSpawnCollectors()
    {
        _spawnCollectorsRoutine = StartCoroutine(SpawnCollectors()); // скан кидает событие, база подписывается методом SpawnCollectors,
                                                                     // а надо чтобы существующие коллекторы переназначались для этой задачи во второй этерации. 
    }

    // public void ResetToSpawnPoint(Collector collector)
    // {
    //     collector.TryBackToSpawnPoint();
    // }

    public SupplyBox RequestToAssignTask()
    {
        SupplyBox supply = _storage.AssignTask();

        if (supply != null)
        {
            return supply;
        }

        return null;
    }

    private IEnumerator SpawnCollectors()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);
        Debug.Log("2");

        while (_storage.SuppliesToDeliver != null)
        {
            yield return wait;

            if (_indexOfCollectors < _amountOfCollectorsToSpawn && _storage.SuppliesToCollect != null)
            {
                _targetSupplyBox = RequestToAssignTask();

                if (_targetSupplyBox != null)
                {
                    Collector collector = Spawn(_spawnPoints[_indexOfCollectors]);
                    collector.RecieveTargetPosition(_targetSupplyBox);
                    collector.RecieveDropOffPosition(_dropOff);
                    collector.RecieveSpawnPoint(_spawnPoints[_indexOfCollectors]);
                    SendToWork(collector);
                    _base.GetCollectors(collector);
                    _indexOfCollectors++;
                }
            }
            else
            {
                StopCoroutine(_spawnCollectorsRoutine);
            }
        }
    }

    private void SendToWork(Collector collector)
    {
        collector.Init();
    }

    private Collector Spawn(Transform spawnPoint)
    {
        return Instantiate(_collectorPrefab, spawnPoint.position, Quaternion.identity);
    }
}