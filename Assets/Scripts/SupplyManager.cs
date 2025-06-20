using System.Collections.Generic;
using UnityEngine;

public class SupplyManager : MonoBehaviour
{
    private List<Rigidbody> _suppliesToCollect;
    
    public List<Rigidbody> SuppliesToCollect => _suppliesToCollect;

    private void Awake()
    {
        _suppliesToCollect = new List<Rigidbody>();
    }

    public void GetSuppliesToCollect(List<Rigidbody> suppliesToCollect)
    {
        _suppliesToCollect = suppliesToCollect;
    }

    public void RemoveSupplies(Rigidbody supply)
    {
        _suppliesToCollect.Remove(supply);
    }
}