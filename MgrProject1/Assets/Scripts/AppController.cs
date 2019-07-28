using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppController : MonoBehaviour
{
    public AppController Instance;
    public static Vector2 SwipeRequire = new Vector2(2,0);

    private Synoptics diodes;
    
    private bool swiped;    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        diodes = Synoptics.CreateSynoptics();
    }
    // Start is called before the first frame update
    void Start()
    {
      // serviceOnLoad = GameObject.Find("ServiceOnLoadObj").GetComponent<ServiceOnLoad>();
      //panelAnim = Panel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitBtn()
    {
        Application.Quit();
    }

    public void OpenWebPage()
    {
        Application.OpenURL("www.festo.com");
    }

    public void SelectScene(int scene)
    {
        SceneManager.LoadScene(scene);
        diodes.IgnoreUI = false;
    }

    public void ServiceScene()
    {
        
        diodes.LoadService = true;
        SceneManager.LoadScene(1);
    }
    public void Swiped()
    {
        Debug.Log("you just swiped");
        swiped = !swiped;

        //if (swiped)
        //    panelAnim.SetTrigger("PanelAnimation");

        //if (!swiped)
        //    panelAnim.SetTrigger("PanelAnimationBack");
    }

    public void Taped()
    {
        Debug.Log("You just tapped");
    }

    public void FingerHeld()
    {
        Debug.Log("You hold finger for 3 seconds already!");
    }

    public void FingerTaped()
    {
        Debug.Log("You just tapped");
    }
    public void FingerTapedTwice()
    {
        Debug.Log("You just tapped twice");
    }
    public void Swiped2X()
    {
        Debug.Log("You just swiped 2 units on X");
    }
    public Vector2 RequiredSwipe()
    {
        return new Vector2(2, 0);
    }

    public void Selected()
    {
        Debug.Log("You have selected game object");
    }
}
