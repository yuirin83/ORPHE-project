using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField]
    AudioClip[] audioClips;
    AudioSource envSound;

    private InnController inn;

    public static int envStatus = 0;

    int preStatus = 0;

    private bool envFlag = false;

    GameObject fire, river;

    void Start()
    {
        inn = GameObject.Find("InterfaceController").GetComponent<InnController>();

        envSound = this.GetComponent<AudioSource>();
        envSound.clip = audioClips[0];

        fire = GameObject.Find("fire");
        river = GameObject.Find("river");
        fire.SetActive(false);
        river.SetActive(false);
    }

    void Update()
    {
        if(envStatus != preStatus){
            envFlag = true;
        }else{
            envFlag = false;
        }

        preStatus = envStatus;
        setEnv();
    }

    public void setEnv(){
        if(envFlag){
            switch(envStatus){
                case 0:
                    break;
                case 1:
                    fire.SetActive(true);
                    river.SetActive(false);
                    soundPlay("fire");
                    break;
                case 2:
                    fire.SetActive(false);
                    river.SetActive(true);
                    soundPlay("water");
                    break;
            }
        }
    }

    public void soundPlay(string seName)
    {
        switch (seName)
        {
            case "fire":
                envSound.clip = audioClips[0];
                envSound.Play();
                break;
            case "water":
                envSound.clip = audioClips[1];
                envSound.Play();
                break;
        }
    }

    public void soundStop(string seName)
    {
        switch (seName)
        {
            case "fire":
                envSound.Stop();
                break;
            case "water":
                envSound.Stop();
                break;
        }
    }
}