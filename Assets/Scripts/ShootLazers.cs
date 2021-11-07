using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootLazers : MonoBehaviour
{
    public Transform laserPoint;
    public LineRenderer lineRenderer;

    protected int reflections = 2;
    protected float maxLength = 100;
    // Update is called once per frame
    void Update()
    {
        var gamepad = Gamepad.current;
        if (Gamepad.current == null)
        { }
        if (gamepad.rightTrigger.wasPressedThisFrame)
        {
            StartCoroutine(Shoot(reflections, maxLength));
        }
    }

    IEnumerator Shoot(int reflections, float maxLength)
    {
        float remainingLength = maxLength;
        Ray2D reflectlaser;
        RaycastHit2D hitInfo;
        Vector2 reflectNormal = new Vector2(0, 0);
        Vector2 startPoint = laserPoint.position;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, laserPoint.position);
        reflectlaser = new Ray2D(laserPoint.position, laserPoint.up);

        Debug.DrawRay(startPoint, reflectlaser.direction * 10, Color.red);

        for (int i = 0; i < reflections; i++)
        {
            hitInfo = Physics2D.Raycast(startPoint, reflectlaser.direction, remainingLength);
            if (hitInfo.transform.name == "Target")
            {
                Debug.Log(hitInfo.transform.name);


                //lineRenderer.SetPosition(0, laserPoint.position);
                lineRenderer.SetPosition(1, hitInfo.point);
            }
            else
            {
                Debug.Log("Also " + hitInfo.transform.name);
                Debug.Log("Normal: " + hitInfo.normal);
                Debug.Log("Point: " + hitInfo.point);

                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hitInfo.point);
                //lineRenderer.SetPosition(1, laserPoint.position + laserPoint.up * remainingLength);
                remainingLength -= Vector2.Distance(laserPoint.position, hitInfo.normal);
                reflectlaser = new Ray2D(hitInfo.point, Vector2.Reflect(reflectlaser.direction, -hitInfo.normal).normalized);
                Debug.DrawRay(hitInfo.point, reflectlaser.direction * 10, Color.red);
                startPoint = hitInfo.point;

            }
        }

        lineRenderer.enabled = true;

        //wait for a short amount of time
        yield return new WaitForSeconds(0.05f);

        lineRenderer.enabled = false;
    }
}