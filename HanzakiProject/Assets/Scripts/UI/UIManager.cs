using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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
    public QuestManager quests;
    public ProgressionManager prog;

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

    public Text mainQuestTitleText;
    public Text[] mainQuestTasksText;
    public RectTransform questArrow;

    public Text sideQuestText;

    public AudioClip questCompleted;
    public AudioClip unlockAbilitySound;
    public AudioClip pickUpSound;

    public GameObject pickUpText;
    public GameObject unlockAbility;
	
	void Awake()
	{
		stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        quests = GameObject.Find("GameManager").GetComponent<QuestManager>();
        prog = GameObject.Find("GameManager").GetComponent<ProgressionManager>();
        _sound = GetComponent<AudioSource>();

    
	}

    void Start()
    {
        UnlockIcons();
        SetQuestsText();
    }

    void Update()
    {
        CountConsumeables();
    }
	
    public void SetQuestsText()
    {
        mainQuestTitleText.text = quests.mainQuests[prog.mainQuestProgression].questTitle;
        for(int i = 0; i < mainQuestTasksText.Length; i++)
        {
            if (i <= quests.mainQuests[prog.mainQuestProgression].atTask)
            {
                mainQuestTasksText[i].text = quests.mainQuests[prog.mainQuestProgression].questTasks[i];
                if(i > 0)
                {
                    mainQuestTasksText[i - 1].text = StrikeThrough(quests.mainQuests[prog.mainQuestProgression].questTasks[i - 1]);
                }
                

                questArrow.anchoredPosition = new Vector2(questArrow.anchoredPosition.x, 56 - (56 * quests.mainQuests[prog.mainQuestProgression].atTask));
                
            }
            else
            {
                mainQuestTasksText[i].text = "";
            }
        }
        if(quests.mainQuests[prog.mainQuestProgression].atTask > 0)
        {
            _sound.PlayOneShot(questCompleted);
        }
    }

    public string StrikeThrough(string s)
    {
        string strikethrough = "";
        foreach (char c in s)
        {
            strikethrough = strikethrough + c + '\u0336';
        }
        return strikethrough;
    }


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

    //Play PickUp Animation
    public void PickUp(string text)
    {
        pickUpText.GetComponent<Text>().text = "You Picked Up a " + text + "!";
        pickUpText.GetComponent<Animator>().SetTrigger("PickUp");
        _sound.PlayOneShot(pickUpSound);
    }

    //Play ability Unlocked Animation
    public void UnlockAbility()
    {
        unlockAbility.GetComponent<Animator>().SetTrigger("Unlock");
        _sound.PlayOneShot(unlockAbilitySound, 0.7f);
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
