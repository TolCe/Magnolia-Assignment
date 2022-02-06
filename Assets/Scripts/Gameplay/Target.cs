using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour, IPoolObject
{
    [SerializeField] private TargetContainer _targetContainer;
    private float _health;
    private bool _alive;
    private string _poolName;
    [SerializeField] private MeshRenderer[] _meshRends;

    private void Awake()
    {
        GameEvents.Instance.OnLevelCompleted += OnLevelCompleted;

        _alive = true;
    }
    private void OnEnable()
    {
        SetTargetProperties();
        ChangeHealth(0);
    }
    private void OnDisable()
    {
        ResetObject();
    }

    public void GetHit(float damage, Vector3 hitPoint, Vector3 direction)
    {
        ParticleController hitParticle = PoolController.Instance.TakeFromPool(_targetContainer.Data.HitParticle.name, _targetContainer.Data.HitParticle, null, hitPoint).GetComponent<ParticleController>();
        hitParticle.PlayParticle(_targetContainer.Data.HitParticle.name, direction);
        ChangeHealth(-damage);
    }

    private void ChangeHealth(float change)
    {
        if (_alive)
        {
            _health += change;

            if (_health > 100)
            {
                _health = 100;
            }
            else if (_health <= 0)
            {
                _health = 0;
            }

            GameEvents.Instance.TargetDamaged(_health);

            if (!CheckIfAlive())
            {
                Die();
            }
        }
    }

    public bool CheckIfAlive()
    {
        if (_health <= 0)
        {
            return false;
        }

        return true;
    }

    public void Die()
    {
        Debug.Log("A target was destroyed");

        _alive = false;
        StartCoroutine(DieActions());
    }

    public void SetTargetProperties()
    {
        _health = _targetContainer.Data.Health;
    }

    private IEnumerator DieActions()
    {
        while (transform.position.y > -3f)
        {
            transform.position += 2 * Time.fixedDeltaTime * Vector3.down;
            yield return new WaitForFixedUpdate();
        }

        PoolController.Instance.PutBackIntoPool(_poolName, gameObject);
        GameEvents.Instance.TargetDestroyed();
    }

    public void SetProperties(Color color, string name)
    {
        _poolName = name;

        Color targetColor = color;
        targetColor.a = 1;
        foreach (var item in _meshRends)
        {
            item.material.color = targetColor;
        }
    }

    public void ResetObject()
    {
        _alive = true;
        _health = 100;
    }

    public void OnLevelCompleted()
    {
        ResetObject();
    }
}
