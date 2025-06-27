using System.Collections;
using UnityEngine;

public class CollectorSpawner : PoolHandler<Collector>
{
    [SerializeField] private SupplyManager _supplyManager;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private DropOff _dropOff;
    // [SerializeField] private Base _base;
    [SerializeField] private float _delay;

    private Coroutine _spawnCollectorsRoutine;
    private SupplyBox _targetSupplyBox;
    private int _currentSupplyIndex = 0;
    private int _amountOfCollectors = 0;

    public void StartSpawnCollectors()
    {
        _spawnCollectorsRoutine = StartCoroutine(SpawnCollectors());
    }

    public void ReleaseCollector(Collector collector)
    {
        _pool.Release(collector);
        _amountOfCollectors--;
    }

    private IEnumerator SpawnCollectors()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return wait;

            if (_amountOfCollectors < PoolMaxSize && _supplyManager.IsAnySupplyUnassigned() == false)
            {
                TransferSupplyBoxAsTarget();
                GetCollectorFromPool();
                _amountOfCollectors++;
            }
        }
    }

    private void TransferSupplyBoxAsTarget()
    {
        _currentSupplyIndex = (_currentSupplyIndex + 1) % _supplyManager.SuppliesToCollect.Count;
        _targetSupplyBox = _supplyManager.SuppliesToCollect[_currentSupplyIndex];
    }

    private void GetCollectorFromPool()
    {
        if (_targetSupplyBox.IsScheduled == false)
        {
            Collector collector = _pool.Get();
            collector.transform.position = _spawnPoint.position;
            collector.RecieveDropOffPosition(_dropOff);
            collector.RecieveTargetPosition(_targetSupplyBox);
            _targetSupplyBox.SetScheduled();
            collector.InitFromPool();
        }
    }
}