using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOverPanel : MonoBehaviour
{
    public void ReturnToMainMenu() {
        SceneManager.LoadScene(0);
    }
}
