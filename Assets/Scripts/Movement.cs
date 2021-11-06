using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float MOVEMENT_SPEED = 5.0f;
    const float SMOOTH_TURN_TIME = 0.1f;
    float turnSmoothVelocity;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        gameObject.transform.Translate(Vector3.right* Time.deltaTime * MOVEMENT_SPEED * horizontalMove);
        gameObject.transform.Translate(Vector3.up * Time.deltaTime * MOVEMENT_SPEED * verticalMove);
        
        //float targetAngle = Mathf.Atan2(transform.position.x, transform.position.y) * Mathf.Rad2Deg;
        //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref turnSmoothVelocity, SMOOTH_TURN_TIME);
        //transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
