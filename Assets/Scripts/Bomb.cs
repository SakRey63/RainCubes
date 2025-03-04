using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Exploder _exploder;
     
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
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
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
        
        _exploder.Explode();
        
        Exploded?.Invoke(this);
    }

    public void ReturnColorAlfa()
    {
        _renderer.material.color = new Color(0, 0, 0, _maxValue);
    }

    private int GetRandomSecond()
    { 
        return Random.Range(_minSecond, _maxSecond);
    }
}