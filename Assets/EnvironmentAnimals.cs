using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAnimals : MonoBehaviour
{
    [SerializeField]
    AudioClip[] audioClips;
    AudioSource envSound;

    GameObject uguisu, bird, hato, frog;

    void Start()
    {
        envSound = this.GetComponent<AudioSource>();
        envSound.clip = audioClips[0];

        uguisu = GameObject.Find("uguisu");
        bird = GameObject.Find("bird");
        hato = GameObject.Find("hato");
        frog = GameObject.Find("frog");
        uguisu.SetActive(false);
        // bird.SetActive(false);
        // hato.SetActive(false);
        // frog.SetActive(false);
    }

    void Update()
    {
        setEnv();
    }

    public void setEnv(){
        if(EnvironmentController.envFlag && EnvironmentController.envStatus == 4){
            uguisu.SetActive(true);
            soundPlay("uguisu");

            // int animal = Random.Range(0, 4);

            // switch(animal){
            //     case 0:
            //         // uguisu.SetActive(true);
            //         soundPlay("uguisu");
            //         break;
            //     case 1:
            //         // bird.SetActive(true);
            //         soundPlay("bird");
            //         break;
            //     case 2:
            //         // hato.SetActive(true);
            //         soundPlay("hato");
            //         break;
            //     case 3:
            //         // frog.SetActive(true);
            //         soundPlay("frog");
            //         break;
            // }
        }
    }

    public void soundPlay(string seName)
    {
        switch (seName)
        {
            case "uguisu":
                envSound.clip = audioClips[0];
                envSound.Play();
                break;
            case "bird":
                envSound.clip = audioClips[1];
                envSound.Play();
                break;
            case "hato":
                envSound.clip = audioClips[2];
                envSound.Play();
                break;
            case "frog":
                envSound.clip = audioClips[3];
                envSound.Play();
                break;
        }
    }

    public void soundStop(string seName)
    {
        envSound.Stop();
    }
}