using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class old_Car : MonoBehaviour
{
    public Transform centerOfMass;

    public WheelCollider wheelColLeftFront;
    public WheelCollider wheelColRightFront;
    public WheelCollider wheelColLeftBack;
    public WheelCollider wheelColRightBack;

    public Transform wheelLeftFront;
    public Transform wheelRightFront;
    public Transform wheelLeftBack;
    public Transform wheelRightBack;

    public float motorTorque = 100f;
    public float maxSteer = 20f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        wheelColLeftBack.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        wheelColRightBack.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        wheelColLeftFront.steerAngle = Input.GetAxis("Horizontal") * maxSteer;
        wheelColRightFront.steerAngle = Input.GetAxis("Horizontal") * maxSteer;
    }

    void Update()
    {
        var pos = Vector3.zero;
        var rot = Quaternion.identity;

        wheelColLeftFront.GetWorldPose(out pos, out rot);
        wheelLeftFront.position = pos;
        wheelLeftFront.rotation = rot;

        wheelColLeftBack.GetWorldPose(out pos, out rot);
        wheelLeftBack.position = pos;
        wheelLeftBack.rotation = rot;

        wheelColRightFront.GetWorldPose(out pos, out rot);
        wheelRightFront.position = pos;
        wheelRightFront.rotation = rot * Quaternion.Euler(0, 180, 0);

        wheelColRightBack.GetWorldPose(out pos, out rot);
        wheelRightBack.position = pos;
        wheelRightBack.rotation = rot * Quaternion.Euler(0, 180, 0);
    }
}
