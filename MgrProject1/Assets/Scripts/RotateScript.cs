using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class RotateScript : MonoBehaviour, IRotateScript
{
    private float speed = 25f;
    private int fingersOnTouch = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime, 0, 0);
                      
    }

    public void rotate(GameObject obj)
    {
        Debug.Log("test test");
        obj.transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
