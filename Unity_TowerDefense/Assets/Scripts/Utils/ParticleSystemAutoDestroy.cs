using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemAutoDestroy : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    
    public void Start() 
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
 
    public void Update() 
    {
        if(!_particleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
