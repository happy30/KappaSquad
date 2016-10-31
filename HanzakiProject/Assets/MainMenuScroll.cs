using UnityEngine;
using System.Collections;

public class MainMenuScroll : MonoBehaviour {

    Rigidbody _rb;
    public float fallSpeed;

    public MainMenuController mainMenuController;
    public bool hasPlayedSFX;
    public AudioSource sound;
    public AudioClip scrollSound;

	// Use this for initialization
	void Awake ()
    {
        sound = GameObject.Find("MainMenuUISounds").GetComponent<AudioSource>();
        mainMenuController = GameObject.Find("MainMenuCanvas").GetComponent<MainMenuController>();
        _rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(mainMenuController.scrollActivated)
        {
            fallSpeed += fallSpeed * 2f * Time.deltaTime;
            if (transform.position.y > 0)
            {
                transform.Translate(0, -fallSpeed * Time.deltaTime, 0);
            }
            else
            {
                if(!hasPlayedSFX)
                {
                    sound.PlayOneShot(scrollSound);
                    hasPlayedSFX = true;
                }
            }
        }
	}
}
