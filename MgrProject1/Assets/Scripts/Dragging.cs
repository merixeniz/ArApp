using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragging : MonoBehaviour
{

    private Vector3 dist;
    private float posX;
    private float posY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        dist = Camera.main.WorldToScreenPoint(transform.localPosition);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;         

    }

    void OnMouseDrag()
    {
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);

        Vector3 worldPos = Camera.main.ScreenToViewportPoint(curPos);
        transform.position = worldPos;
    }
}
