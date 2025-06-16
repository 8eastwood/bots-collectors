using UnityEngine;
using UnityEngine.Pool;

public class PoolHandler<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefabObject;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    protected ObjectPool<T> _pool;
    
    public int PoolMaxSize => _poolMaxSize;

    private void Awake()
    {
        _pool = new ObjectPool<T>
        (createFunc: () => Instantiate(_prefabObject),
            actionOnGet: GetFromPool,
            actionOnRelease: ReleaseInPool,
            actionOnDestroy: Destroy,
            collectionCheck: true,
            _poolCapacity,
            _poolMaxSize
        );
    }

    private void GetFromPool(T prefabObject)
    {
        prefabObject.gameObject.SetActive(true);
    }

    private void ReleaseInPool(T prefabObject)
    {
        prefabObject.gameObject.SetActive(false);
    }
}