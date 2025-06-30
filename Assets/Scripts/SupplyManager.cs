using System;
using System.Collections.Generic;
using UnityEngine;

public class SupplyManager : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    
    private List<SupplyBox> _suppliesToCollect;

    public List<SupplyBox> SuppliesToCollect => _suppliesToCollect;

    public Action NoSuppliesLeft;
    public Action<SupplyBox> Delivered;

    private void Awake()
    {
        _suppliesToCollect = new List<SupplyBox>();
    }

    public void GetSuppliesToCollect(List<SupplyBox> suppliesToCollect)
    {
        _suppliesToCollect = suppliesToCollect;
    }

    public bool IsAnySupplyUnassigned()
    {
        if (_suppliesToCollect != null)
        {
            foreach (SupplyBox supply in _suppliesToCollect)
            {
                if (supply.IsScheduled == false)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void SupplyDelivered(SupplyBox supply)
    {
        RemoveSupplies(supply);
        Delivered?.Invoke(supply);
        _scoreCounter.Add();
    }

    private void RemoveSupplies(SupplyBox supply)
    {
        _suppliesToCollect.Remove(supply);

        if (_suppliesToCollect.Count == 0)
        {
            _suppliesToCollect = null;
            NoSuppliesLeft?.Invoke();
        }
    }
}