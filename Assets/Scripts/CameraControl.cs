using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 7.0f;
    [SerializeField]
    float rotationSpeed = 1.0f;

    // Variables to store rotation values
    public float rotV = 0.0f;
    public float rotH = 0.0f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Rotation based on mouse input
        rotV -= Input.GetAxis("Camera Vertical") * rotationSpeed;
        rotH += Input.GetAxis("Camera Horizontal") * rotationSpeed;

        // Clamp vertical rotation
        rotV = Mathf.Clamp(rotV, -90.0f, 90.0f);

        // Apply rotation to camera
        transform.localRotation = Quaternion.Euler(rotV, rotH, 0.0f);

        // Movement based on keyboard input
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xMovement = xInput * Mathf.Cos(rotH * Mathf.Deg2Rad) + zInput * Mathf.Sin(rotH * Mathf.Deg2Rad);
        float yMovement = Input.GetAxis("Ultra Vertical");
        float zMovement = zInput * Mathf.Cos(rotH * Mathf.Deg2Rad) - xInput * Mathf.Sin(rotH * Mathf.Deg2Rad);

        // Calculate movement direction
        Vector3 movement = new Vector3(xMovement, yMovement, zMovement);

        // Normalize diagonal movement
        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }

        // Move camera relative to its rotation
        rb.velocity = movement * movementSpeed;
    }
}
