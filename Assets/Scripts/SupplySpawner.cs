using System.Collections;
using UnityEngine;

public class SupplySpawner : MonoBehaviour
{
    [SerializeField] private SupplyBox _supplyBox;
    
    private float _delay = 2f;

    private void Update()
    {
        
    }

    private IEnumerator SpawnSupply()
    {
        Spawn();
    }

    private void Spawn()
    {
        Instantiate(_supplyBox);
    }
}

