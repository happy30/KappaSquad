//PauseManager by Arne
using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour {
	
	public GameObject pausedPanel;
	public GameObject exitGamePanel;
	public GameObject optionsManagerPanel;
	public bool inExitMenu; //off is niet, on is wel
	public bool inOptionsMenu; //off is niet, on is wel
	public int menuStatus; //1 is options menu, 2 is exitgame menu
	
	
	// Use this for initialization
	void Awake () 
	{
		pausedPanel.SetActive(false);
		menuStatus = 3;
	}
	// Update is called once per frame, BGM stops/resumes,window pop up,cursor shows/hides,extra info at pause?
	void Update () 
	{
		if(Input.GetButtonDown("Cancel")) 
		{
			switch (menuStatus) 
			{
				
			case 1:
			
				optionsManagerPanel.SetActive(false);
				pausedPanel.SetActive(true);
				menuStatus = 3;
				break;
				
			case 2:
			
				exitGamePanel.SetActive(false);
				pausedPanel.SetActive(true);
				menuStatus = 3;
				break;
				
			case 3:
			
				if(Time.timeScale == 1.0f)
				{
					Time.timeScale = 0.0f; //BGM should stop, window with settings and resume game, screen darker, cursor shows
					pausedPanel.SetActive(true);
				}
				else 
				{
					Time.timeScale = 1.0f; //BGM continues where left off, window closes, Cursor hides again
					pausedPanel.SetActive(false);
				}
				break;
			}
		}
	}
	//ResumeGame gets you back into the game
	public void ResumeGame () 
	{
		pausedPanel.SetActive(false);
		Time.timeScale = 1.0f; //BGM continues where left off, window closes, Cursor hides again
		menuStatus = 3;
	}
	//OptionsMenu gets you to the options panel
	public void OptionsMenu ()
	{
		optionsManagerPanel.SetActive(true);
		pausedPanel.SetActive(false);
		menuStatus = 1;
	}
	//OptionsBack gets you back to the previous screen
	public void OptionsBack () 
	{
		optionsManagerPanel.SetActive(false);
		pausedPanel.SetActive(true);
		menuStatus = 3;
	}
	//ExitGameMenu gives you the option to leave or resume
	public void ExitGameMenu ()
	{
		exitGamePanel.SetActive(true);
		menuStatus = 2;
	}
	//ExitGameTrue goes to the main menu scene
	public void ExitGameTrue () 
	{
		Application.LoadLevel(0);
		Time.timeScale = 1.0f; //BGM continues where left off, window closes, Cursor hides again
		menuStatus = 3;
	}
	//ExitGameFalse gets you back to the previous screen
	public void ExitGameFalse ()
	{
		exitGamePanel.SetActive(false);
	}
}