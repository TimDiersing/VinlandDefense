using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{   

    public static void SaveFull(PersistantData data) {
        string path = Path.Combine(Application.persistentDataPath, "/full.mato");

        using (FileStream stream = new FileStream(path, FileMode.Create)) {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
        }
    }

    public static PersistantData LoadFull() {
        string path = Path.Combine(Application.persistentDataPath, "/full.mato");

        if (File.Exists(path)) {

            PersistantData data;
            using (FileStream stream = new FileStream(path, FileMode.Open)) {
                BinaryFormatter formatter = new BinaryFormatter();
                data = formatter.Deserialize(stream) as PersistantData;
            }

            return data;

        } else {
            Debug.Log("Save file not found " + path);
            return null;
        }
    }
    public static void SaveTurrets(TurretSaveData turretData) {
        string path = Path.Combine(Application.persistentDataPath, "/turrets.mato");

        using FileStream stream = new FileStream(path, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, turretData);
    }

    public static TurretSaveData LoadTurrets() {
        string path = Path.Combine(Application.persistentDataPath, "/turrets.mato");

        if (File.Exists(path)) {

            TurretSaveData turretData;
            using (FileStream stream = new FileStream(path, FileMode.Open)) {
                BinaryFormatter formatter = new BinaryFormatter();
                turretData = formatter.Deserialize(stream) as TurretSaveData;
            }

            return turretData;

        } else {
            Debug.Log("Save file not found " + path);
            return null;
        }
    }
}
