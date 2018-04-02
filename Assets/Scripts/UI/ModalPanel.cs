using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanel : MonoBehaviour {

    public Text question;
    public Image iconImage;
    public Button firstButtonInPanel;
    public Button secondButtonInPanel;
    public Button thirdButtonInPanel;
    public GameObject modalPanelObject;

    private static ModalPanel modalPanel;

    public static ModalPanel Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
            if (!modalPanel)
            {
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
            }
        }

        return modalPanel;
    }

    // Yes/No/Cancel: A string, a Yes event, a No event, and a Cancel event
    public void Choice(string question, UnityAction firstButtonEvent, UnityAction secondButtonEvent, UnityAction thirdButtonEvent)
    {
        modalPanelObject.SetActive(true);

        firstButtonInPanel.onClick.RemoveAllListeners();
        firstButtonInPanel.onClick.AddListener(firstButtonEvent);
        firstButtonInPanel.onClick.AddListener(ClosePanel);

        secondButtonInPanel.onClick.RemoveAllListeners();
        secondButtonInPanel.onClick.AddListener(secondButtonEvent);
        secondButtonInPanel.onClick.AddListener(ClosePanel);

        thirdButtonInPanel.onClick.RemoveAllListeners();
        thirdButtonInPanel.onClick.AddListener(thirdButtonEvent);
        thirdButtonInPanel.onClick.AddListener(ClosePanel);

        this.question.text = question;

        this.iconImage.gameObject.SetActive(false);
        firstButtonInPanel.gameObject.SetActive(true);
        secondButtonInPanel.gameObject.SetActive(true);
        thirdButtonInPanel.gameObject.SetActive(true);
    }

    void ClosePanel()
    {
        modalPanelObject.SetActive(false);
    }
}
