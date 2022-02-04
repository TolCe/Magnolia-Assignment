using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool _instance = null;
    public static BulletPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (BulletPool)FindObjectOfType(typeof(BulletPool));
            }

            return _instance;
        }
    }

    [SerializeField] private GameObject _bulletPrefab;
    private List<GameObject> _pool = new List<GameObject>();

    private void Awake()
    {
        GameEvents.Instance.OnBulletDisposed += PutBackIntoPool;
        GameEvents.Instance.OnLevelCompleted += ResetPool;
    }

    public Bullet TakeFromPool(Vector3 spawnPoint, Vector3 direction, float damage)
    {
        GameObject bulletObj = null;

        if (_pool.Count == 0)
        {
            bulletObj = Instantiate(_bulletPrefab);
        }
        else
        {
            bulletObj = _pool[0];
            _pool.RemoveAt(0);
            bulletObj.SetActive(true);
        }

        bulletObj.transform.position = spawnPoint;
        bulletObj.GetComponent<Bullet>().Ignite(direction, damage);

        return bulletObj.GetComponent<Bullet>();
    }

    public void PutBackIntoPool(Bullet objToPut)
    {
        objToPut.ResetBullet();
        _pool.Add(objToPut.gameObject);
        objToPut.gameObject.SetActive(false);
    }

    public void ResetPool()
    {
        _pool = new List<GameObject>();
    }
}
