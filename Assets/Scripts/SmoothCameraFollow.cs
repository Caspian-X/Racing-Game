using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothnessSpeed = 10f; //0.125f
    public Vector3 cameraOffset;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothnessSpeed * Time.deltaTime); //Change Lerp to SmoothDamp
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
