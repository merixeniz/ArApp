using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scalling : MonoBehaviour
{

    public float initialFingersDistance;
    public Vector3 initialScale;
    public static Transform ScaleTransform;

    // Start is called before the first frame update
    void Start()
    {
        ScaleTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        int fingersOnScreen = 0; 

        foreach (Touch touch in Input.touches) 
        {
            fingersOnScreen++;
            Debug.Log("Fingers on Screen: " + fingersOnScreen.ToString());
            if (fingersOnScreen == 2)
            { 
                if (touch.phase == TouchPhase.Began)
                {
                    initialFingersDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                    initialScale = ScaleTransform.localScale;
                }
                else
                {
                    float currentFingersDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);

                    float scaleFactor = currentFingersDistance / initialFingersDistance;

                    ScaleTransform.localScale = initialScale * scaleFactor;
                }
            }
        }

        
    }
}
