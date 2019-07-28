using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    Off = 0,
    On = 1,
    Blinking = 2,
    BlinkingFast = 3
}

public class RunDiodeScript : MonoBehaviour
{
    public Text GuideText;

    private Renderer rend;
    private bool colorChanged;
    private State state = State.Off;
    private Color lerpedColor = Color.black;

    private Synoptics diodes;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        diodes = Synoptics.CreateSynoptics();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Blinking)
        {
            lerpedColor = Color.Lerp(Color.black, Color.green, Mathf.PingPong(Time.time, 0.7f));
            rend.material.SetColor("_Color", lerpedColor);
        }
        
        else
        {
            if (colorChanged)
                rend.material.SetColor("_Color", Color.green);


            else if (!colorChanged)
                rend.material.SetColor("_Color", Color.black);
        }
    }

    private void OnMouseDown()
    {
        switch (state)
        {
            case State.Off:
                 state = State.On; // current state
                 colorChanged = true;
                 diodes.Run = 1;
                //diodes.IgnoreUI = true;
                //Debug.Log("Current state: " + state);
                break;

            case State.On:
                 state = State.Off;
                 colorChanged = false;
                 diodes.Run = 0;
                 //diodes.IgnoreUI = false;
                //Debug.Log("Current state: " + state);

                break;

            //case State.Blinking:
            //     state = State.Off;
            //     colorChanged = false;
            //     diodes.Run = 0;
            //     diodes.IgnoreUI = false;
            //     //Debug.Log("Current state: " + state);

            //    break;
                
        }

        switch (diodes.Run)
        {
            case 0:
                //GuideText.text += Environment.NewLine;
                GuideText.text = "PLC program stopped" + Environment.NewLine + "RUN/STOP switch set to position '0' ";
                GuideText.color = Color.green;
                GuideText.fontSize = 30;
                break;
            case 1:
                //GuideText.text += Environment.NewLine;
                GuideText.text = "PLC program started" + Environment.NewLine + "RUN/STOP switch set to position '1 ... F' ";
                GuideText.color = Color.green;
                GuideText.fontSize = 30;
                break;
            case 2:
                //GuideText.text += Environment.NewLine;
                GuideText.text = "Diode Run is Blinking";
                GuideText.color = Color.green;
                GuideText.fontSize = 30;
                break;
        }

        //diodes.IgnoreUI = true;
        
        //Debug.Log("Error state:" + diodes.Error);
    }    
}
