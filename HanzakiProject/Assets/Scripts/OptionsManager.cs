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

    public KeyBinding keyBinding;
    public GameObject PressAnyKeyPanel;

    public RectTransform[] cursorPositions;

    public enum CursorPositions
    {
        Hints,
        JumpKey,
        SlashKey,
        ShurikenKey,
        GrapplingHookKey,
        SmokeBombKey,
        Master,
        BGM,
        SFX
    };

    public GameObject cursorArrow;
    public CursorPositions cursorPos;
    public float cursorArrowSpeed;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        cursorArrow.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(cursorArrow.GetComponent<RectTransform>().anchoredPosition, new Vector2(cursorArrow.GetComponent<RectTransform>().anchoredPosition.x, cursorPositions[(int)cursorPos].anchoredPosition.y), cursorArrowSpeed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if((int)cursorPos < 8)
            {
                cursorPos++;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if ((int)cursorPos > 0)
            {
                cursorPos--;
            }
        }


        if (PressAnyKeyPanel.activeSelf)
        {
            if(Input.anyKey)
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
                            PressAnyKeyPanel.SetActive(false);
                        }
                        if (keyBinding == KeyBinding.Slash)
                        {
                            slashKey.text = kcode.ToString();
                            InputManager.Slash = kcode;
                            InputManager.SaveKeys();
                            PressAnyKeyPanel.SetActive(false);
                        }
                        if (keyBinding == KeyBinding.Shuriken)
                        {
                            shurikenKey.text = kcode.ToString();
                            InputManager.Shuriken = kcode;
                            InputManager.SaveKeys();
                            PressAnyKeyPanel.SetActive(false);
                        }
                        if (keyBinding == KeyBinding.GrapplingHook)
                        {
                            grapplingHookKey.text = kcode.ToString();
                            InputManager.Hook = kcode;
                            InputManager.SaveKeys();
                            PressAnyKeyPanel.SetActive(false);
                        }
                        if (keyBinding == KeyBinding.SmokeBomb)
                        {
                            smokeBombKey.text = kcode.ToString();
                            InputManager.SmokeBomb = kcode;
                            InputManager.SaveKeys();
                            PressAnyKeyPanel.SetActive(false);
                        }
                    } 
                }
            }
        }
	}

    public void KeyBindingButton (int binding)
    {
        PressAnyKeyPanel.SetActive(true);
        keyBinding = (KeyBinding)binding;
    }
}
