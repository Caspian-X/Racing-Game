using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform centerOfMass;
    public float motorTorque = 1200f;
    public float maxSteer = 40f;
    public float maxRPM = 20000f;

    public float Steer { get; set; }
    public float Throttle { get; set; }

    private Rigidbody rb;
    private Wheel[] wheels;
    private Vector3 startingPos;
    private Quaternion startingRot;

    void Awake()
    {
        wheels = GetComponentsInChildren<Wheel>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
        startingPos = gameObject.transform.position;
        startingRot = gameObject.transform.rotation;
    }

    void Update()
    {
        //apply power and steering
        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = Steer * maxSteer;
            wheel.Torque = Throttle * motorTorque;
        }

        //set the max speed
        foreach (var wheel in wheels)
        {
            if (wheel.GetComponent<WheelCollider>().rpm > maxRPM)
                wheel.Torque = 0;
        }
    }

    /// <summary>
    /// Stops each of the wheels from spinning and resets the player's position.
    /// </summary>
    public void Restart()
    {
        foreach (var wheel in wheels)
            wheel.GetComponent<WheelCollider>().brakeTorque = float.MaxValue;
        rb.velocity = Vector3.zero;

        gameObject.transform.rotation = startingRot;
        gameObject.transform.position = startingPos;

        Invoke(nameof(ResetWheelBrakeTorque), 0.25f);
    }

    private void ResetWheelBrakeTorque()
    {
        foreach (var wheel in wheels)
            wheel.GetComponent<WheelCollider>().brakeTorque = 0;
    }
}
