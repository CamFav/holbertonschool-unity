using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; // Controls velocity multiplier
    private Rigidbody rb;
    private int score =  0;   // Set the initial value of score to 0.
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Text winLoseText; // Reference to the WinLoseText UI element
    public Image winLoseBG; // Reference to the WinLoseBG UI element


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetScoreText();
        SetHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            winLoseText.text = "Game Over!";
            winLoseText.color = Color.white;
            winLoseBG.color = Color.red;
            winLoseBG.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3));
            score = 0;
            health = 5;
        }
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    void FixedUpdate()
    {
        // Get input from ZQSD or arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a Vector3 for movement
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Move the player
        rb.AddForce(movement * speed);
    }

    // Increment the value of score when the Player touches an object tagged Pickup
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup")) // Check if player collides with a object tagged Pickup (coin)
        {
            score++;
            SetScoreText();
            Debug.Log("Score: " + score);
            other.gameObject.SetActive(false); // Disable the coin (Destroy(other.gameObject) to destroy it)
        }

        if (other.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
            Debug.Log("Health: "  + health);
        }

        if (other.CompareTag("Goal"))
        {
            winLoseText.text = "You Win!";
            winLoseText.color = Color.black;
            winLoseBG.color = Color.green;
            winLoseBG.gameObject.SetActive(true);

            StartCoroutine(LoadScene(3));
        }
    }

    // Method to update the score text UI element
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // Update the score text with the current score
    }

    // Method to update the  health text UI element
    void  SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
