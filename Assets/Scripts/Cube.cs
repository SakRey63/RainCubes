using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    private Color _color;
    private Renderer _renderer;
    private int _minSecond = 2;
    private int _maxSecond = 6;
    private bool _collisionPlatform = true;

    public event Action<Cube> CollisionEnter;

    public void CollusionPlatform(Material material)
    {
        _collisionPlatform = true;
        _renderer.material = material;
    }
    
    private void Awake()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        _renderer = GetComponent<Renderer>();
    }

    private IEnumerator ReleaseActivation()
    {
        yield return new WaitForSecondsRealtime(GetRandomSecond());
        
        CollisionEnter?.Invoke(this);
        
        Debug.Log("Время кончилось");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_collisionPlatform)
        {
            if (other.gameObject.GetComponent<Platform>())
            {
                _collisionPlatform = false;
            
                _color = other.gameObject.GetComponent<Platform>().NewColor;
                        
                ApplyNewColor(_color);
                        
                StartCoroutine(ReleaseActivation());
            }
        }
    }

    private void ApplyNewColor(Color color)
    {
        _renderer.material.color = color;
    }
    
    private int GetRandomSecond()
    { 
        return Random.Range(_minSecond, _maxSecond);
    }
}
