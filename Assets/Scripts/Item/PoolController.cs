using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    private static PoolController _instance = null;
    public static PoolController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (PoolController)FindObjectOfType(typeof(PoolController));
            }

            return _instance;
        }
    }

    private Dictionary<string, List<GameObject>> _pool;

    private void Awake()
    {
        ResetPool();

        GameEvents.Instance.OnLevelLoaded += OnLevelLoaded;
        GameEvents.Instance.OnBulletDisposed += PutBackIntoPool;
    }

    public GameObject TakeFromPool(string objName, GameObject prefab, Transform spawnParent, Vector3 spawnPoint)
    {
        GameObject obj = null;

        if (!_pool.ContainsKey(objName))
        {
            _pool.Add(objName, new List<GameObject>());
        }

        if (_pool[objName].Count == 0)
        {
            obj = Instantiate(prefab);
        }
        else
        {
            obj = _pool[objName][0];
            _pool[objName].RemoveAt(0);
            obj.SetActive(true);
        }

        obj.transform.SetParent(spawnParent);
        obj.transform.localPosition = spawnPoint;

        return obj;
    }

    public void PutBackIntoPool(string objName, GameObject objToPut)
    {
        _pool[objName].Add(objToPut.gameObject);
        objToPut.SetActive(false);
    }

    public void ResetPool()
    {
        _pool = new Dictionary<string, List<GameObject>>();
    }

    private void OnLevelLoaded(LevelVO levelVO, int index)
    {

    }
}
