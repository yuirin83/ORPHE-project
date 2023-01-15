using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscReceive : MonoBehaviour
{
    public static int oscNum;
    void Start()
    {
        var server = GetComponent<uOSC.uOscServer>();
        server.onDataReceived.AddListener(OnDataReceived);
    }

    public void OnDataReceived(uOSC.Message message)
    {

        foreach (var value in message.values)
        {
            if(value is int) oscNum = (int)value;
        }

        // Debug.Log(oscNum);
    }
}
