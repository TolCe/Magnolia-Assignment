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
        Target target = PoolController.Instance.TakeFromPool("Target", _currentLevelVO.TargetPrefab, _spawnpoints[totalKill % _spawnpoints.Length], Vector3.zero).GetComponent<Target>();
        target.SetColor(_currentLevelVO.TargetColor);
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
