using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    private static GameEvents _instance = null;
    public static GameEvents Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (GameEvents)FindObjectOfType(typeof(GameEvents));
            }

            return _instance;
        }
    }

    public event Action<Transform> OnNewEnemySpawned;
    public void NewEnemySpawned(Transform target)
    {
        if (OnNewEnemySpawned != null)
        {
            OnNewEnemySpawned(target);
        }
    }

    public event Action OnWeaponFired;
    public void WeaponFired()
    {
        if (OnWeaponFired != null)
        {
            OnWeaponFired();
        }
    }

    public event Action<Quaternion, float> OnWeaponRecoiled;
    public void WeaponRecoiled(Quaternion targetRotation, float time)
    {
        if (OnWeaponRecoiled != null)
        {
            OnWeaponRecoiled(targetRotation, time);
        }
    }

    public event Action OnWeaponCold;
    public void WeaponCold()
    {
        if (OnWeaponCold != null)
        {
            OnWeaponCold();
        }
    }

    public event Action<int, int> OnMagModified;
    public void MagModified(int magLeft, int magSize)
    {
        if (OnMagModified != null)
        {
            OnMagModified(magLeft, magSize);
        }
    }

    public event Action OnMagEmpty;
    public void MagEmpty()
    {
        if (OnMagEmpty != null)
        {
            OnMagEmpty();
        }
    }

    public event Action OnMagReload;
    public void MagReload()
    {
        if (OnMagReload != null)
        {
            OnMagReload();
        }
    }

    public event Action<float> OnMagReloading;
    public void MagReloading(float reloadPercentage)
    {
        if (OnMagReloading != null)
        {
            OnMagReloading(reloadPercentage);
        }
    }

    public event Action OnMagReloaded;
    public void MagReloaded()
    {
        if (OnMagReloaded != null)
        {
            OnMagReloaded();
        }
    }

    public event Action<string, GameObject> OnBulletDisposed;
    public void BulletDisposed(string name, GameObject obj)
    {
        if (OnBulletDisposed != null)
        {
            OnBulletDisposed(name, obj);
        }
    }

    public event Action OnWeaponSwitch;
    public void WeaponSwitch()
    {
        if (OnWeaponSwitch != null)
        {
            OnWeaponSwitch();
        }
    }

    public event Action<bool, string, int> OnWeaponSelected;
    public void WeaponSelected(bool isAuto, string name, int index)
    {
        if (OnWeaponSelected != null)
        {
            OnWeaponSelected(isAuto, name, index);
        }
    }

    public event Action<float> OnBulletHit;
    public void BulletHit(float damage)
    {
        if (OnBulletHit != null)
        {
            OnBulletHit(damage);
        }
    }

    public event Action<float> OnTargetDamaged;
    public void TargetDamaged(float healthLeft)
    {
        if (OnTargetDamaged != null)
        {
            OnTargetDamaged(healthLeft);
        }
    }

    public event Action OnTargetReset;
    public void TargetReset()
    {
        if (OnTargetReset != null)
        {
            OnTargetReset();
        }
    }

    public event Action OnTargetDestroyed;
    public void TargetDestroyed()
    {
        if (OnTargetDestroyed != null)
        {
            OnTargetDestroyed();
        }
    }

    public event Action<int> OnPlayerGainedKill;
    public void PlayerGainedKill(int totalKill)
    {
        if (OnPlayerGainedKill != null)
        {
            OnPlayerGainedKill(totalKill);
        }
    }

    public event Action<LevelVO, int> OnLevelLoaded;
    public void LevelLoaded(LevelVO currentLevelVO, int levelIndex)
    {
        if (OnLevelLoaded != null)
        {
            OnLevelLoaded(currentLevelVO, levelIndex);
        }
    }

    public event Action OnLevelCompleted;
    public void LevelCompleted()
    {
        if (OnLevelCompleted != null)
        {
            OnLevelCompleted();
        }
    }
}
