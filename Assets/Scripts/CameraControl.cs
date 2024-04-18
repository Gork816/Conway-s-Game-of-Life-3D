using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Speed of movement
    public float movementSpeed = 5.0f;
    // Speed of rotation
    public float rotationSpeed = 3.0f;

    // Variables to store rotation values
    float rotationX = 0.0f;
    float rotationY = 0.0f;

    void Update()
    {
        // Rotation based on mouse input
        rotationX -= Input.GetAxis("Camera Vertical") * rotationSpeed;
        rotationY += Input.GetAxis("Camera Horizontal") * rotationSpeed;

        // Clamp vertical rotation
        rotationX = Mathf.Clamp(rotationX, -90.0f, 90.0f);

        // Apply rotation to camera
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0.0f);

        // Movement based on keyboard input
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement);

        // Normalize diagonal movement
        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }

        // Move camera relative to its rotation
        transform.Translate(movement * movementSpeed * Time.deltaTime);
    }
}
