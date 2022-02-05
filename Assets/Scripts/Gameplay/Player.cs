using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _totalKilled;

    private void Awake()
    {
        GameEvents.Instance.OnLevelLoaded += OnLevelLoaded;
        GameEvents.Instance.OnTargetDestroyed += KillTarget;
    }

    private void KillTarget()
    {
        _totalKilled++;
        GameEvents.Instance.PlayerGainedKill(_totalKilled);
    }

    private void ResetPlayer()
    {
        _totalKilled = 0;
    }

    private void OnLevelLoaded(LevelVO arg1, int arg2)
    {
        ResetPlayer();
    }
}
