using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int buttonClickIndex;
    private AudioManager am;

    public void Awake() {
        am = AudioManager.Instance;
    }
    public void StartLevel(int level) {
        SceneManager.LoadScene(level);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void PlayButtonSound() {
        am.Play(buttonClickIndex);
    }
}
