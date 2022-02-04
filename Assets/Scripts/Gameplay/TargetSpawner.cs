using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    private LevelVO _currentLevelVO;
    [SerializeField] private Transform[] _spawnpoints;
    private int _totalKilled;

    private void Awake()
    {
        GameEvents.Instance.OnTargetDestroyed += KillTarget;
        GameEvents.Instance.OnLevelLoaded += OnLevelLoaded;
    }

    void Spawn()
    {
        Transform target = TargetPool.Instance.TakeFromPool(_currentLevelVO.TargetPrefab, _spawnpoints[_totalKilled % _spawnpoints.Length]);
        target.GetComponent<Target>().SetColor(_currentLevelVO.TargetColor);
        GameEvents.Instance.NewEnemySpawned(target);
    }

    private void KillTarget()
    {
        _totalKilled++;
        if (_totalKilled == 5)
        {
            GameEvents.Instance.LevelCompleted();
        }
        else
        {
            Spawn();
        }
    }

    private void OnLevelLoaded(LevelVO currentLevelVO, int levelIndex)
    {
        _currentLevelVO = currentLevelVO;
        Spawn();
    }
}
