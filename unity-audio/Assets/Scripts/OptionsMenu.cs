using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertYToggle;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public AudioMixer audioMixer;
    
    private const string BGM_VOLUME_KEY = "BGMVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    
    void Start()
    {
        // Load the invert Y setting from PlayerPrefs
        bool invertY = PlayerPrefs.GetInt("InvertY", 0) == 1;
        invertYToggle.isOn = invertY;

        // Load and apply the saved BGM and SFX volumes
        float savedBGMVolume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, 0.75f);
        bgmSlider.value = savedBGMVolume;
        SetBGMVolume(savedBGMVolume);

        float savedSFXVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.75f);
        sfxSlider.value = savedSFXVolume;
        SetSFXVolume(savedSFXVolume);

        // Add listeners to the sliders to adjust volume when dragged
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    // Called when the Apply button is pressed
    public void Apply()
    {
        // Save the invert Y setting
        bool invertY = invertYToggle.isOn;
        PlayerPrefs.SetInt("InvertY", invertY ? 1 : 0);

        // Save the BGM and SFX volumes
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, bgmSlider.value);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxSlider.value);
        PlayerPrefs.Save();

        Debug.Log("Settings saved: InvertY = " + PlayerPrefs.GetInt("InvertY") + 
                  ", BGMVolume = " + PlayerPrefs.GetFloat(BGM_VOLUME_KEY) +
                  ", SFXVolume = " + PlayerPrefs.GetFloat(SFX_VOLUME_KEY));

        // Return to the previous scene
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene", "MainMenu"));
    }

    // Called when the Back button is pressed
    public void Back()
    {
        // Return to the previous scene without saving any changes
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene", "MainMenu"));
    }

    // Adjust BGM volume and apply it to the AudioMixer
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20); // Convert to dB
    }

    // Adjust SFX volume and apply it to the AudioMixer
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20); // Convert to dB
    }
}
