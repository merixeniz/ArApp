using Lean.Touch;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainSceneController : MonoBehaviour
{
    #region Public Variables

    public MainSceneController Instance;    
    public GameObject Panel;
    public GameObject ServicePanel;
    public GameObject GuidePanel;
    public GameObject PanelHelp;
    public GameObject ErrorPanel;
    public GameObject SplashScreen;
    public GameObject CPX;
    public GameObject[] CPXArrows;
    public GameObject[] ModulesText;
    public GameObject ScrewDriverIMG;
    public Text GuideText;
    public Toggle RotationCheckBox;
    public Toggle ScallingCheckBox;
    public InputField InputErrCode;

    #endregion

    #region Private Variables
    private Synoptics diodes;
    
    private Animator panelAnim;
    private Animator CPXAnim;
    private Animator servicePanelAnim;
    private Animator guidePanelAnim;
    private Animator panelHelpAnim;
    private Animator errorPanelAnim;

    private bool swiped;
    private bool tapped;
    private bool CPXdisassembly = false;
    private bool fingerHeld = false;    
    private bool helpPanelOn = false;
    private bool errorPanelOn = false;

    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        CPXAnim = CPX.GetComponent<Animator>();
        panelAnim = Panel.GetComponent<Animator>();        
        servicePanelAnim = ServicePanel.GetComponent<Animator>();
        guidePanelAnim = GuidePanel.GetComponent<Animator>();
        panelHelpAnim = PanelHelp.GetComponent<Animator>();
        errorPanelAnim = ErrorPanel.GetComponent<Animator>();

        foreach (GameObject arrow in CPXArrows)
        {
            arrow.SetActive(false);
        }

        foreach (GameObject ModulesTxt in ModulesText)
        {
            ModulesTxt.SetActive(false);
        }

        diodes = Synoptics.CreateSynoptics();

        RotationCheckBox.enabled = true;
        ScallingCheckBox.enabled = true;

        ScrewDriverIMG.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (diodes.LoadService == true)
        {
            LoadServiceScene();
            diodes.LoadService = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ECheckRotation());

        StartCoroutine(ECheckScalling());        
    }

    #region User interaction events

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

        if (!diodes.IgnoreUI)
        {
            tapped = !tapped;

            if (tapped)
                panelAnim.SetTrigger("PanelAnimation");

            else if (!tapped)
                panelAnim.SetTrigger("PanelAnimationBack");
        }

        //diodes.IgnoreUI = false;
    }

    #endregion

    #region Panel managment

    public void LoadServiceScene()
    {
        servicePanelAnim.SetTrigger("ServicePanelOn");
        diodes.IgnoreUI = true;
    }


    public void ServicePanelOn()
    {
        if (CPXdisassembly)
            AssemblyMethod();

       panelAnim.SetTrigger("PanelAnimationBack");
       servicePanelAnim.SetTrigger("ServicePanelOn");

        diodes.IgnoreUI = true;
    } 

    public void ServicePanelOff()
    {
        foreach (GameObject ModulesTxt in ModulesText)
        {
            ModulesTxt.SetActive(false);
        }

        foreach (GameObject Arrow in CPXArrows)
        {
            Arrow.SetActive(false);
        }

        CPXAnim.SetTrigger("CPXSetIdleStep2");
        CPXAnim.SetTrigger("CPXSetIdleStep3");

        servicePanelAnim.SetTrigger("ServicePanelOff");
        guidePanelAnim.SetTrigger("GuidePanelOff");
        panelAnim.SetTrigger("PanelAnimation");

        ScrewDriverIMG.SetActive(false);

        diodes.IgnoreUI = false;
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

    public void ErrorPanelOn()
    {
        if (CPXdisassembly)
            AssemblyMethod();

        GuideText.text = "Enter your error code or click on synoptic diodes";
        GuideText.fontSize = 30;
        GuideText.color = Color.black;

        errorPanelAnim.SetTrigger("ErrorPanelOn");
        guidePanelAnim.SetTrigger("GuidePanelOn");
        panelAnim.SetTrigger("PanelAnimationBack");

        diodes.IgnoreUI = true;
    }

    public void ErrorPanelOff()
    {
        GuideText.text = " ";
        errorPanelAnim.SetTrigger("ErrorPanelOff");
        guidePanelAnim.SetTrigger("GuidePanelOff");
        panelAnim.SetTrigger("PanelAnimation");

        diodes.IgnoreUI = false;
        ScrewDriverIMG.SetActive(false);
    }
    #endregion

    #region Scene Manager

    public void SwitchScene(int scene)
    {
        SceneManager.LoadScene(scene);
        diodes.IgnoreUI = false;
    }

    #endregion

    #region Assembly/Disassembly methods

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

            foreach(GameObject Arrow in CPXArrows)
            {
                Arrow.SetActive(false);
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

    #endregion
    
    #region ServiceAnims

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
                GuideText.text = "Make sure that air and electric supply is OFF!" + Environment.NewLine + "Unscrew these two screws";
                ScrewDriverIMG.SetActive(true);
                GuideText.fontSize = 30;
                               
                break;
            case 2:
                Debug.Log("step nr 2");
                CPXArrows[0].SetActive(false);
                CPXArrows[1].SetActive(false);
                CPXArrows[2].SetActive(true);

                guidePanelAnim.SetTrigger("GuidePanelOn");
                CPXAnim.SetTrigger("CPXSetIdleStep3");

                CPXAnim.SetTrigger("CPXArrow3");
                CPXAnim.SetTrigger("CPXValve4ServicePull");
                GuideText.text = "Pull valve";
                ScrewDriverIMG.SetActive(false);

                break;
            case 3:
                Debug.Log("step nr 3");

                foreach (GameObject arrow in CPXArrows)
                {
                    arrow.SetActive(false);
                }

                guidePanelAnim.SetTrigger("GuidePanelOn");
                CPXAnim.SetTrigger("CPXSetIdleStep2");
                CPXAnim.SetTrigger("CPXValve4Off");
                GuideText.text = "Take the valve off";
                ScrewDriverIMG.SetActive(false);

                break;
            case 4:
                Debug.Log("step nr 4");

                foreach (GameObject arrow in CPXArrows)
                {
                    arrow.SetActive(false);
                }

                guidePanelAnim.SetTrigger("GuidePanelOn");
                CPXAnim.SetTrigger("CPXSetIdleStep2");
                CPXAnim.SetTrigger("CPXSetIdleStep3");


                CPXAnim.SetTrigger("CPXStep4");

                GuideText.text = "Put in new valve";
                ScrewDriverIMG.SetActive(false);
                break;
            case 5:
                Debug.Log("step nr 5");

                CPXArrows[0].SetActive(true);
                CPXArrows[1].SetActive(true);
                CPXArrows[2].SetActive(false);

                guidePanelAnim.SetTrigger("GuidePanelOn");
                CPXAnim.SetTrigger("CPXSetIdleStep2");
                CPXAnim.SetTrigger("CPXSetIdleStep3");

                CPXAnim.SetTrigger("CPXArrows12");
                
                GuideText.text = "Screw in mounting screws ";
                ScrewDriverIMG.SetActive(true);
                break;
        }
    }

    #endregion

    public void CheckSynopticsValues()
    {
        Debug.Log("Error: " + diodes.Error);
    }

    #region Checkboxes 

    public IEnumerator ECheckScalling()
    {
        yield return new WaitForSeconds(1);
        
        if (ScallingCheckBox.isOn)
            CPX.GetComponent<Scalling>().enabled = true;

        else if (!ScallingCheckBox.isOn)
            CPX.GetComponent<Scalling>().enabled = false;

        //Debug.Log("Rotation CheckBox value isOn: " + RotationCheckBox.isOn);
    }
    
    public IEnumerator ECheckRotation()
    {
        yield return new WaitForSeconds(1);
              

        if (RotationCheckBox.isOn)
            CPX.GetComponent<LeanRotateCustomAxis>().enabled = true;

        else if (!RotationCheckBox.isOn)
            CPX.GetComponent<LeanRotateCustomAxis>().enabled = false;

        //Debug.Log("Rotation CheckBox value isOn: " + RotationCheckBox.isOn);
    }

    #endregion

    #region ErrorCodes

    public void ErrorCodesCheck()
    {
        switch (int.Parse(InputErrCode.text))
        {
            case 0:
                GuideText.text = "Reserved";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 1:
                GuideText.text = "Short circuit";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
            break;
            case 2:
                GuideText.text = "Undervoltage";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 3:
                GuideText.text = "Overvoltage";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 4:
                GuideText.text = "Overload";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 5:
                GuideText.text = "Overheating";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 6:
                GuideText.text = "Cable break";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 7:
                GuideText.text = "Upper limit value exceeded";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 8:
                GuideText.text = "Lower limit value exceeded";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 9:
                GuideText.text = "Reserved";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 10:
                GuideText.text = "Reserved";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 11:
                GuideText.text = "Reserved";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 12:
                GuideText.text = "Reserved";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 13:
                GuideText.text = "Reserved";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 14:
                GuideText.text = "Reserved";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 15:
                GuideText.text = "Reserved";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 16:
                GuideText.text = "Incorrect value setting";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 17:
                GuideText.text = "Valve: Switching counter, limit value exceeded";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 18:
                GuideText.text = "Reserved";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 19:
                GuideText.text = "Reserved";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 20:
                GuideText.text = "Parametrisation fault (configurable)";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 21:
                GuideText.text = "Parametrisation fault (data format)";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 22:
                GuideText.text = "Parametrisation fault (linear scalling)";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 23:
                GuideText.text = "Parametrisation fault (digital filter)";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 24:
                GuideText.text = "Parametrisation fault (lower limit value)";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 25:
                GuideText.text = "Parametrisation fault (upper limit value)";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 26:
                GuideText.text = "Defective actuator supply";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 27:
                GuideText.text = "CP module failure";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 28:
                GuideText.text = "Defective CP configuration";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 29:
                GuideText.text = "Short circuit in the CP string (CP line)";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 30:
                GuideText.text = "Slave has no bus connection";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            case 31:
                GuideText.text = "Channel failed";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;
            default:
                GuideText.text = "Error code has not been found in database";
                GuideText.color = Color.yellow;
                GuideText.fontSize = 30;
                break;

        }
        
    }

    #endregion

    [Obsolete]
    public IEnumerator ECheckSynoptics() //not in use
    {
        yield return new WaitForSeconds(2);

        switch (diodes.Run)
        {
            case 1:

                GuideText.text = Environment.NewLine;
                GuideText.text += "Diode Run is On";
                GuideText.color = Color.green;
                GuideText.fontSize = 20;
                break;
        }
    }
}
