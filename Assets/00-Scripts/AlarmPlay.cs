using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmPlay : MonoBehaviour
{
    [SerializeField]
    AudioClip[] audioClips;

    AudioSource alarm_SE;

    private TimerDirector timerDirector;
    private bool alarmFlag = false;

    void Start()
    {
        alarm_SE = this.GetComponent<AudioSource>();
        alarm_SE.clip = audioClips[0];

        timerDirector = this.GetComponent<TimerDirector>();
    }

    void Update()
    {
        TimeUpAlarm();
    }

    public void TimeUpAlarm()
    {
        if(timerDirector.timeUp && !alarmFlag)
        {
            alarmFlag = true;
            alarm_SE.Play();
        }

        if(!timerDirector.timeUp)
        {
            alarmFlag = false;
            alarm_SE.Stop();
        }
        
    }
}
