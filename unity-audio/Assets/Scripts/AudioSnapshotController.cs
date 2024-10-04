using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Method that transition between paused audio and normal audio (low pass filter)
/// </summary>
public class AudioSnapshotController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixerSnapshot normalSnapshot; // Snapshot normal
    public AudioMixerSnapshot pausedSnapshot; // Snapshot paused
    public float transitionTime = 0.5f; // Transition time

    // Called when game is paused
    public void PauseGame()
    {
        pausedSnapshot.TransitionTo(transitionTime); // Transition to low pass filter
    }

    // Appelée lorsque le jeu reprend
    public void ResumeGame()
    {
        normalSnapshot.TransitionTo(transitionTime); // Transition to normal
    }
}
