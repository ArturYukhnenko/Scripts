using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour {
    public float timeCircle;
    public float timeRemain = 0;
    public bool timeRunning = false;
    public TMP_Text timeTxt;
    public Button night;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRunning)
        {
            if (timeRemain >= 0)
            {
                timeRemain -= Time.deltaTime;
                Display(timeRemain);
            }
            else
            {
                timeRunning = false;
                night.onClick.Invoke();
            }
        }
    }

    public void Activate(float time)
    {
        timeRunning = true;
        timeRemain = time;
    }

    void Display(float timeToDisplay)
    {
        timeToDisplay += 1;
        float min = Mathf.FloorToInt(timeToDisplay / 60);
        float sec = Mathf.FloorToInt(timeToDisplay % 60);
        timeTxt.text = $"{min:00} : {sec:00}";
    }

    public void ResetTimer() {
        timeRemain = timeCircle;
        timeRunning = true;
    }
}
