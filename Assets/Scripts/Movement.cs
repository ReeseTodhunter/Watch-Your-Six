using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    const float MOVEMENT_SPEED = 5f;
    const float ROTATION_SPEED = 500f;

    Controls controls;

    float inputMagnitude = 0f;
    Vector2 movementDirection = new Vector2(0f, 0f);
    Vector2 lookDirection = new Vector2(0f, 0f);

    void Awake()
    {
        controls = new Controls();

        controls.Gameplay.Move.performed += ctx => movementDirection = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => movementDirection = Vector2.zero;

        controls.Gameplay.Look.performed += ctx => lookDirection = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => lookDirection = Vector2.zero;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the direction of movement (WASD/Joystick input) and store in a Vector2 and get the magnitude
        inputMagnitude = Mathf.Clamp01(movementDirection.magnitude); //Save original movement magnitude
        movementDirection.Normalize();
        //Move the sprite in the direction of movement
        gameObject.transform.Translate(movementDirection * Time.deltaTime * inputMagnitude * MOVEMENT_SPEED, Space.World);

        //Rotate the sprite in the direction of right stick
        if (lookDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, ROTATION_SPEED * Time.deltaTime);
        }
    }
}