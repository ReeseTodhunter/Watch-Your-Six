using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float MOVEMENT_SPEED = 5.0f;
    const float SMOOTH_TURN_TIME = 0.1f;
    float turnSmoothVelocity;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(-Vector3.right * Time.deltaTime * MOVEMENT_SPEED);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * MOVEMENT_SPEED);
        }
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(Vector3.up * Time.deltaTime * MOVEMENT_SPEED);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(-Vector3.up * Time.deltaTime * MOVEMENT_SPEED);
        }

        //float targetAngle = Mathf.Atan2(transform.position.x, transform.position.y) * Mathf.Rad2Deg;
        //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref turnSmoothVelocity, SMOOTH_TURN_TIME);
        //transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
