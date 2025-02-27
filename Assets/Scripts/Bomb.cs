using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _forseExplosions = 300;
    [SerializeField] private float _radiusExplosions = 50;
     
    private int _minSecond = 2;
    private int _maxSecond = 6;
    private float _maxValue = 1;
    private Renderer _renderer;

    public event Action<Bomb> Exploded;
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(ChangeTransparency());
    }

    private IEnumerator ChangeTransparency()
    {
        int delay = GetRandomSecond();
        
        float epsilonTime = 0;
        float channelAMax = 1f;
        float channelA = 1f;

        while (epsilonTime < delay)
        {
            epsilonTime += Time.deltaTime;

            _renderer.material.color = new Color(0, 0, 0, channelA -= channelAMax * Time.deltaTime / delay);

            yield return null;
        }
        
        Explode();
    }

    public void ReturnColorAlfa()
    {
        _renderer.material.color = new Color(0, 0, 0, _maxValue);
    }

    private void Explode()
    {
        Collider[] cubeshits = Physics.OverlapSphere(transform.position, _radiusExplosions);

        List<Rigidbody> cubes = new();

        foreach (var hit in cubeshits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        foreach (var cube in cubes)
        {
            cube.AddExplosionForce(_forseExplosions, transform.position, _radiusExplosions);
        }
        
        Exploded?.Invoke(this);
    }

    private int GetRandomSecond()
    { 
        return Random.Range(_minSecond, _maxSecond);
    }
}