using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class SupplySpawner : MonoBehaviour
{
    [SerializeField] private SupplyBox _supplyBox;
    [SerializeField] private float minStartPointX;
    [SerializeField] private float maxStartPointX;
    [SerializeField] private float minStartPointZ;
    [SerializeField] private float maxStartPointZ;
    [SerializeField] private float startPointY;

    // private float _delay = 0f;
    private int _maxSpawns = 5;
    private int _spawns = 0;

    private void Start()
    {
        StartCoroutine(SpawnSupply());
    }

    private IEnumerator SpawnSupply()
    {
        // WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_spawns < _maxSpawns)
        {
            yield return null;

            Vector3 spawnPosition = new Vector3(Random.Range(minStartPointX, maxStartPointX), startPointY,
                Random.Range(minStartPointZ, maxStartPointZ));

            Spawn(spawnPosition);
            _spawns++;
            
            Debug.Log(_spawns);
        }
    }

    private void Spawn(Vector3 spawnPosition)
    {
        Instantiate(_supplyBox, spawnPosition, Quaternion.identity);
    }
}