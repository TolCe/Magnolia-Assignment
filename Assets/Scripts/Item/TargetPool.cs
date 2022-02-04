using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPool : MonoBehaviour
{
    private static TargetPool _instance = null;
    public static TargetPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (TargetPool)FindObjectOfType(typeof(TargetPool));
            }

            return _instance;
        }
    }

    private List<GameObject> _pool = new List<GameObject>();

    private void Awake()
    {
        GameEvents.Instance.OnLevelCompleted += ResetPool;
    }

    public Transform TakeFromPool(GameObject targetPrefab, Transform spawnPoint)
    {
        GameObject targetObj = null;

        if (_pool.Count == 0)
        {
            targetObj = Instantiate(targetPrefab);
        }
        else
        {
            targetObj = _pool[0];
            _pool.RemoveAt(0);
            targetObj.SetActive(true);
        }

        targetObj.transform.SetParent(spawnPoint);
        targetObj.transform.localPosition = Vector3.zero;

        return targetObj.transform;
    }

    public void PutBackIntoPool(GameObject target)
    {
        _pool.Add(target);
        target.SetActive(false);
    }

    public void ResetPool()
    {
        _pool = new List<GameObject>();
    }
}
