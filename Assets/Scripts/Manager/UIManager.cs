using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        GameEvents.Instance.OnLevelLoaded += WriteLevelIndex;
        GameEvents.Instance.OnWeaponSelected += WriteWeaponName;
        GameEvents.Instance.OnLevelCompleted += OnLevelCompleted;
        GameEvents.Instance.OnMagModified += WriteMagSize;
        GameEvents.Instance.OnTargetDamaged += ShowEnemyHealth;
    }
    private void Start()
    {
        OnLevelStart();
    }

    private void OnLevelStart()
    {
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

    private void WriteWeaponName(bool isAuto, string name)
    {
        ChangeText(WeaponNameText, name);
    }

    private void WriteMagSize(int magLeft, int magSize)
    {
        ChangeText(MagText, magLeft + "/" + magSize);
    }

    private void WriteLevelIndex(LevelVO levelVO, int levelIndex)
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
