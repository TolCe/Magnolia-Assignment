using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    private LevelVO _currentLevelVO;
    [SerializeField] private Transform[] _spawnpoints;

    private void Awake()
    {
        GameEvents.Instance.OnLevelLoaded += OnLevelLoaded;
        GameEvents.Instance.OnPlayerGainedKill += OnPlayerGainedKill;
    }

    void Spawn(int totalKill)
    {
        Target target = PoolController.Instance.TakeFromPool(_currentLevelVO.TargetPrefab.name, _currentLevelVO.TargetPrefab, _spawnpoints[(totalKill + _currentLevelVO.StartSpawnIndex) % _spawnpoints.Length], Vector3.zero).GetComponent<Target>();
        target.SetProperties(_currentLevelVO.TargetColor, _currentLevelVO.TargetPrefab.name);
        GameEvents.Instance.NewEnemySpawned(target.transform);
    }

    private void OnLevelLoaded(LevelVO currentLevelVO, int levelIndex)
    {
        _currentLevelVO = currentLevelVO;
        Spawn(0);
    }

    private void OnPlayerGainedKill(int totalKill)
    {
        if (totalKill == 5)
        {
            GameEvents.Instance.LevelCompleted();
        }
        else
        {
            Spawn(totalKill);
        }
    }
}
