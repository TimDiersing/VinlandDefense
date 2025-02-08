using UnityEngine;
public enum Skill {
    AddDamage,
    MultDamage,
    GoldInc,
}

[CreateAssetMenu(fileName = "data", menuName = "ScriptableObjects/SubskillScriptableObject", order = 1), System.Serializable]
public class Subskill : ScriptableObject
{
    public Skill skill;
    public string title;
    public float skillValue;
}