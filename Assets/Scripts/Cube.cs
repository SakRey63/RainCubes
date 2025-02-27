using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _delay;
    
    private Color _color;
    private Renderer _renderer;
    private int _minSecond = 2;
    private int _maxSecond = 6;
    private bool _collisionPlatform;

    public event Action<Cube> CollisionEnter;
    
    public Rigidbody Rigidbody { get; private set; }
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        
        if (TryGetComponent(out Rigidbody rigidbody))
        {
            Rigidbody = rigidbody;
        }
    }

    private IEnumerator ReleaseActivation()
    {
        _delay = GetRandomSecond();
        
        yield return new WaitForSeconds(_delay);
        
        CollisionEnter?.Invoke(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Platform platform) && _collisionPlatform)
        { 
            _collisionPlatform = false;
            
            _color = platform.NewColor;
                        
            ApplyNewColor(_color);
                        
            StartCoroutine(ReleaseActivation());
        }
    }
    
    public void CollusionPlatform(Material material)
    {
        _collisionPlatform = true;
        _renderer.material = material;
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