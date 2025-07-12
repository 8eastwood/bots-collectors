using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class SupplySpawner : PoolHandler<SupplyBox>
{
    [SerializeField] private Storage _storage;
    [SerializeField] private SupplyBox _supplyBoxPrefab;
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private float minStartPointX;
    [SerializeField] private float maxStartPointX;
    [SerializeField] private float minStartPointZ;
    [SerializeField] private float maxStartPointZ;
    [SerializeField] private float startPointY;

    private Coroutine _supplySpawnRoutine;
    private Vector3 _spawnPoint;
    private int _maxSpawns = 5;
    private int _spawns = 0;

    private void Start()
    {
        StartSpawnSupply();
    }

    private void OnEnable()
    {
        _storage.NoSuppliesLeft += StartSpawnSupply;
        // _collisionHandler.CollectorReturned += ReleaseInPool;
        // _storage.Delivered += ReleaseInPool;
    }

    private void OnDisable()
    {
        _storage.NoSuppliesLeft -= StartSpawnSupply;
        // _collisionHandler.CollectorReturned -= ReleaseInPool;
        // _storage.Delivered -= ReleaseInPool;
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
                GetFromPool();
                _spawns++;
            }
        }
    }
    
    private void GetFromPool()
    {
        SupplyBox supplyBox = _pool.Get();
        supplyBox.transform.position = GetSpawnPoint();
        supplyBox.OnDestroy += ReleaseInPool;
    }
    
    private Vector3 GetSpawnPoint()
    {
        return new Vector3(Random.Range(minStartPointX, maxStartPointX), startPointY,
            Random.Range(minStartPointZ, maxStartPointZ));
    }

    private void ReleaseInPool(SupplyBox supplyBox)
    {
        supplyBox.Rigidbody.isKinematic = false;
        supplyBox.BoxCollider.enabled = true;
        _pool.Release(supplyBox);
        
        _storage.Delivered -= ReleaseInPool;
    }
}