using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public AudioSource bgmAudioSource;

    public void LoadScene(string sceneName)
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop();
        }

        SceneManager.LoadScene(sceneName);
    }
}
