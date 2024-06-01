using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // Reference the Player GameObject
    public Vector3 offset; // Store the distance difference between Player and Camera

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // LastUpdate is called once every Update has been called.
    void LateUpdate()
    {
        transform.position = player.transform.position + offset; // Set the position of the camera
        transform.LookAt(player.transform); // Make sure Camera only looking at player 
    }
}
