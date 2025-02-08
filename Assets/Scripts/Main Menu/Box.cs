using UnityEngine;

[CreateAssetMenu(fileName = "data", menuName = "ScriptableObjects/BoxScriptableObject", order = 1)]
public class Box : ScriptableObject
{
    public string boxName;
    [Range(0f, 1f)]
    public float rareOdds;

    [Range(0f, 1f)]
    public float epicOdds;

    [Range(0f, 1f)]
    public float legnerdayOdds;
    public int items;
    public int cost;
}
