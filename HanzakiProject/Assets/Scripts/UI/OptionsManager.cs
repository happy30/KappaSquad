using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class OptionsManager : MonoBehaviour
{

    public enum KeyBinding
    {
        Jump,
        Slash,
        Shuriken,
        GrapplingHook,
        SmokeBomb
    };

    public Text jumpKey;
    public Text slashKey;
    public Text shurikenKey;
    public Text grapplingHookKey;
    public Text smokeBombKey;

    public Text keyToChangeText;

    public KeyBinding keyBinding;
    public GameObject PressAnyKeyPanel;

    public RectTransform[] cursorPositions;
    public GameObject checkmark;
    public bool cursorCantMove;

    public enum CursorPositions
    {
        Hints,
        Music,
        BGM,
        SFX,
        Jump,
        Slash,
        Shuriken,
        GrapplingHook,
        SmokeBomb,
        Back
    };

    public GameObject cursorArrow;
    public CursorPositions cursorPos;
    public float cursorArrowSpeed;
    public MainMenuController mainMenuController;
    CanvasGroup _canvasGroup;
    public float optionsAlpha;

    public OptionsSettings optionSettings;

	// Use this for initialization
	void Awake()
    {
        optionSettings = GameObject.Find("GameManager").GetComponent<OptionsSettings>();
        mainMenuController = GameObject.Find("MainMenuCanvas").GetComponent<MainMenuController>();
        _canvasGroup = GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(InputManager.Slash))
        {
            if(cursorPos == CursorPositions.Jump)
            {
                KeyBindingButton(0);
            }
            else if (cursorPos == CursorPositions.Slash)
            {
                KeyBindingButton(1);
            }
            else if (cursorPos == CursorPositions.Shuriken)
            {
                KeyBindingButton(2);
            }
            else if (cursorPos == CursorPositions.GrapplingHook)
            {
                KeyBindingButton(3);
            }
            else if (cursorPos == CursorPositions.SmokeBomb)
            {
                KeyBindingButton(4);
            }
            else if (cursorPos == CursorPositions.Hints)
            {
                if(optionSettings.displayHints)
                {
                    checkmark.GetComponent<Image>().enabled = false;
                    optionSettings.displayHints = false;
                }
                else
                {
                    checkmark.GetComponent<Image>().enabled = true;
                    optionSettings.displayHints = true;
                }
            }
            


            else if (cursorPos == CursorPositions.Back)
            {
                mainMenuController.optionsOpen = false;
            }
        }


        if(mainMenuController.optionsOpen)
        {
            _canvasGroup.alpha = optionsAlpha;
            if(optionsAlpha < 1)
            {
                optionsAlpha += Time.deltaTime * 3;
            }
            else if(Input.GetButtonDown("Cancel"))
            {
                mainMenuController.optionsOpen = false;
            }
        }
        else
        {
            _canvasGroup.alpha = optionsAlpha;
            if (optionsAlpha > 0)
            {
                optionsAlpha -= Time.deltaTime * 3;
            }
            else
            {
                mainMenuController.OptionsBack();
                gameObject.SetActive(false);
            }
        }
        cursorArrow.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(cursorArrow.GetComponent<RectTransform>().anchoredPosition, new Vector2(cursorPositions[(int)cursorPos].anchoredPosition.x, cursorPositions[(int)cursorPos].anchoredPosition.y), cursorArrowSpeed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.DownArrow)  && !cursorCantMove)
        {
            if((int)cursorPos < 9)
            {
                if((int)cursorPos == 3)
                {
                    cursorPos = (CursorPositions)9;
                }
                else
                {
                    cursorPos++;
                }
                
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !cursorCantMove)
        {
            if ((int)cursorPos > 0)
            {
                if((int)cursorPos == 9)
                {
                    cursorPos = (CursorPositions)3;
                }
                else if((int)cursorPos == 4)
                {
                    cursorPos = (CursorPositions)0;
                }
                else
                {
                    cursorPos--;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !cursorCantMove)
        {
            if ((int)cursorPos < 4)
            {
                cursorPos += 4;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !cursorCantMove)
        {
            if ((int)cursorPos > 3 && (int)cursorPos < 8)
            {
                if((int)cursorPos == 4)
                {
                    cursorPos = (CursorPositions)1;
                }
                else
                {
                    cursorPos -= 4;
                }
                
            }
            else if((int)cursorPos == 8)
            {
                cursorPos -= 5;
            }
        }


        if (PressAnyKeyPanel.activeSelf)
        {


            if(Input.anyKeyDown)
            {
                foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(kcode))
                    {
                        if(keyBinding == KeyBinding.Jump)
                        {
                            jumpKey.text = kcode.ToString();
                            InputManager.JumpTD = kcode;
                            InputManager.SaveKeys();
                            Invoke("SmallDelayToClose", 0.05f);
                        }
                        else if (keyBinding == KeyBinding.Slash)
                        {
                            slashKey.text = kcode.ToString();
                            InputManager.Slash = kcode;
                            InputManager.SaveKeys();
                            Invoke("SmallDelayToClose", 0.05f);
                        }
                        else if (keyBinding == KeyBinding.Shuriken)
                        {
                            shurikenKey.text = kcode.ToString();
                            InputManager.Shuriken = kcode;
                            InputManager.SaveKeys();
                            Invoke("SmallDelayToClose", 0.05f);
                        }
                        else if (keyBinding == KeyBinding.GrapplingHook)
                        {
                            grapplingHookKey.text = kcode.ToString();
                            InputManager.Hook = kcode;
                            InputManager.SaveKeys();
                            Invoke("SmallDelayToClose", 0.05f);
                        }
                        else if (keyBinding == KeyBinding.SmokeBomb)
                        {
                            smokeBombKey.text = kcode.ToString();
                            InputManager.SmokeBomb = kcode;
                            InputManager.SaveKeys();
                            Invoke("SmallDelayToClose", 0.05f);
                        }
                    } 
                }
            }
        }
	}

    public void KeyBindingButton (int binding)
    {
        Invoke("SmallDelayToOpen", 0.05f);
        keyBinding = (KeyBinding)binding;
        keyToChangeText.text = "Press the key to set " + keyBinding.ToString();
    }

    public void SmallDelayToOpen()
    {
        PressAnyKeyPanel.SetActive(true);
    }

    public void SmallDelayToClose()
    {
        PressAnyKeyPanel.SetActive(false);
    }

    public void SetKeybindingsText()
    {
        jumpKey.text = InputManager.JumpTD.ToString();
        slashKey.text = InputManager.Slash.ToString();
        shurikenKey.text = InputManager.Shuriken.ToString();
        grapplingHookKey.text = InputManager.Hook.ToString();
        smokeBombKey.text = InputManager.SmokeBomb.ToString();
        if(optionSettings.displayHints)
        {
            checkmark.GetComponent<Image>().enabled = true;
        }
        else
        {
            checkmark.GetComponent<Image>().enabled = true;
        }
    }

    public void EditSlider()
    {
        
    }


}
