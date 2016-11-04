﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    //Scroll texture
    public Texture2D continueUnlockedTexture;
    public Texture2D continueLockedTexture;
    public Texture2D continueUnlockedTextureAlpha2;
    public Texture2D continueLockedTextureAlpha2;
    public bool continueUnlocked;
    public Material scrollMaterial;

    public MainMenuScroll scrollScript;
    public GameObject scroll;

    public RectTransform optionsPanel;
    public float optionsSlideTime;
    public float optionsLocationX;
    public float scrollLocationX;
    public bool optionsOpen;

    public GameObject blackScreen;

    //For the arrow
    public RectTransform[] buttons;
    public RectTransform cursorArrow;
    public float arrowSpeed;
    public float yPosArrow;
    public bool scrollActivated;

    public bool fadeScreen;
    public float fadeValue;

    public float cutoutValue;

    //Any key
    public GameObject pressAnyKeyObject;

    //sounds
    AudioSource sound;
    public AudioClip buttonHover;
    public AudioClip openMenu;

    public enum CursorAt
    {
        NewGame,
        Continue,
        Options,
        Exit
    };

    public CursorAt cursorAt;

    void Awake()
    {
        sound = GameObject.Find("MainMenuUISounds").GetComponent<AudioSource>();
        scrollScript = GameObject.Find("ScrollBottom").GetComponent<MainMenuScroll>();
    }
	// Use this for initialization
	void Start ()
    {
        cursorArrow.anchoredPosition = new Vector2(cursorArrow.anchoredPosition.x, buttons[0].anchoredPosition.y);
        cursorArrow.gameObject.SetActive(false);
        optionsLocationX = 1920;
        scrollLocationX = scroll.transform.position.x;

        if (continueUnlocked)
        {
            scrollMaterial.mainTexture = continueUnlockedTexture;
        }
        else
        {
            scrollMaterial.mainTexture = continueLockedTexture;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(cursorArrow.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SetCursorPosition((int)cursorAt + 1, true);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SetCursorPosition((int)cursorAt - 1, false);
            }

            if (cursorAt == CursorAt.NewGame)
            {
                yPosArrow = buttons[0].anchoredPosition.y;
            }
            else if (cursorAt == CursorAt.Continue)
            {
                yPosArrow = buttons[1].anchoredPosition.y;
            }
            else if (cursorAt == CursorAt.Options)
            {
                yPosArrow = buttons[2].anchoredPosition.y;
            }
            else if (cursorAt == CursorAt.Exit)
            {
                yPosArrow = buttons[3].anchoredPosition.y;
            }

            cursorArrow.anchoredPosition = Vector2.Lerp(cursorArrow.anchoredPosition, new Vector2(cursorArrow.anchoredPosition.x, yPosArrow), arrowSpeed * Time.deltaTime);
        }
        else
        {
            if(Input.anyKey && !scrollActivated)
            {
                sound.PlayOneShot(openMenu);
                pressAnyKeyObject.SetActive(false);
                scrollActivated = true;
                Invoke("ArrowCursorAppear", 2);
            }
        } 

        if(Input.GetKeyDown(InputManager.Slash) && cursorArrow.gameObject.activeSelf)
        {
            if(cursorAt == CursorAt.NewGame)
            {
                NewGame();
                Invoke("InitiateFade", 1f);
                cursorArrow.gameObject.SetActive(false);
            }
            else if(cursorAt == CursorAt.Options)
            {
                cursorArrow.gameObject.SetActive(false);
                scroll.transform.GetComponentInChildren<Cloth>().enabled = false;
                optionsLocationX = 0;
                scrollLocationX = scrollLocationX - 60f;
            }
        }

        if(fadeScreen)
        {
            fadeValue += 0.5f * Time.deltaTime;
            Camera.main.fieldOfView -= fadeValue;
            blackScreen.GetComponent<Image>().color = new Color(blackScreen.GetComponent<Image>().color.r, blackScreen.GetComponent<Image>().color.g, blackScreen.GetComponent<Image>().color.b, fadeValue / 0.8f);
        }

        if(Input.GetButtonDown("Cancel"))
        {
            if(optionsOpen)
            {
                OptionsBack();
            }
        }

        optionsPanel.anchoredPosition = Vector3.Lerp(optionsPanel.anchoredPosition, new Vector2(optionsLocationX, optionsPanel.anchoredPosition.y), optionsSlideTime * Time.deltaTime);
        scroll.transform.position = Vector3.Lerp(scroll.transform.position, new Vector3(scrollLocationX, scroll.transform.position.y, scroll.transform.position.z), optionsSlideTime * Time.deltaTime);
    }

    public void SetCursorPosition(int pos, bool goingDown)
    {
        Debug.Log((int)cursorAt);
        if(pos == 0)
        {
            cursorAt = CursorAt.NewGame;
            sound.PlayOneShot(buttonHover);
        }
        if(pos == 1)
        {
            if(goingDown)
            {
                if(continueUnlocked)
                {
                    cursorAt = CursorAt.Continue;
                    sound.PlayOneShot(buttonHover);
                }
                else
                {
                    cursorAt = CursorAt.Options;
                    sound.PlayOneShot(buttonHover);
                }
            }
            else
            {
                if (continueUnlocked)
                {
                    cursorAt = CursorAt.Continue;
                    sound.PlayOneShot(buttonHover);
                }
                else
                {
                    cursorAt = CursorAt.NewGame;
                    sound.PlayOneShot(buttonHover);
                }
            }
        }
        if (pos == 2)
        {
            cursorAt = CursorAt.Options;
            sound.PlayOneShot(buttonHover);
        }
        if(pos == 3)
        {
            cursorAt = CursorAt.Exit;
            sound.PlayOneShot(buttonHover);
        }
        Debug.Log((int)cursorAt);
    }

    public void ArrowCursorAppear()
    {
        cursorArrow.gameObject.SetActive(true);
    }

    public void NewGame()
    {
        if (continueUnlocked)
        {
            
        }
        else
        {
            scrollScript.scroll.GetComponent<Renderer>().material.mainTexture = continueLockedTextureAlpha2;
            scrollScript.scrollObject.GetComponent<Animator>().SetBool("BurnScroll", true);
        }
    }

    public void OptionsBack()
    {
        optionsOpen = false;
        optionsLocationX = 1920;
        Invoke("ArrowCursorAppear", 1.2f);
        scrollLocationX = scrollLocationX + 60f;
        scroll.transform.GetComponentInChildren<Cloth>().enabled = true;
    }


    public void InitiateFade()
    {
        fadeScreen = true;
    }


}
