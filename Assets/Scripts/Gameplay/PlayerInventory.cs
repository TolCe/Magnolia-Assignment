using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [HideInInspector] public GameObject weapon;
    [SerializeField] string weaponName;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //SelectWeapon(PlayerPrefs.GetString("WEAPON", "Player_Sniper"));
        SelectWeapon(weaponName);
    }

    public void SelectWeapon(string weaponName)
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
        }

        string selectedWeapon = weaponName;
        PlayerPrefs.SetString("WEAPON", selectedWeapon);
        SetWeapon(selectedWeapon);
    }

    void SetWeapon(string selectedWeapon)
    {
        weapon = Instantiate(Resources.Load(selectedWeapon) as GameObject, transform);
    }
}
