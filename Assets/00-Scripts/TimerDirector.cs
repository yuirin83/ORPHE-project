using System.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class TimerDirector : MonoBehaviour
{
    [System.NonSerialized] public bool nowCounting = false;
    [System.NonSerialized] public bool deltaTimeRefresh = true;
    [System.NonSerialized] public bool isPose = false;
    
    [Space(10)]

    [SerializeField]
    private TextMeshProUGUI timeCounterText;

    private DateTime startDataTime;
    private TimeSpan elapsedTime;
    private TimeSpan totalTime;
    private TimeSpan maxTime;

    [Space(10)]
    
    public int setTime = 10;
    public int setTimeSec = 0;

    [System.NonSerialized] public bool timeUp = false;

    void Start()
    {
        startDataTime = DateTime.Now;
        elapsedTime = new TimeSpan(0,0,0);
        totalTime = new TimeSpan(0,0,0);
        maxTime = new TimeSpan(0,setTime,setTimeSec);
    }

    void Update()
    {
        if(!nowCounting){
            CountReady();
        }else{
            CountStart();
        }

        //カウントダウン表記で表示
        if(totalTime.Hours == 0){
            timeCounterText.text = totalTime.Minutes.ToString("00") + ":" + totalTime.Seconds.ToString("00");
        }else{
            timeCounterText.text = totalTime.Hours.ToString("00") + ":" + totalTime.Minutes.ToString("00") + ":" + totalTime.Seconds.ToString("00");
        }    
    }

    public void CountReady()
    {
        timeUp = false;
        deltaTimeRefresh = true;
        isPose = false;
        
        maxTime = new TimeSpan(0,setTime,setTimeSec);
        totalTime = maxTime;
    }

    public void CountStart()
    {
        DeltaTimeRefresh();

        if(!isPose){
            elapsedTime = DateTime.Now - startDataTime;
            //elapsedTime：経過時間
            if(totalTime.Hours > 0 || totalTime.Minutes > 0 || totalTime.Seconds > 0){
                totalTime = maxTime - elapsedTime; 
            }else{
                totalTime = new TimeSpan(0,0,0);
                timeUp = true;
            }
        }else{
            maxTime = totalTime;
        }
    }

    //停止あるいは一時停止から復帰する際に必ず通す
    public void DeltaTimeRefresh()
    {
        if(deltaTimeRefresh){
            startDataTime = DateTime.Now;
            deltaTimeRefresh = false;
        }
    }
}
