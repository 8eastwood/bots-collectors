using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private CollectorSpawner _collectorSpawner;
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private Storage _storage;
    [SerializeField] private Scanner _scanner;

    private List<Collector> _busyCollectors;
    private List<Collector> _freeCollectors;

    public Action<Collector> Reassigned;

    private void Awake()
    {
        _busyCollectors = new List<Collector>();
        _freeCollectors = new List<Collector>();
    }

    private void Start()
    {
        Debug.Log("1");
        SpawnCollectors();
    }

    private void OnEnable()
    {
        _scanner.SuppliesFounded += AssignCollector;
        _collisionHandler.CollectorReturned += SetFreeFromTask;
    }

    private void OnDisable()
    {
        _scanner.SuppliesFounded -= AssignCollector;
        _collisionHandler.CollectorReturned -= SetFreeFromTask;
    }

    public void GetCollectors(Collector collector)
    {
        _busyCollectors.Add(collector);
        // Debug.Log(_busyCollectors.Count);
    }

    private void SpawnCollectors()
    {
        _collectorSpawner.StartSpawnCollectors();
    }

    private void SetFreeFromTask(Collector collector)
    {
        // _collectorSpawner.ResetToSpawnPoint(collector);
        _storage.SupplyDelivered(collector.TargetSupplyBox);
        collector.TargetSupplyBox.Destroy();
        collector.FreeFromTask();
        collector.TryBackToSpawnPoint();
        _freeCollectors.Add(collector);
        _busyCollectors.Remove(collector);

        if (_freeCollectors.Count != 0 && _storage.SuppliesToCollect.Count > 0)
        {
            AssignCollector();
        }
    }

    private void AssignCollector()
    {
        // Debug.Log(_freeCollectors.Count);

        for (int i = _freeCollectors.Count - 1; i >= 0; i--)
        {
            Collector collector = _freeCollectors[i];

            if (!collector.IsBusy)
            {
                collector.RecieveTargetPosition(_collectorSpawner.RequestToAssignTask());
                Reassigned?.Invoke(collector);
                _busyCollectors.Add(collector);
                _freeCollectors.RemoveAt(i);
            }
        }
    }
}