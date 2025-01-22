using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;
    
    private GameObject _cube;
    
    private ObjectPool<GameObject> _pool;
    
    public void ReleaseCube(GameObject cube)
        {
            _pool.Release(cube);
        }
    
    public void GetCubes(GameObject cube)
        {
            _cube = cube;
    
            _pool.Get();
        }
    
    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_cube),
            actionOnRelease: (cube) => cube.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }
    
    
}
