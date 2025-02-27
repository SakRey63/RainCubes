using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapaciti = 15;
    [SerializeField] private int _poolMaxSize = 30;
    
    private int _amountAllTime = 0;

    private ObjectPool<T> _pool;

    public event Action<PoolInfo> PoolChanged;

    protected void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => GetAction(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapaciti,
            maxSize: _poolMaxSize);
    }
    
    protected virtual void GetAction(T obj)
    {
        obj.gameObject.SetActive(true);

        _amountAllTime++;
        
        PoolChanged?.Invoke(new PoolInfo(_amountAllTime, _pool.CountAll, _pool.CountActive));
    }
    
    protected virtual void ReturnInPool(T obj)
    { 
        PoolChanged?.Invoke(new PoolInfo(_amountAllTime, _pool.CountAll, _pool.CountActive));
    }

    protected void GetGameObject()
    {
        _pool.Get();
    }

    protected void Release(T obj)
    {
        _pool.Release(obj);
    }
}