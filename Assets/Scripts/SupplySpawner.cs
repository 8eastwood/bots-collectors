using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class SupplySpawner : MonoBehaviour
{
    [SerializeField] private SupplyManager _supplyManager;
    [SerializeField] private SupplyBox _supplyBoxPrefab;
    [SerializeField] private float minStartPointX;
    [SerializeField] private float maxStartPointX;
    [SerializeField] private float minStartPointZ;
    [SerializeField] private float maxStartPointZ;
    [SerializeField] private float startPointY;

    private Coroutine _supplySpawnRoutine;
    private Vector3 _spawnPoint;
    private int _maxSpawns = 5;
    private int _spawns = 0;

    private void Awake()
    {
        StartSpawnSupply();
    }

    private void OnEnable()
    {
        _supplyManager.NoSuppliesLeft += StartSpawnSupply;
        _supplyManager.Delivered += Destroy;
    }

    private void OnDisable()
    {
        _supplyManager.NoSuppliesLeft -= StartSpawnSupply;
        _supplyManager.Delivered -= Destroy;
    }

    private void StartSpawnSupply()
    {
        if (_supplySpawnRoutine != null)
        {
            StopCoroutine(_supplySpawnRoutine);
            _spawns = 0;
        }

        _supplySpawnRoutine = StartCoroutine(SpawnSupply());
    }

    private IEnumerator SpawnSupply()
    {
        while (enabled)
        {
            yield return null;

            if (_spawns < _maxSpawns)
            {
                CreateSupply();
                _spawns++;
            }
        }
    }

    private void CreateSupply()
    {
        Instantiate(_supplyBoxPrefab, GetSpawnPoint(), Quaternion.identity);
    }

    private Vector3 GetSpawnPoint()
    {
        return new Vector3(Random.Range(minStartPointX, maxStartPointX), startPointY,
            Random.Range(minStartPointZ, maxStartPointZ));
    }

    private void Destroy(SupplyBox supplyBox)
    {
        Destroy(supplyBox.gameObject);
    }
}