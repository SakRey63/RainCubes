using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _minXPosition = -17.4f;
    [SerializeField] private float _maxXPosition = 6.4f;
    [SerializeField] private float _yPosition = 17.71f;
    [SerializeField] private float _minZPosition = 4.3f;
    [SerializeField] private float _maxZPosition = 13.3f;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private Material _material;
    [SerializeField] private float _quaternionZero = 0f;
    [SerializeField] private Pool _pool;
    [SerializeField] private GameObject _cube;
    
    private void Start()
    {
        InvokeRepeating(nameof(OnGet), 0, _repeatRate);
    }
    private void OnEnable()
    {
        Cube.EndTime += CubeOnEndTime;
    }

    private void OnDisable()
    {
        Cube.EndTime -= CubeOnEndTime;
    }

    private void OnGet()
    {
        GameObject cube = ActionOnGet(_cube);
        
        _pool.GetCubes(cube);
    }

    private void CubeOnEndTime(GameObject obj)
    {
        Debug.Log("я упал на платформу");
        
        _pool.ReleaseCube(obj);
    }
    
    private GameObject ActionOnGet(GameObject obj)
    {
        obj.transform.position = new Vector3(Random.Range(_minXPosition, _maxXPosition), _yPosition, Random.Range(_minZPosition, _maxZPosition));
        obj.transform.rotation = new Quaternion(_quaternionZero, _quaternionZero, _quaternionZero, _quaternionZero);
        obj.SetActive(true);
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.GetComponent<Renderer>().material = _material;

        return obj;
    } 
}
