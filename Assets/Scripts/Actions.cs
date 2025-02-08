using System;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public static Actions Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }


    public event Action<TurretItem> onTurretUnlocked;
    public event Action onGoldChanged;
    public event Action<int> onTurretRecycled;


    public void TurretUnlocked(TurretItem turret) {
        if (onTurretUnlocked != null) {
            onTurretUnlocked(turret);
        }
    }

    public void GoldChanged() {
        if (onGoldChanged != null) {
            onGoldChanged();
        }
    }
    public void TurretRecycled(int id) {
        if (onTurretRecycled != null) {
            onTurretRecycled(id);
        }
    }
}
