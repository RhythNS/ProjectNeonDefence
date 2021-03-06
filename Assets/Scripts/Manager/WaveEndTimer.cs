﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveEndTimer : MonoBehaviour
{
    public static WaveEndTimer Instance { get; private set; }

    private float remainingTime;
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        Instance = this;
    }

    public void StartCountdown(float time)
    {
        remainingTime = time;
        text.text = remainingTime.ToString();
        enabled = true;
    }

    private void Update()
    {
        remainingTime -= Time.deltaTime;
        text.text = Mathf.Round(remainingTime).ToString();

        if (remainingTime <= 0)
        {
            GameManager.Instance.OnBeginNextWave();
            enabled = false;
        }
    }
}
