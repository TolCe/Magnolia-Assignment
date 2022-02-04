using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponContainer[] _weapons;

    [SerializeField] private int _currentWeaponIndex;
    [SerializeField] private WeaponContainer _currentWeaponContainer;

    private int _bulletLeft;

    private void Awake()
    {
        GameEvents.Instance.OnWeaponFired += Shoot;
        GameEvents.Instance.OnMagReload += Reload;
        GameEvents.Instance.OnWeaponSwitch += ChangeWeapon;
    }
    private void Start()
    {
        GameEvents.Instance.WeaponCold();
        SelectWeapon(0);
    }

    private void ChangeWeapon()
    {
        if (_currentWeaponIndex < _weapons.Length - 1)
        {
            _currentWeaponIndex++;
        }
        else
        {
            _currentWeaponIndex = 0;
        }

        SelectWeapon(_currentWeaponIndex);

        Debug.Log("Switching to " + _currentWeaponContainer.Data.Name);
    }

    private void SelectWeapon(int index)
    {
        _currentWeaponContainer = _weapons[index];
        _bulletLeft = _currentWeaponContainer.Data.MagCapacity;
        GameEvents.Instance.WeaponSelected(_currentWeaponContainer.Data.Auto, _currentWeaponContainer.Data.Name);
        ModifyMag();
    }

    private void Shoot()
    {
        if (_bulletLeft > 0)
        {
            Debug.Log("Weapon Fired");

            _bulletLeft--;
            Vector3 bulletDirection = transform.forward;
            float randomValue = Random.Range(0, 100);
            if (randomValue < 100f - _currentWeaponContainer.Data.AccuracyPercentage)
            {
                Debug.Log("Missed");

                bulletDirection += 0.25f * (transform.right + transform.up);
            }

            BulletPool.Instance.TakeFromPool(transform.position, bulletDirection, _currentWeaponContainer.Data.Damage);
            ModifyMag();
            StartCoroutine(Cooldown());
        }
    }

    private void Reload()
    {
        if (_bulletLeft != _currentWeaponContainer.Data.MagCapacity)
        {
            StartCoroutine(ReloadRoutine());
        }
    }

    private IEnumerator ReloadRoutine()
    {
        float timer = 0;
        while (timer < _currentWeaponContainer.Data.ReloadTime)
        {
            if (timer % 1f < Time.fixedDeltaTime)
            {
                Debug.Log("Reloading in " + Mathf.CeilToInt(_currentWeaponContainer.Data.ReloadTime - timer) + " seconds");
            }

            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Debug.Log("Mag has been reloaded");

        _bulletLeft = _currentWeaponContainer.Data.MagCapacity;
        GameEvents.Instance.WeaponCold();
        GameEvents.Instance.MagReloaded();
        ModifyMag();
    }

    private IEnumerator Cooldown()
    {
        if (CheckMag())
        {
            yield return new WaitForSeconds(60f / _currentWeaponContainer.Data.FireRatePerMinute);
            GameEvents.Instance.WeaponCold();
        }
    }

    private void ModifyMag()
    {
        GameEvents.Instance.MagModified(_bulletLeft, _currentWeaponContainer.Data.MagCapacity);
    }

    private bool CheckMag()
    {
        if (_bulletLeft == 0)
        {
            Debug.Log("Mag is empty");
            GameEvents.Instance.MagEmpty();

            return false;
        }

        return true;
    }
}
