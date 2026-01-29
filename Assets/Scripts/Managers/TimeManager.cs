using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro; // Requires TextMeshPro to be installed

public class TimeManager : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI timeText;

    public static int Hour { get; private set; }
    public static int Minute { get; private set; }

    // 0.2f means 1 minute passes every 0.2 real seconds.
    // (Calculation: 1 / 0.2 = 5 minutes per real second)
    private float minuteToRealTime = 0.2f;
    private float timer;
    private bool isShiftActive = true;

    // Actions if other scripts need to listen for time changes
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    void Start()
    {
        Hour = 8;
        Minute = 0;
        timer = minuteToRealTime;
        UpdateUI();
    }

    void Update()
    {
        if (!isShiftActive) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Minute++;
            OnMinuteChanged?.Invoke();

            if (Minute >= 60)
            {
                Hour++;
                Minute = 0;
                OnHourChanged?.Invoke();
            }

            UpdateUI();
            timer = minuteToRealTime;

            // Check if it's 6 PM (18:00)
            if (Hour >= 18)
            {
                EndShift();
            }
        }
    }

    void UpdateUI()
    {
        if (timeText == null) return;

        // Use a ternary operator to choose between "Open" and "Closed"
        string statusText = isShiftActive ? " <color=#00FF00>Open</color>" : " <color=#FF0000>Closed</color>";

        // Formats as: Open 08:00
        timeText.text = $"{statusText} \r\n <color=#FFFFFF>{Hour:00}:{Minute:00}</color>";
    }

    void EndShift()
    {
        isShiftActive = false;
        Hour = 18;
        Minute = 0;
        UpdateUI();
        Debug.Log("Flame & Fork is now Closed!");
    }

    // Helper for other scripts (like your Spawner) to check status
    public bool IsBistroOpen()
    {
        return isShiftActive;
    }

    public bool IsShiftActive()
    {
        return isShiftActive;
    }
}
