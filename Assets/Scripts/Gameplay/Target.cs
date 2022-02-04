using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float _health;
    private bool _alive;
    [SerializeField] private MeshRenderer _meshRend;

    private void Awake()
    {
        _alive = true;
    }
    private void OnEnable()
    {
        ChangeHealth(0);
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
        ResetTarget();
        TargetPool.Instance.PutBackIntoPool(gameObject);
        GameEvents.Instance.TargetDestroyed();
    }

    public void SetColor(Color color)
    {
        Color targetColor = color;
        targetColor.a = 1;
        _meshRend.material.color = targetColor;
    }

    public void ResetTarget()
    {
        _alive = true;
        _health = 100;
    }
}
