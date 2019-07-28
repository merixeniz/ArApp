using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorDiodeScript : MonoBehaviour
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
            lerpedColor = Color.Lerp(Color.black, Color.red, Mathf.PingPong(Time.time, 0.7f));
            rend.material.SetColor("_Color", lerpedColor);
        }

        else
        {
            if (colorChanged)
                rend.material.SetColor("_Color", Color.red);


            else if (!colorChanged)
                rend.material.SetColor("_Color", Color.black);
        }
    }

    private void OnMouseDown()
    {
        switch (state)
        {
            case State.Off:
                state = State.On;
                colorChanged = true;
                diodes.Error = 1;
                //diodes.IgnoreUI = true;
                //Debug.Log("Current state: " + state);
                break;

            case State.On:
                state = State.Off;
                colorChanged = false;
                diodes.Error = 0;
                //diodes.IgnoreUI = false;
                break;

            //case State.Blinking:
            //    state = State.Off;
            //    colorChanged = false;
            //    diodes.Error = 0;
            //    diodes.IgnoreUI = false;
            //    //Debug.Log("Current state: " + state);

            //    break;
        }

        switch (diodes.Error)
        {
            case 0:
                //GuideText.text += Environment.NewLine;
                GuideText.text = "No error";
                GuideText.color = Color.red;
                GuideText.fontSize = 30;
                break;
            case 1:
                //GuideText.text += Environment.NewLine;
                GuideText.text = "PLC program error" + Environment.NewLine + "Read out error code via hanheld or Codesys";
                GuideText.color = Color.red;
                GuideText.fontSize = 30;
                break;
            case 2:
                //GuideText.text += Environment.NewLine;
                GuideText.text = "Diode Error is Blinking";
                GuideText.color = Color.red;
                GuideText.fontSize = 20;
                break;
        }

        //diodes.IgnoreUI = true;

        //Debug.Log("Error state:" + diodes.Error);
    }
}
