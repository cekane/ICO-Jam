using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timer = 7f;
    // Start is called before the first frame update
    private bool timerStarted = false;
    void Start()
    {
        timerText.enabled = false;
    }

    void FixedUpdate()
    {
        if(timerStarted)
        {
            timer -= Time.deltaTime;
            int timerInt = (int)(timer/2); 
            timerText.text = timerInt.ToString();
            if(timer < 0)
            {
                // You lose, out of time
            }
        }
    }

    public void StartTimer(bool s)
    {
        timerStarted = s;
        timerText.enabled = true;
    }
}
