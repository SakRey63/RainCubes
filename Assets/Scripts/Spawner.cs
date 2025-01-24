using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _minXPosition = -17.4f;
    [SerializeField] private float _maxXPosition = 6.4f;
    [SerializeField] private float _yPosition = 17.71f;
    [SerializeField] private float _minZPosition = 4.3f;
    [SerializeField] private float _maxZPosition = 13.3f;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private Material _material;
    [SerializeField] private Cube _cube;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;
    
    private ObjectPool<Cube> _pool;
    
    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cube),
            actionOnGet: (cube) => GetAction(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }
    
    private void Start()
    {
        StartCoroutine(SpawnerCube());
    }

    private IEnumerator SpawnerCube()
    {
        WaitForSeconds wait = new(_repeatRate);
        
        while (true)
        {
            SpawnCube();
            
            yield return wait;
        }
    }
    
    private void GetAction(Cube cube)
    {
        cube.CollusionPlatform(_material);
        
        cube.CollisionEnter += CubeRelease;
    }
    
    private void CubeRelease(Cube cube)
    {
        cube.CollisionEnter -= CubeRelease;
        
        _pool.Release(cube);
    }
    
    private void SpawnCube()
    {
        Cube cube = _pool.Get();
        
        cube.transform.position = new Vector3(Random.Range(_minXPosition, _maxXPosition), _yPosition, Random.Range(_minZPosition, _maxZPosition));
        cube.gameObject.SetActive(true);
    } 
}
