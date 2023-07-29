using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30.0f;
    [SerializeField] float timeToShowCorrectAnswer = 10.0f;
    float timerValue;
    public bool isLoadNextQuestion = false;

    public float fillFraction = 0;

    enum TimerState
    {
        STATE_ANSWERING,
        STATE_SHOW_ANSWER
    };

    TimerState _state = TimerState.STATE_ANSWERING;

    public bool IsAnswering()
    {
        return this._state == TimerState.STATE_ANSWERING;
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }
    // Update is called once per frame
    void Start()
    {
        SetState(TimerState.STATE_ANSWERING);
    }
    void Update()
    {
        UpdateTimer();
    }

    void SetState(TimerState state)
    {
        this._state = state;
        switch (this._state)
        {
            case TimerState.STATE_ANSWERING:
                {
                    timerValue = timeToCompleteQuestion;
                    break;
                }
            case TimerState.STATE_SHOW_ANSWER:
                {
                    timerValue = timeToShowCorrectAnswer;
                    break;
                }
        }
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        switch (this._state)
        {
            case TimerState.STATE_ANSWERING:
                {
                    if (isLoadNextQuestion) break;
                    if (timerValue <= 0)
                    {
                        SetState(TimerState.STATE_SHOW_ANSWER);
                    }
                    else
                    {
                        fillFraction = timerValue / timeToCompleteQuestion;
                    }
                    break;
                }
            case TimerState.STATE_SHOW_ANSWER:
                {
                    if (timerValue < 0)
                    {
                        isLoadNextQuestion = true;
                        SetState(TimerState.STATE_ANSWERING);
                    }
                    else
                    {
                        fillFraction = timerValue / timeToShowCorrectAnswer;
                    }
                    break;
                }
        }
    }
}
