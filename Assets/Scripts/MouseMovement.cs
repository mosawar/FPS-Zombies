using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 500f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;

    // Start is called before the first frame update
    void Start()
    {
        // locks cursor and makes it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // up and down
        xRotation -= mouseY;

        // clamp rotation
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // left right
        yRotation += mouseX;

        // for rotation
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
