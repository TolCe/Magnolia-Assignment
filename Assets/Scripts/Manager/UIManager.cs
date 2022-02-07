using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] LevelElements;
    [SerializeField] private GameObject[] SuccessElements;
    [SerializeField] private GameObject[] FailElements;

    [SerializeField] private Text MagText;
    [SerializeField] private Text WeaponNameText;
    [SerializeField] private Text LevelIndexText;
    [SerializeField] private Image EnemyHealthFillImage;
    [SerializeField] private Text ReloadingText;
    [SerializeField] private Image ReloadFillImage;

    private void Awake()
    {
        GameEvents.Instance.OnLevelLoaded += OnLevelLoaded;
        GameEvents.Instance.OnWeaponSelected += WriteWeaponName;
        GameEvents.Instance.OnLevelCompleted += OnLevelCompleted;
        GameEvents.Instance.OnMagModified += WriteMagSize;
        GameEvents.Instance.OnTargetDamaged += ShowEnemyHealth;
        GameEvents.Instance.OnMagReload += OnMagReload;
        GameEvents.Instance.OnMagReloading += OnMagReloading;
        GameEvents.Instance.OnMagReloaded += OnMagReloaded;
    }

    private void OnLevelLoaded(LevelVO levelVO, int levelIndex)
    {
        WriteLevelIndex(levelIndex);
        ArrangeUIForStart();
    }

    private void ArrangeUIForStart()
    {
        ReloadingText.gameObject.SetActive(false);
        ReloadFillImage.gameObject.SetActive(false);

        foreach (var item in LevelElements)
        {
            item.gameObject.SetActive(true);
        }
        foreach (var item in SuccessElements)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in FailElements)
        {
            item.gameObject.SetActive(false);
        }
    }

    private void OnMagReload()
    {
        ReloadingText.gameObject.SetActive(true);
        ReloadFillImage.gameObject.SetActive(true);
    }

    private void OnMagReloading(float reloadRatio)
    {
        ReloadingText.text = "Reloading" + string.Concat(Enumerable.Repeat(".", Mathf.CeilToInt(reloadRatio * 9) % 3 + 1));
        ReloadFillImage.fillAmount = reloadRatio;
    }

    private void OnMagReloaded()
    {
        ReloadingText.gameObject.SetActive(false);
        ReloadFillImage.gameObject.SetActive(false);
    }

    private void OnLevelCompleted()
    {
        foreach (var item in LevelElements)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in SuccessElements)
        {
            item.gameObject.SetActive(true);
        }
    }

    private void WriteWeaponName(bool isAuto, string name, int index)
    {
        ChangeText(WeaponNameText, name);
    }

    private void WriteMagSize(int magLeft, int magSize)
    {
        ChangeText(MagText, magLeft + "/" + magSize);
    }

    private void WriteLevelIndex(int levelIndex)
    {
        ChangeText(LevelIndexText, "Level " + (levelIndex + 1));
    }

    private void ChangeText(Text aText, string aMessage)
    {
        aText.text = aMessage;
    }

    private void ShowEnemyHealth(float health)
    {
        EnemyHealthFillImage.fillAmount = health * 0.01f;
    }
}
