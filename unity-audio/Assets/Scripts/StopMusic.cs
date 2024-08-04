using UnityEngine;

public class WinFlag : MonoBehaviour
{
    public AudioClip victorySting;
    private AudioSource bgmSource;

    void Start()
    {
        bgmSource = GameObject.Find("Level01BGM").GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bgmSource.Stop();
            AudioSource.PlayClipAtPoint(victorySting, transform.position);
        }
    }
}