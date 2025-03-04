using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private float _minXPosition = -17.4f;
    [SerializeField] private float _maxXPosition = 6.4f;
    [SerializeField] private float _yPosition = 17.71f;
    [SerializeField] private float _minZPosition = 4.3f;
    [SerializeField] private float _maxZPosition = 13.3f;
    [SerializeField] private float _repeatRate = 0.5f;
    [SerializeField] private Material _material;

    private Transform _startPosition;

    public event Action<Cube> Falled;

    private void Start()
    {
        StartCoroutine(RepeatGetCube(_repeatRate));
    }

    private IEnumerator RepeatGetCube(float delay)
    {
        WaitForSeconds timeRepeat = new WaitForSeconds(delay);
        
        while (enabled)
        {
            GetGameObject();
            
            yield return timeRepeat;
        }
    }

    protected override void GetAction(Cube cube)
    {
        _startPosition = cube.transform;
        
        CreateRandomStartPosition(cube);
        
        cube.CollusionPlatform(_material);

        cube.AddComponent<ColorChanger>();
        
        base.GetAction(cube);

        cube.CollisionEnter += ReturnInPool;
    }

    protected override void ReturnInPool(Cube cube)
    {
        cube.CollisionEnter -= ReturnInPool;
        
        Falled?.Invoke(cube);

        ResetStatus(cube);
        
        Release(cube);
        
        base.ReturnInPool(cube);
    }

    private void CreateRandomStartPosition(Cube cube)
    {
        cube.transform.position = new Vector3(UnityEngine.Random.Range(_minXPosition, _maxXPosition), _yPosition,
            UnityEngine.Random.Range(_minZPosition, _maxZPosition));
    }
    
    private void ResetStatus(Cube cube)
    {
        cube.transform.SetPositionAndRotation(_startPosition.position, _startPosition.rotation);

        cube.Rigidbody.velocity = Vector3.zero;
        cube.Rigidbody.angularVelocity = Vector3.zero;
    }
}