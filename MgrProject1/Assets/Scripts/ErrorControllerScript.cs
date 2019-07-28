using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ErrorControllerScript : MonoBehaviour
{
    public ErrorControllerScript Instance;
    public GameObject Panel;
    public GameObject ServicePanel;
    public GameObject GuidePanel;
    public GameObject PanelHelp;
    public GameObject SplashScreen;
    public GameObject CPX;
    public GameObject[] CPXArrows;
    public GameObject[] ModulesText;
    public Text GuideText;

    private Animator panelAnim;
    private Animator CPXAnim;
    private Animator servicePanelAnim;
    private Animator guidePanelAnim;
    private Animator panelHelpAnim;

    private bool swiped;
    private bool tapped;
    private bool CPXdisassembly = false;
    private bool fingerHeld = false;
    private bool step1, step2, step3, step4, step5 = false;
    private bool helpPanelOn = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        CPXAnim = CPX.GetComponent<Animator>();
        panelAnim = Panel.GetComponent<Animator>();
        servicePanelAnim = ServicePanel.GetComponent<Animator>();
        guidePanelAnim = GuidePanel.GetComponent<Animator>();
        panelHelpAnim = PanelHelp.GetComponent<Animator>();

        foreach (GameObject arrow in CPXArrows)
        {
            arrow.SetActive(false);
        }

        foreach (GameObject ModulesTxt in ModulesText)
        {
            ModulesTxt.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Swiped()
    {
        Debug.Log("you just swiped");
        //swiped = !swiped;

        //if (swiped)
        //    panelAnim.SetTrigger("PanelAnimation");

        //if (!swiped)
        //    panelAnim.SetTrigger("PanelAnimationBack");
    }

    public void Tapped()
    {
        Debug.Log("You just tapped");

        tapped = !tapped;

        if (tapped)
            panelAnim.SetTrigger("PanelAnimation");

        else if (!tapped)
            panelAnim.SetTrigger("PanelAnimationBack");
    }

    public void ServicePanelOn()
    {
        if (CPXdisassembly)
            AssemblyMethod();

        panelAnim.SetTrigger("PanelAnimationBack");
        servicePanelAnim.SetTrigger("ServicePanelOn");
    }

    public void ServicePanelOff()
    {
        AssemblyMethod();

        servicePanelAnim.SetTrigger("ServicePanelOff");
        panelAnim.SetTrigger("PanelAnimation");
    }

    public void HelpPanelMethod()
    {
        helpPanelOn = !helpPanelOn;

        if (helpPanelOn)
        {
            panelHelpAnim.SetTrigger("PanelHelpOn");
        }

        else if (!helpPanelOn)
        {
            panelHelpAnim.SetTrigger("PanelHelpOff");
        }


    }




    public void SwitchScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void DisassemblyMethod()
    {
        if (!CPXdisassembly)
        {
            foreach (GameObject ModulesTxt in ModulesText)
            {
                ModulesTxt.SetActive(true);
            }

            CPXAnim.SetTrigger("CPXModule1Off");
            CPXAnim.SetTrigger("CPXModule2Off");
            CPXAnim.SetTrigger("CPXModule3Off");
            CPXAnim.SetTrigger("CPXModule5Off");
            CPXAnim.SetTrigger("CPXValve1Off");
            CPXAnim.SetTrigger("CPXValve2Off");
            CPXAnim.SetTrigger("CPXValve3Off");
            CPXAnim.SetTrigger("CPXValve4Off");

            CPXdisassembly = true;
        }
    }

    public void AssemblyMethod()
    {
        if (CPXdisassembly)
        {
            foreach (GameObject ModulesTxt in ModulesText)
            {
                ModulesTxt.SetActive(false);
            }

            CPXAnim.SetTrigger("CPXModule1On");
            CPXAnim.SetTrigger("CPXModule2On");
            CPXAnim.SetTrigger("CPXModule3On");
            CPXAnim.SetTrigger("CPXModule5On");
            CPXAnim.SetTrigger("CPXValve1On");
            CPXAnim.SetTrigger("CPXValve2On");
            CPXAnim.SetTrigger("CPXValve3On");
            CPXAnim.SetTrigger("CPXValve4On");

            CPXdisassembly = false;
        }
    }

    public void ServiceAnims(int step)
    {
        switch (step)
        {
            case 1:
                Debug.Log("step nr 1");
                // to do if czy obiekt jest aktywny na scenie
                CPXArrows[0].SetActive(true);
                CPXArrows[1].SetActive(true);
                CPXArrows[2].SetActive(false);

                CPXAnim.SetTrigger("CPXSetIdleStep2");
                CPXAnim.SetTrigger("CPXSetIdleStep3");

                CPXAnim.SetTrigger("CPXArrows12");
                guidePanelAnim.SetTrigger("GuidePanelOn");
                GuideText.text = "Make sure that air and electric supply is OFF!" + Environment.NewLine + "Unscrew two screws";
                GuideText.fontSize = 30;


                break;
            case 2:
                Debug.Log("step nr 2");
                CPXArrows[0].SetActive(false);
                CPXArrows[1].SetActive(false);
                CPXArrows[2].SetActive(true);

                CPXAnim.SetTrigger("CPXSetIdleStep3");

                CPXAnim.SetTrigger("CPXArrow3");
                CPXAnim.SetTrigger("CPXValve4ServicePull");
                GuideText.text = "Pull valve";

                break;
            case 3:
                Debug.Log("step nr 3");

                foreach (GameObject arrow in CPXArrows)
                {
                    arrow.SetActive(false);
                }

                CPXAnim.SetTrigger("CPXSetIdleStep2");

                CPXAnim.SetTrigger("CPXValve4Off");

                GuideText.text = "Take the valve off";

                break;
            case 4:
                Debug.Log("step nr 4");

                foreach (GameObject arrow in CPXArrows)
                {
                    arrow.SetActive(false);
                }

                CPXAnim.SetTrigger("CPXSetIdleStep2");
                CPXAnim.SetTrigger("CPXSetIdleStep3");


                CPXAnim.SetTrigger("CPXStep4");

                GuideText.text = "Put in new valve";
                break;
            case 5:
                Debug.Log("step nr 5");

                CPXArrows[0].SetActive(true);
                CPXArrows[1].SetActive(true);
                CPXArrows[2].SetActive(false);

                CPXAnim.SetTrigger("CPXSetIdleStep2");
                CPXAnim.SetTrigger("CPXSetIdleStep3");

                CPXAnim.SetTrigger("CPXArrows12");

                GuideText.text = "Screw in mounting screws ";


                break;
        }
    }

    public void ChildrenOfCPX()
    {
        foreach (Transform child in CPX.transform)
        {
            if (child.name != "Module1New")
            {
                print("Foreach loop: " + child);
                child.GetComponent<GameObject>();                                
            }

        }
    }
}
