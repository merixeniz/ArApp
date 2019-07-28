using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller : MonoBehaviour
{
    public GameObject obj;

    public IRotateScript Rotate;

    public void Awake()
    {
        Rotate = GetComponent<RotateScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate.rotate(obj); // wywoluje funkcje rotate cyklicznie        
    }

    public void Swiped()
    {
        Debug.Log("you just swiped");
    }

    public void Taped()
    {
        Debug.Log("You just tapped");
    }

    public void FingerHold()
    {
        Debug.Log("You hold finger for 3 seconds already!");
    }
}
