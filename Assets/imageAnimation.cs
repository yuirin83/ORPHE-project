using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageAnimation : MonoBehaviour
{
    private InnController inn;

    void Start()
    {
        inn = GameObject.Find("InterfaceController").GetComponent<InnController>();
    }

    void Update()
    {
        if(!inn.inputReady){
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);;
            this.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
        }else{
            this.transform.Rotate(new Vector3(0, 0, 28) * Time.deltaTime);
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
