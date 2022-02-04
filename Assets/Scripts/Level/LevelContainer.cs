using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelContainer", menuName = "New Level Container")]
public class LevelContainer : ScriptableObject
{
    public List<LevelVO> Levels;
}
