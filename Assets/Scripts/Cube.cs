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
    private Coroutine _releaseActivation;
    private bool onCollisionPlatform = true;

    public static event Action<GameObject> EndTime;
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private IEnumerator ReleaseActivation()
    {
        var wait = new WaitForSecondsRealtime(GetRandomSecond());
        
        yield return wait;
        
        EndTime?.Invoke(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Platform>() && onCollisionPlatform)
        {
            onCollisionPlatform = false;

            _color = other.gameObject.GetComponent<Platform>().NewColor;
            
            ApplyNewColor(_color);
            
            _releaseActivation = StartCoroutine(ReleaseActivation());
        }
    }

    private void ApplyNewColor(Color color)
    {
        _renderer.material.color = color;
    }
    
    private int GetRandomSecond()
    {
        int second = Random.Range(_minSecond, _maxSecond);

        return second;
    }
}
