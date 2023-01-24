using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melaAnim : MonoBehaviour
{
    public GameObject[] mela = new GameObject[5];

    public GameObject[] mela_z = new GameObject[5];

    Animator[] animator = new Animator[5];

    Animator[] animator_z = new Animator[5];

    int myNum = 999;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            animator[i] = mela[i].GetComponent<Animator>();
            animator_z[i] = mela_z[i].GetComponent<Animator>();
            animator[i].SetBool("melaX", false);
            animator_z[i].SetBool("melaZ", false);

        }
    }

    void Update()
    {
        switch (OscReceive.oscStatus)
        {
            case "stop":
                myNum = 0;
                break;
            case "katonn":
                myNum = 1;
                break;
            case "suitonn":
                myNum = 2;
                break;
            case "mokutonn":
                myNum = 3;
                break;
            case "kuchiyose":
                myNum = 4;
                break;
            default:
                myNum = 999;
                for (int i = 0; i < 5; i++)
                {
                    animator[i].SetBool("melaX", false);
                    animator_z[i].SetBool("melaZ", false);
                }
                break;
        }

        if (myNum < 999)
        {
            melaAnimSet();
        }
    }

    void melaAnimSet()
    {
        animator[myNum].SetBool("melaX", true);
        animator_z[myNum].SetBool("melaZ", true);

        for (int i = 0; i < 5; i++){
            if (i != myNum){
                    animator[i].SetBool("melaX", false);
                    animator_z[i].SetBool("melaZ", false);
            }
        }
    }
}
