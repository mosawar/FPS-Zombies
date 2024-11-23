using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 10f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    // Method to process the player's look input (mouse movement)
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // This will tilt the camera up or down depending on the mouse movement
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;

        // Clamp the xRotation to prevent excessive tilting of the camera
        // This ensures the camera can't rotate too far up or down (beyond -80 and 80 degrees)
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

        // Apply the calculated xRotation to the camera's local rotation to control the camera pitch (up/down look)
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player (the entire body) horizontally based on the mouse's horizontal movement (mouseX)
        // The player rotates around the y-axis (yaw) when the mouse moves left or right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
