using System;
using System.Collections.Generic;
using UnityEngine;

public class SupplyManager : MonoBehaviour
{
    private List<SupplyBox> _suppliesToCollect;
    private int _deliviredSupplies = 0;

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
            Debug.Log("не нулл");
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
        // supply.ReleaseInPool();
        _deliviredSupplies++;

        // Debug.Log(_suppliesToCollect.Count);
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