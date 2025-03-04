using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _forseExplosions = 300;
    [SerializeField] private float _radiusExplosions = 50;
    
    public void Explode()
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
    }
}