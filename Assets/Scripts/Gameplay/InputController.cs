using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private bool _shouldGetInput;
    private bool _shouldAutoFire;
    private bool _canReload;
    private bool _canPressTrigger;
    private bool _reloadingWeapon;

    private void Awake()
    {
        GameEvents.Instance.OnLevelLoaded += OnLevelLoaded;
        GameEvents.Instance.OnLevelCompleted += OnLevelCompleted;
        GameEvents.Instance.OnWeaponSelected += OnWeaponSelected;
        GameEvents.Instance.OnWeaponFired += LockTrigger;
        GameEvents.Instance.OnMagEmpty += LockTrigger;
        GameEvents.Instance.OnWeaponCold += FreeTrigger;
        GameEvents.Instance.OnMagReloaded += OnReloaded;
    }

    private void LateUpdate()
    {
        if (_shouldGetInput)
        {
            if (!_reloadingWeapon)
            {
                if (_canPressTrigger)
                {
                    if (_shouldAutoFire)
                    {
                        if (Input.GetKey(KeyCode.D))
                        {
                            GameEvents.Instance.WeaponFired();
                        }
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.D))
                        {
                            GameEvents.Instance.WeaponFired();
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.R) && _canReload)
                {
                    Reloading();
                    GameEvents.Instance.MagReload();
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    GameEvents.Instance.WeaponSwitch();
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameEvents.Instance.TargetReset();
            }
        }
    }

    private void OnWeaponSelected(bool isAuto, string name, int index)
    {
        _canReload = false;
        _shouldAutoFire = isAuto;
    }

    private void LockTrigger()
    {
        _canReload = true;
        _canPressTrigger = false;
    }

    private void FreeTrigger()
    {
        _canPressTrigger = true;
    }

    private void Reloading()
    {
        _reloadingWeapon = true;
    }

    private void OnReloaded()
    {
        _reloadingWeapon = false;
        _canReload = false;
    }

    private void OnLevelLoaded(LevelVO arg1, int arg2)
    {
        _shouldGetInput = true;
    }

    private void OnLevelCompleted()
    {
        _shouldGetInput = false;
    }
}
