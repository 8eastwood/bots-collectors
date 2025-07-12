using System;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private CollectorSpawner _collectorSpawner;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private Base _base;

    private Queue<SupplyBox> _suppliesToCollect;
    private List<SupplyBox> _suppliesToDeliver;
    private List<Collector> _collectors;
    private SupplyBox _supplyToAssign;

    public Queue<SupplyBox> SuppliesToCollect => _suppliesToCollect;
    public List<SupplyBox> SuppliesToDeliver => _suppliesToDeliver;

    public Action<SupplyBox> Delivered;
    public Action NoSuppliesLeft;

    private void Awake()
    {
        _suppliesToCollect = new Queue<SupplyBox>();
        _suppliesToDeliver = new List<SupplyBox>();
    }

    public SupplyBox AssignTask()
    {
        if (_supplyToAssign != null)
        {
            _supplyToAssign = null;
        }

        if (_suppliesToCollect.Count != 0)
        {
            _supplyToAssign = _suppliesToCollect.Dequeue();
            // Debug.Log("к сбору " + _suppliesToCollect.Count);
            _suppliesToDeliver.Add(_supplyToAssign);
        }
        else
        {
            return null;
        }

        return _supplyToAssign;
    }

    public void GetSuppliesToCollect(Queue<SupplyBox> suppliesToCollect)
    {
        foreach (SupplyBox supply in suppliesToCollect)
        {
            if (!_suppliesToCollect.Contains(supply) && !_suppliesToDeliver.Contains(supply))
            {
                _suppliesToCollect.Enqueue(supply);
            }
        }
    }

    public void SupplyDelivered(SupplyBox supply)
    {
        Delivered?.Invoke(supply);
        Debug.Log("delivered");
        RemoveSuppliesFromCollection(supply);
        _scoreCounter.Add();
    }

    private void RemoveSuppliesFromCollection(SupplyBox supply)
    {
        _suppliesToDeliver.Remove(supply);
        
        if (_suppliesToDeliver.Count == 0)
        {
            Debug.Log("припасы кончились");
            NoSuppliesLeft?.Invoke();
        }
    }
}