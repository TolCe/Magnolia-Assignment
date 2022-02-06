using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private ParticleSystem _particle;
    private string _poolName;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (!_particle.IsAlive(true))
        {
            PoolController.Instance.PutBackIntoPool(_poolName, gameObject);
        }
    }

    public void PlayParticle(string name, Vector3 direction)
    {
        _poolName = name;
        transform.LookAt(direction);
        _particle.Play();
    }
}
