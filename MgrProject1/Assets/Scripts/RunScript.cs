using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunScript : MonoBehaviour
{
    public GameObject Diode;
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        //Renderer rend = Diode.GetComponent<Renderer>();
        //rend.material.shader = Shader.Find("SphereMaterial");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //Debug.Log("Run pressed");
        //rend.material.SetColor("SphereMaterial", Color.green);
        
    }
}
