using UnityEngine;

[System.Serializable]
public class WeaponVO
{
    public string Name;
    public int FireRatePerMinute;
    public bool Auto;
    public float RecoilAmount;
    public int MagCapacity;
    public float AccuracyPercentage;
    public float Damage;
    public float ReloadTime;
    public GameObject Bullet;
}
