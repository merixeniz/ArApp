using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceOnLoad : MonoBehaviour
{
    public GameObject ServicePanel;

    private Animator servicePanelAnim;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        servicePanelAnim = ServicePanel.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TestCall()
    {
        Synoptics diodes = Synoptics.CreateSynoptics();

        servicePanelAnim.SetTrigger("ServicePanelOn");
        diodes.IgnoreUI = true;
    }
}
