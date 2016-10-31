using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    //Scroll texture
    public Texture2D continueUnlockedTexture;
    public Texture2D continueLockedTexture;
    public bool continueUnlocked;
    public Material scrollMaterial;

    //For the arrow
    public RectTransform[] buttons;
    public RectTransform cursorArrow;
    public float arrowSpeed;
    public float yPosArrow;
    public bool scrollActivated;

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
    }
	// Use this for initialization
	void Start ()
    {
        cursorArrow.anchoredPosition = new Vector2(cursorArrow.anchoredPosition.x, buttons[0].anchoredPosition.y);
        cursorArrow.gameObject.SetActive(false);
        if(continueUnlocked)
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
}
