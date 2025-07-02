using System;
using System.Collections.Generic;
using UnityEngine;

public class SupplyManager : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;

    private Queue<SupplyBox> _suppliesToCollect;
    private List<SupplyBox> _suppliesToDeliver;

    private int _index = 0;
    private SupplyBox _supplyToAssign;

    public Queue<SupplyBox> SuppliesToCollect => _suppliesToCollect;
    public List<SupplyBox> SuppliesToDeliver => _suppliesToDeliver;

    public Action NoSuppliesLeft;
    public Action<SupplyBox> Delivered;

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
        RemoveSupplies(supply);
        Delivered?.Invoke(supply);
        _scoreCounter.Add();
    }

    private void RemoveSupplies(SupplyBox supply)
    {
        _suppliesToDeliver.Remove(supply);

        if (_suppliesToDeliver.Count == 0)
        {
            _suppliesToDeliver = null;
            _suppliesToCollect = null;
            NoSuppliesLeft?.Invoke();
        }
    }
}