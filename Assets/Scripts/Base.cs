using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private CollectorSpawner _collectorSpawner;
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private SupplyManager _supplyManager;
    [SerializeField] private Scanner _scanner;

    private void OnEnable()
    {
        _scanner.SuppliesFounded += SpawnCollectors;
        _collisionHandler.CollectorReturned += RemoveCollectors;
    }

    private void OnDisable()
    {
        _scanner.SuppliesFounded -= SpawnCollectors;
        _collisionHandler.CollectorReturned -= RemoveCollectors;
    }

    private void SpawnCollectors()
    {
        _collectorSpawner.StartSpawnCollectors();
    }

    private void RemoveCollectors(Collector collector)
    {
        _collectorSpawner.ReleaseCollector(collector);
        _supplyManager.SupplyDelivered(collector.TargetSupplyBox);
    }
}