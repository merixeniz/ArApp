using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLDiodeScript : MonoBehaviour
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
                state = State.On;
                colorChanged = true;
                diodes.PL = 1;
                //diodes.IgnoreUI = true;
                //Debug.Log("Current state: " + state);

                break;

            case State.On:
                state = State.Blinking;
                diodes.PL = 2;
                //Debug.Log("Current state: " + state);

                break;

            case State.Blinking:
                state = State.Off;
                colorChanged = false;
                diodes.PL = 0;
                //diodes.IgnoreUI = false;
                //Debug.Log("Current state: " + state);

                break;

        }

        switch (diodes.PL)
        {
            case 0:
                //GuideText.text += Environment.NewLine;
                GuideText.text = " ";
                break;
            case 1:
                //GuideText.text += Environment.NewLine;
                GuideText.text = "No error. Load voltage present";
                GuideText.color = Color.green;
                GuideText.fontSize = 30;
                break;
            case 2:
                //GuideText.text += Environment.NewLine;
                GuideText.text = "Load voltage at the system supply or additional supply outside the tolerance range" + Environment.NewLine + "Eliminate undervoltage";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
        }

        //diodes.IgnoreUI = true;

        //Debug.Log("Error state:" + diodes.Error);
    }
}
