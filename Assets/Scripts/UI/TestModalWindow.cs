using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TestModalWindow : MonoBehaviour {

    private ModalPanel modalPanel;
    private DisplayManager displayManager;

    private UnityAction myFirstAction;
    private UnityAction mySecondAction;
    private UnityAction myThirdAction;

    void Awake()
    {
        modalPanel = ModalPanel.Instance();
        displayManager = DisplayManager.Instance();

        myFirstAction = new UnityAction(TestFirstButtonFunction);
        mySecondAction = new UnityAction(TestSecondButtonFunction);
        myThirdAction = new UnityAction(TestThirdButtonFunction);
    }
    
    // Send to the Modal Panel to set up the Buttons and Functions to call
    public void Test123()
    {
        modalPanel.Choice("Would you like a poke in the eye?\nHow about with a sharp stick?", myFirstAction, mySecondAction, myThirdAction);
    }

    // These are wrapped into UnityActions
    void TestFirstButtonFunction()
    {
        displayManager.DisplayMessage("Heck yeah!");
    }

    void TestSecondButtonFunction()
    {
        displayManager.DisplayMessage("No way, Jose!");
    }

    void TestThirdButtonFunction()
    {
        displayManager.DisplayMessage("I give up!");
    }
}
