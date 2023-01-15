using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnController : MonoBehaviour
{
    TimerDirector timerDirector;

    [System.NonSerialized]
    public int gameNum; 

    public bool inputReady = false;

    void Start()
    {
        timerDirector = GameObject.Find("Timer").GetComponent<TimerDirector>(); 
    }

    void Update()
    {
        gameNum = OscReceive.oscNum;
        Debug.Log(gameNum);

        operateInput();

        if(Input.GetKeyDown(KeyCode.Q)){
            EnvironmentController.envStatus = 0;
        }
        if(Input.GetKeyDown(KeyCode.A)){
            EnvironmentController.envStatus = 1;
        }
        if(Input.GetKeyDown(KeyCode.S)){
            EnvironmentController.envStatus = 2;
        }
        if(Input.GetKeyDown(KeyCode.W)){
            EnvironmentController.envStatus = 3;
        }
    }

    public void operateInput(){
        switch(gameNum){
            case 0:
                break;
            case 1:
                inputReady = true;
                break;
            case 2:
                inputReady = false;
                break;
            case 3:
                if(inputReady) timerDirector.nowCounting = false;
                break;
        }
    }

    public void innAction(){
        if(inputReady){
            if( gameNum == 4 ){
                EnvironmentController.envStatus = 1;
            }else if( gameNum == 5){
                EnvironmentController.envStatus = 2;
            }else if( gameNum == 6){
                EnvironmentController.envStatus = 3;
            }
        }
    }
}
