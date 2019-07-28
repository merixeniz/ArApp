using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFDiodeScript : MonoBehaviour
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
            lerpedColor = Color.Lerp(Color.black, Color.red, Mathf.PingPong(Time.time, 0.75f));
            rend.material.SetColor("_Color", lerpedColor);
        }

        else if (state == State.BlinkingFast)
        {
            lerpedColor = Color.Lerp(Color.black, Color.red, Mathf.PingPong(Time.time, 0.35f));
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
                diodes.SF = 1;
                //diodes.IgnoreUI = true;
                //Debug.Log("Current state: " + state);

                break;

            case State.On:
                state = State.Blinking;
                colorChanged = false;
                diodes.SF = 2;
                //Debug.Log("Current state: " + state);

                break;

            case State.Blinking:
                colorChanged = false;
                state = State.BlinkingFast;
                diodes.SF = 3;
                break;

            case State.BlinkingFast:
                state = State.Off;
                colorChanged = false;
                diodes.SF = 0;
                //diodes.IgnoreUI = false;
                //Debug.Log("Current state: " + state);

                break;

        }

        switch (diodes.SF)
        {
            case 0:
                //GuideText.text += Environment.NewLine;
                GuideText.text = "No error";
                GuideText.color = Color.green;
                GuideText.fontSize = 30;
                break;
            case 1:
                //GuideText.text += Environment.NewLine;
                GuideText.text = "Serious error (class 3)";
                GuideText.color = Color.red;
                GuideText.fontSize = 30;
                break;
            case 2:
                //GuideText.text += Environment.NewLine;
                GuideText.text = "Minor Error (class 1)";
                GuideText.color = Color.red;
                GuideText.fontSize = 30;
                break;
            case 3:
                GuideText.text = "Error (class 2)";
                GuideText.color = Color.red;
                GuideText.fontSize = 30;
                break;
        }

        //diodes.IgnoreUI = true;

        //Debug.Log("Error state:" + diodes.Error);
    }
}
