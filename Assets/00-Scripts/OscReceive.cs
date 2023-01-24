using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscReceive : MonoBehaviour
{
    public static int oscNum;
    public static string oscStatus;

    void Start()
    {
        var server = GetComponent<uOSC.uOscServer>();
        server.onDataReceived.AddListener(OnDataReceived);
    }

    public void OnDataReceived(uOSC.Message message)
    {

        // foreach (var value in message.values)
        // {
        //     if(value is int) oscNum = (int)value;
        // }


        if(message.address == "/Num"){
            foreach (var value in message.values)
            {
                if(value is int) oscNum = (int)value;
            }
        }else if(message.address == "/Status"){
            foreach (var value in message.values)
            {
                if (value is string) oscStatus =  (string)value;
            }

            // Debug.Log(oscStatus);
        }
    }
}
