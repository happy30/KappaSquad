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
	public StatsManager stats;

    public Text shurikenAmountText;
    public Text smokeBombAmountText;
    public GameObject shurikenAmountCircle;
    public GameObject smokeBombAmountCircle;

    public GameObject shurikenHotkeyObject;
    public GameObject grapplingHookHotkeyObject;
    public GameObject smokeBombHotkeyObject;

    public GameObject slashIcon;
    public GameObject shurikenIcon;
    public GameObject grapplingHookIcon;
    public GameObject smokeBombIcon;
    public GameObject dashIcon;

    public GameObject[] lockedIcons;
	
	void Awake()
	{
		stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
	}

    void Start()
    {
        UnlockIcons();
    }

    void Update()
    {
        CountConsumeables();
    }
	/*public int shurikenAmount;
	public Sprite locked;
	public Sprite unlocked;
	
	public void Awake () 
	{
		
	
	}*/
	
	
	//Keeps count of shurikens on screen
	void CountConsumeables()
	{
        if(shurikenAmountCircle.activeSelf)
        {
            shurikenAmountText.text = stats.shurikenAmount.ToString();
        }
        
        if(smokeBombAmountCircle.activeSelf)
        {
            smokeBombAmountText.text = stats.smokeBombAmount.ToString();
        }
        
		
	}

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

    public void UnlockIcons()
    {
        if (stats.katanaUnlocked)
        {
            
        }
        if (stats.shurikenUnlocked)
        {
            lockedIcons[0].SetActive(false);
            shurikenAmountCircle.SetActive(true);
            shurikenHotkeyObject.SetActive(true);
        }
        if (stats.grapplingHookUnlocked)
        {
            lockedIcons[1].SetActive(false);
            grapplingHookHotkeyObject.SetActive(true);
        }
        if (stats.smokeBombUnlocked)
        {
            smokeBombAmountCircle.SetActive(true);
            lockedIcons[2].SetActive(false);
            smokeBombHotkeyObject.SetActive(true);
        }
    }

}
