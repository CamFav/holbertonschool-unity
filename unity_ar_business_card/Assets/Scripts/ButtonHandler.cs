using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ButtonHandler : MonoBehaviour
{
    public Button button;
    public string url;
    public Color pressColor = Color.gray;
    private Color originalColor;
    private Image buttonImage;
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        buttonImage = button.GetComponent<Image>();
        audioSource = button.GetComponent<AudioSource>();

        if (buttonImage != null)
        {
            originalColor = buttonImage.color;
        }

        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (buttonImage != null)
        {
            StartCoroutine(HandleClickFeedback());
        }

        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
    }

    private IEnumerator HandleClickFeedback()
    {
        if (buttonImage != null)
        {
            buttonImage.color = pressColor;
            yield return new WaitForSeconds(0.1f);
            buttonImage.color = originalColor;
        }
    }
}
