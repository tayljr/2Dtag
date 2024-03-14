using System.Collections;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private float timer;
    private bool timesUp = false;
    private bool timerStopped = false;
    public TMP_Text timerUI;
    public GameManager gameManager;
    
    void OnEnable()
    {
        gameManager.PauseGameEvent += StopTimer;
        gameManager.PlayGameEvent += PlayTimer;
    }
    void OnDisable()
    {
        gameManager.PauseGameEvent -= StopTimer;
        gameManager.PlayGameEvent -= PlayTimer;
    }

    public void StartTimer(int setTime)
    {
        timer = setTime;
        timerUI.text = timer.ToString();
        StartCoroutine(Countdown());
    }

    public void StopTimer()
    {
        StopAllCoroutines();
        timerStopped = true;
    }

    public void PlayTimer()
    {
        timerStopped = false;
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        //Debug.Log("Timer is set for " + timer + " seconds.");
        while (timer > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timer -= 1.0f;
            timerUI.text = timer.ToString();
            //Debug.Log("Time left: " + ((int)timer));
        }
        //Debug.Log("Time is up.");
        timesUp = true;
        StopAllCoroutines();
    }

    public bool TimerStatus()
    {
        return timesUp;
    }

    public bool TimerStopped()
    {
        return timerStopped;
    }
}