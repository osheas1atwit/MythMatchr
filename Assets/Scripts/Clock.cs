using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] GameObject timerObject;
    [SerializeField] TextMeshProUGUI timerText;
    private float baseTime;
    private float currentTimer;
    private bool easyMode = false;
    private bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        //startTime = Time.time;
    }

    public void InitTimer(float amountofTime)
    {
        started = true;
        if (amountofTime == 0)
        {
            timerObject.SetActive(false);
            easyMode = true;
        }
        currentTimer = amountofTime;
        baseTime = amountofTime;
    }

    public void ClockReset()
    {
        currentTimer = baseTime;
    }

    // Update is called once per frame
    void Update()
    {
        /* float t = Time.time - startTime;

         string m = ((int)t / 60).ToString();
         string s = (t % 60).ToString();

         timerText.text = m + ":" + s;*/
        if (started)
        {
            if (easyMode)
                return;
            currentTimer -= Time.deltaTime;
            timerText.text = currentTimer.ToString("0");

            if(currentTimer <= 0)
            {
                // HURT {PLAYER
                GM.instance.TimerEnded();
                currentTimer = baseTime;
            }
        }
    }
}
