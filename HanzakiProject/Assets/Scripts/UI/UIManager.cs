using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject letterboxes;
    public GameObject topLetterbox;
    public GameObject bottomLetterbox;
    public GameObject chatPanel;

    public Text npcNameText;
    public Text cutsceneText;
    public Text interactText;

    public GameObject interactTextObject;
    public GameObject npcNameTextObject;

    AudioSource _sound;

    //Play the letterbox animation
    public void EnterCutscene()
    {
        topLetterbox.GetComponent<Animator>().SetBool("SlideIn", true);
        bottomLetterbox.GetComponent<Animator>().SetBool("SlideIn", true);
    }

    //Play the letterbox animation
    public void ExitCutscene()
    {
        topLetterbox.GetComponent<Animator>().SetBool("SlideIn", false);
        bottomLetterbox.GetComponent<Animator>().SetBool("SlideIn", false);
    }

    //Set the text in the chatpanel
    public void UpdateText(string name, string text)
    {
        if(name != "")
        {
            npcNameTextObject.GetComponent<Animator>().SetBool("FadeIn", true);
        }
        npcNameText.text = name;
        cutsceneText.text = text;
    }

    //Change the text that shows how to interact
    public void ChangeInteractText(InteractScript interactObject)
    {
        if(!Camera.main.GetComponent<CameraController>().inCutscene)
        {
            interactTextObject.GetComponent<Animator>().SetBool("FadeIn", true);
            interactText.text = "Press Z to " + interactObject.interactText;
        }
        else
        {
            interactText.text = "";
        }
    }

    //Hide the interact text
    public void RemoveInteractText()
    {
        //interactText.text = "";
        interactTextObject.GetComponent<Animator>().SetBool("FadeIn", false);
    }
}
