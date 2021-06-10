using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum ControlType { HumanInput, AI }
    public ControlType controlType = ControlType.HumanInput;

    public float BestLapTime { get; private set; } = Mathf.Infinity;
    public float LastLapTime { get; private set; } = 0;
    public float CurrentLapTime { get; private set; } = 0;
    public int CurrentLap { get; private set; } = 0;

    private float lapTimer;
    private int lastCheckpointPassed = 0;

    private GameObject checkpointsParent;
    private int checkpointCount;
    private int checkpointLayer;
    private Car carController;

    void Start()
    {
        checkpointsParent = GameObject.Find("Checkpoints");
        checkpointCount = checkpointsParent.transform.childCount;
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
        carController = GetComponent<Car>();
    }

    void Update()
    {
        CurrentLapTime = lapTimer > 0 ? Time.time - lapTimer : 0;

        if (controlType == ControlType.HumanInput)
        {
            carController.Steer = GameManager.Instance.InputController.SteerInput;
            carController.Throttle = GameManager.Instance.InputController.ThrottleInput;
        }
    }

    private void StartLap()
    {
        CurrentLap++;
        lastCheckpointPassed = 1;
        lapTimer = Time.time;
    }

    private void EndLap()
    {
        LastLapTime = Time.time - lapTimer;
        BestLapTime = Mathf.Min(LastLapTime, BestLapTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == checkpointLayer)
        {
            //if you pass checkpoint 1
            if (other.gameObject.name == "1")
            {
                if (lastCheckpointPassed == checkpointCount)
                {
                    EndLap();
                    StartLap();
                }
                else
                    StartLap();
            }

            //if you pass the next checkpoint
            if (other.gameObject.name == (lastCheckpointPassed + 1).ToString())
                lastCheckpointPassed++;
        }
    }
}
