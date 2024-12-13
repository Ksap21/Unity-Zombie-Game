using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement: MonoBehaviour
{
    public float Mouse_Sensitivity=100f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    //public float xSensitivity = 30f;
    //public float ySensitivity = 30f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;


    public void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Mouse_Sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Mouse_Sensitivity * Time.deltaTime;

        // Adjust the xRotation (up/down) and clamp it
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // Adjust the yRotation (left/right)
        yRotation += mouseX;

        // Apply the rotation to the player (y-axis) and camera (x-axis)
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        //cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
