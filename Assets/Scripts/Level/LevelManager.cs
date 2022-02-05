using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelContainer _levelContainer;

    private int _levelIndexClamped;
    private int _currentlevelIndex;

    private void Awake()
    {
        _levelIndexClamped = PlayerPrefs.GetInt("LEVELINDEX", 0);
        _currentlevelIndex = PlayerPrefs.GetInt("CURRENTLEVELINDEX", 0);
    }
    private void Start()
    {
        LoadLevel();
    }

    public void NextLevel()
    {
        _currentlevelIndex++;
        if (_currentlevelIndex >= _levelContainer.Levels.Count)
        {
            int _randLevel = Random.Range(0, _levelContainer.Levels.Count);
            _levelIndexClamped = _randLevel;
        }
        else
        {
            _levelIndexClamped = _currentlevelIndex;
        }

        PlayerPrefs.SetInt("CURRENTLEVELINDEX", _currentlevelIndex);
        PlayerPrefs.SetInt("LEVELINDEX", _levelIndexClamped);
        LoadLevel();
    }

    public void LoadLevel()
    {
        GameEvents.Instance.LevelLoaded(_levelContainer.Levels[_levelIndexClamped], _currentlevelIndex);
    }
}
