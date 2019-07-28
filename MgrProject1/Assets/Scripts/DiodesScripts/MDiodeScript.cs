using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MDiodeScript : MonoBehaviour
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
            lerpedColor = Color.Lerp(Color.black, Color.yellow, Mathf.PingPong(Time.time, 0.7f));
            rend.material.SetColor("_Color", lerpedColor);
        }

        else
        {
            if (colorChanged)
                rend.material.SetColor("_Color", Color.yellow);


            else if (!colorChanged)
                rend.material.SetColor("_Color", Color.black);
        }
    }

    private void OnMouseDown()
    {
        switch (state)
        {
            case State.Off:
                state = State.Blinking;
                diodes.M = 2;
                //Debug.Log("Current state: " + state);

                break;

            //case State.On:
            //    state = State.Blinking;
            //    diodes.M = 2;
            //    //Debug.Log("Current state: " + state);

            //    break;

            case State.Blinking:
                state = State.Off;
                colorChanged = false;
                diodes.M = 0;
                //diodes.IgnoreUI = false; 
                //Debug.Log("Current state: " + state);
                break;

        }

        switch (diodes.M)
        {
            case 0:
                //GuideText.text += Environment.NewLine;
                GuideText.text = "Forcing is not active";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 1:
                //GuideText.text += Environment.NewLine;
                GuideText.text = "Diode M is On";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 2:
                //GuideText.text += Environment.NewLine;
                GuideText.text = "Forcing is active" + Environment.NewLine + "The Force function is enabled (see CPX system manual, system parameter Force mode; function no. 4402).";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
        }

        //diodes.IgnoreUI = true;

        //Debug.Log("Error state:" + diodes.Error);
    }
}
