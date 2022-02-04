using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _range;
    private float _damage;
    private Vector3 _initPos;
    private Collider _collider;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }
    private void Start()
    {
        _initPos = transform.position;
    }
    private void Update()
    {
        _rb.velocity = _speed * transform.forward;

        if (Vector3.Magnitude(_initPos - transform.position) > _range)
        {
            GameEvents.Instance.BulletDisposed(this);
        }
    }

    public void Ignite(Vector3 direction, float damage)
    {
        _damage = damage;
        _collider.enabled = true;
        transform.LookAt(direction + transform.position);
        //rb.AddForce(speed * transform.forward, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Debug.Log("A target was hit");

            Target target = other.GetComponentInParent<Target>();
            target.ChangeHealth(-_damage);
            GameEvents.Instance.BulletHit(_damage);
        }

        GameEvents.Instance.BulletDisposed(this);
    }

    public void ResetBullet()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _collider.enabled = false;
    }
}
