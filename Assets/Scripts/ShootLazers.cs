using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
public class ShootLazers : MonoBehaviour
{
    public Transform laserPoint;
    public LineRenderer lineRenderer;

    protected int reflections = 3;
    protected float maxLength = 100;
    // Update is called once per frame
    void Update()
    {
        var Gamepads = Gamepad.all;
        if (Gamepads[GetComponent<Movement>().playerNum].rightTrigger.wasPressedThisFrame)
        {
            StartCoroutine(Shoot(reflections, maxLength));
        }
    }

    IEnumerator Shoot(int reflections, float maxLength)
    {
        float remainingLength = maxLength;
        Ray2D reflectlaser;
        RaycastHit2D hitInfo;
        Vector2 startPoint;
        Vector2 direction;

        lineRenderer.positionCount = 1; //Sets size of positionCount eg. size is now 0, 1
        lineRenderer.SetPosition(0, laserPoint.position); //Sets position 0 to the gun (Starting point)

        startPoint = laserPoint.position;
        direction = laserPoint.up;
        for (int i = 0; i < reflections; i++)
        {
            reflectlaser = new Ray2D(startPoint, direction); //Cast Laser (Ray fired)
            hitInfo = Physics2D.Raycast(startPoint, direction, remainingLength); //What does laser hit?
            if (hitInfo.transform.name == "Target")
            {
                lineRenderer.positionCount += 1;
                Debug.Log(hitInfo.transform.name);
                //lineRenderer.SetPosition(0, laserPoint.position);
                lineRenderer.SetPosition(i + 1, hitInfo.point);
                remainingLength = 0f;
                i = reflections;
                GetComponent<Movement>().playerScore += 1;
            }
            else
            {
                Debug.Log("Also " + hitInfo.transform.name);
                Debug.Log("Point: " + hitInfo.point);

                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(i + 1, hitInfo.point);
                remainingLength -= Vector2.Distance(laserPoint.position, hitInfo.normal);
                //reflectlaser = new Ray2D(hitInfo.point, Vector2.Reflect(reflectlaser.direction, -hitInfo.normal).normalized);
                Debug.DrawRay(hitInfo.point, reflectlaser.direction * 10, Color.red);
                startPoint = hitInfo.point - (direction * 0.01f);
                direction = Vector2.Reflect(reflectlaser.direction, -hitInfo.normal).normalized;
            }
        }

        lineRenderer.enabled = true;

        //wait for a short amount of time
        yield return new WaitForSeconds(0.2f);

        lineRenderer.enabled = false;
    }
}