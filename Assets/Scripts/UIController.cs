using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject UIRaceCanvas;
    public Text UITextCurrentLap;
    public Text UITextCurrentLapTime;
    public Text UITextBestLapTime;
    public Player player;

    void Update()
    {
        UITextCurrentLap.text = $"Lap: {player.CurrentLap}";
        UITextCurrentLapTime.text = $"Current lap time: {Math.Round(player.CurrentLapTime, 3)}";
        UITextBestLapTime.text = $"Best lap time: {Math.Round(player.BestLapTime, 3)}";
    }
}
