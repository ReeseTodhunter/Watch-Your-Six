using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float MOVEMENT_SPEED = 5f;
    const float ROTATION_SPEED = 500f;

    float horizontalInput = 0f;
    float verticalInput = 0f;
    float inputMagnitude = 0f;
    Vector2 movementDirection = new Vector2(0f, 0f);

    // Update is called once per frame
    void Update()
    {
        //Get the direction of movement (WASD/Joystick input) and store in a Vector2 and get the magnitude
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontalInput, verticalInput);
        inputMagnitude = Mathf.Clamp01(movementDirection.magnitude); //Save original movement magnitude
        movementDirection.Normalize();

        //Move the sprite in the direction of movement
        gameObject.transform.Translate(movementDirection * Time.deltaTime * inputMagnitude * MOVEMENT_SPEED, Space.World);
        
        //Rotate the sprite in the direction of movement
        if(movementDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, ROTATION_SPEED * Time.deltaTime);
        }
    }
}
