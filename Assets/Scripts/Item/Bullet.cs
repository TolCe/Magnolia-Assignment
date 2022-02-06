using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject
{
    private float _speed;
    private float _range;
    private float _damage;
    private Vector3 _initPos;
    private Collider _collider;
    private Rigidbody _rb;

    private void Awake()
    {
        GameEvents.Instance.OnLevelCompleted += OnLevelCompleted;

        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        _initPos = transform.position;
    }

    public void Ignite(Vector3 direction, float damage, float speed, float range)
    {
        _speed = speed;
        _range = range;
        _damage = damage;
        _collider.enabled = true;
        transform.LookAt(direction + transform.position);
        StartCoroutine(BulletTravel());
    }

    private IEnumerator BulletTravel()
    {
        while (Vector3.Magnitude(_initPos - transform.position) < _range)
        {
            if (!_collider.enabled)
            {
                yield break;
            }

            _rb.velocity = _speed * transform.forward;
            yield return new WaitForFixedUpdate();
        }

        ResetObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Debug.Log("A target was hit");

            Target target = other.GetComponentInParent<Target>();
            target.GetHit(_damage, transform.position, _initPos - transform.position);
            GameEvents.Instance.BulletHit(_damage);
        }

        ResetObject();
    }

    public void ResetObject()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _collider.enabled = false;

        GameEvents.Instance.BulletDisposed("Bullet", gameObject);
    }

    public void OnLevelCompleted()
    {
        ResetObject();
    }
}
