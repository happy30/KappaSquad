using UnityEngine;
using System.Collections;

public class MainMenuScroll : MonoBehaviour {

    Rigidbody _rb;
    public float fallSpeed;

    public MainMenuController mainMenuController;
    public bool hasPlayedSFX;
    public AudioSource sound;
    public AudioClip scrollSound;
    public float cutOffValue;

    public float timer;

    public GameObject scrollObject;
    public GameObject scroll;
    

	// Use this for initialization
	void Awake ()
    {
        sound = GameObject.Find("MainMenuUISounds").GetComponent<AudioSource>();
        mainMenuController = GameObject.Find("MainMenuCanvas").GetComponent<MainMenuController>();
        _rb = GetComponent<Rigidbody>();
        cutOffValue = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (mainMenuController.scrollActivated)
        {
            scrollObject.GetComponent<Animator>().SetBool("StartScrolling", true);
            scroll.GetComponent<Cloth>().externalAcceleration = new Vector3(10f, 0, 30f);
            Invoke("ResetAcceleration", 1.5f);
            /*
            if (cutOffValue > 0)
            {
                cutOffValue -= fallSpeed * Time.deltaTime;
            }
            if (transform.position.y > -12)
            {
                timer += Time.deltaTime;
                transform.position = new Vector3(transform.position.x, scrollObject.GetComponent<Renderer>().material.GetFloat("_Cutoff") * 23, transform.position.z);
                scrollObject.GetComponent<Cloth>().externalAcceleration = new Vector3(0.4f, 0, 10);
            }
            else
            {
                if(!hasPlayedSFX)
                {
                    Debug.Log(timer);
                    sound.PlayOneShot(scrollSound);
                    hasPlayedSFX = true;
                    Invoke("ResetAcceleration", 1f);
                }
            }
            */
        }
	}

    public void ResetAcceleration()
    {
        scroll.GetComponent<Cloth>().externalAcceleration = new Vector3(0.4f, 0, 0.4f);
        if (!hasPlayedSFX)
        {
            sound.PlayOneShot(scrollSound);
            hasPlayedSFX = true;
        }
    }
}
