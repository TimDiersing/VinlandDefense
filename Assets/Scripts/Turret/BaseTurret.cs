using UnityEngine;

public enum Rarity {
    Common,
    Rare,
    Epic,
    Legendary
}
/*
    A turrets base scriptable object that defines what the turrets base stats and prefab are.
*/
[CreateAssetMenu(fileName = "data", menuName = "ScriptableObjects/TurretScriptableObject", order = 1)]
public class BaseTurret : ScriptableObject
{
    public GameObject turretPF;
    public string title;
    public int index;
    public Rarity rarity;
    public Sprite UIImage;
    public int cost;
    public int amount;
    public float dmgPerLevel;
    public int recycleAmount;
}
