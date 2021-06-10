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

    void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
    }

    void Update()
    {
        //apply power and steering
        Steer = GameManager.Instance.InputController.SteerInput;
        Throttle = GameManager.Instance.InputController.ThrottleInput;

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
}
