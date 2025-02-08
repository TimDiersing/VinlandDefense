using UnityEngine;

/*
    Turret parent class for defineing any created turret prefab
*/
public class Turret : MonoBehaviour {
    public float damage;
    public float turnSpeed;
    public float fireRate;
     
    // 0 first, 1 closest, 2 farthest, 3 strongest
    protected int searchType;
    private void Start() {
        searchType = 0;
    }
    public void IncreaseDmg(float amt) {
        damage += amt;
    }

    public void MultDmg(float amt) {
        damage *= amt;
    }
    public void SetSearchType(int searchType) {
        this.searchType = searchType;
    }
    public int GetSearchType() {
        return searchType;
    }
}

