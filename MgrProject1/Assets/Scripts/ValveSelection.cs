using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveSelection : MonoBehaviour
{
    private GameObject[] valveList;
    private int currentValve = 0;
    // Start is called before the first frame update
    void Start()
    {
        // tworzenie dynamicznej tablicy x elementowej
        valveList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            valveList[i] = transform.GetChild(i).gameObject;
            Debug.Log("valvelist[i] value: " +  valveList[i]);            
        }

        foreach (GameObject valve in valveList)
        {
            valve.SetActive(false);
        }

        if (valveList[0])
            valveList[0].SetActive(true);

        Debug.Log(valveList.Length.ToString());
    }
       

    public void ToggleValves(bool direction)
    {
        valveList[currentValve].SetActive(false);

        if (direction == true)
        {
            currentValve++;
            
            if (currentValve >= valveList.Length)
            {
                currentValve = 0;
            }
            
        }
        else if (direction == false)
        {
            currentValve--;
            
            if (currentValve < 0)
            {
                currentValve = valveList.Length - 1;
            }
        }

        valveList[currentValve].SetActive(true);
    }

    public void HideValves()
    {
        foreach (GameObject valve in valveList)
        {
            valve.SetActive(false);
        }
    }

}
