using UnityEngine;

public class Target : MonoBehaviour, IPoolObject
{
    [SerializeField] private TargetContainer _targetContainer;
    private float _health;
    private bool _alive;
    [SerializeField] private MeshRenderer _meshRend;

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

    public void ChangeHealth(float change)
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
        PoolController.Instance.PutBackIntoPool("Target", gameObject);
        GameEvents.Instance.TargetDestroyed();
    }

    public void SetTargetProperties()
    {
        _health = _targetContainer.Data.Health;
    }

    public void SetColor(Color color)
    {
        Color targetColor = color;
        targetColor.a = 1;
        _meshRend.material.color = targetColor;
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
