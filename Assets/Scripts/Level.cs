using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "data", menuName = "ScriptableObjects/LevelScriptableObject", order = 1)]
public class Level : ScriptableObject
{
    public int levelNumber;
    public List<Wave> waves;
}
