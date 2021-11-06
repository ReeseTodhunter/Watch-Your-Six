using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLazers : MonoBehaviour
{
    public Transform laserPoint;
    public LineRenderer lineRenderer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine (Shoot());
        }

    }

    IEnumerator Shoot()
    {
       RaycastHit2D hitInfo = Physics2D.Raycast(laserPoint.position, laserPoint.up);

        if (hitInfo)
        {
            Debug.Log(hitInfo.transform.name);

            lineRenderer.SetPosition(0, laserPoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, laserPoint.position);
            lineRenderer.SetPosition(1, laserPoint.position + laserPoint.up * 100);
        }

        lineRenderer.enabled = true;

        //wait for a short amount of time
        yield return new WaitForSeconds(0.05f);

        lineRenderer.enabled = false;
    }
}
