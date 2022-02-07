using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        GameEvents.Instance.OnLevelLoaded += OnLevelLoaded;
        GameEvents.Instance.OnMagReload += OnMagReload;
        GameEvents.Instance.OnWeaponFired += OnWeaponFired;
        GameEvents.Instance.OnWeaponSwitch += OnWeaponSwitch;
        GameEvents.Instance.OnWeaponSelected += OnWeaponSelected;
    }

    private void OnLevelLoaded(LevelVO arg1, int arg2)
    {
        SetTrigger("Draw");
    }

    private void OnWeaponSwitch()
    {
        SetTrigger("Holster");
    }

    private void OnWeaponSelected(bool arg1, string arg2, int index)
    {
        SetTrigger("Draw");
        SetInt("WeaponIndex", index);
    }

    private void OnWeaponFired()
    {
        SetTrigger("Fire");
    }

    private void OnMagReload()
    {
        SetTrigger("Reload");
    }

    private void SetTrigger(string state)
    {
        _animator.SetTrigger(state);
    }
    private void SetInt(string state, int index)
    {
        _animator.SetInteger(state, index);
    }
}
