using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    private float musicVolume;
    private float sfxVolume;

    public void Start() {
        LoadSettings();
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
    }
    private void LoadSettings() {
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.75f);
    }
    public void SaveSettings() {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();

        AudioManager.Instance.SetMusicVolume(musicVolume);
        AudioManager.Instance.SetSFXVolume(sfxVolume);
    }
    public void SetMusicVolume(float volume) {
        musicVolume = volume;
        musicSlider.value = musicVolume;
    }
    public void SetSfxVolume(float volume) {
        sfxVolume = volume;
        sfxSlider.value = sfxVolume;
    }
    public float GetMusicVolume() {
        return musicVolume;
    }
    public float GetSfxVolume() {
        return sfxVolume;
    }
}
