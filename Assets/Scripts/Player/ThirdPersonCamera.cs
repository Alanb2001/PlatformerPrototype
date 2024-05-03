using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")] 
    public Transform orientaiton;
    public Transform player;
    public Transform playerObject;
    public Rigidbody rigidBody;
    
    public float rotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Rotate orientation
        Vector3 viewDirection =
            player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientaiton.forward = viewDirection.normalized;
        
        // Rotate player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDirection = orientaiton.forward * verticalInput + orientaiton.right * horizontalInput;

        if (inputDirection != Vector3.zero)
        {
            playerObject.forward = Vector3.Slerp(playerObject.forward, inputDirection.normalized,
                Time.deltaTime * rotationSpeed);
        }
    }
}
