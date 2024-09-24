using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Play animation according to selected scene 
/// </summary>
public class CutsceneManager : MonoBehaviour
{
    public Animator cutsceneAnimator;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Active Scene: " + sceneName);

        switch (sceneName)
        {
            case "Level01":
                Debug.Log("Playing Intro01");
                cutsceneAnimator.Play("Intro01");
                break;
            case "Level02":
                Debug.Log("Playing Intro02");
                cutsceneAnimator.Play("Intro02");
                break;
            case "Level03":
                Debug.Log("Playing Intro03");
                cutsceneAnimator.Play("Intro03");
                break;
            default:
                Debug.Log("No matching scene, disabling animator");
                cutsceneAnimator.enabled = false;
                break;
        }
    }
}
