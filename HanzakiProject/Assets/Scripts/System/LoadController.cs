//LoadController by Jordi

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadController : MonoBehaviour
{

    public GameObject loadingInterface;
    public float xPos;
    public RectTransform loadSprite;
    public Slider progressBar;
    AsyncOperation async;

    //Load the scene
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
        //loadingInterface = GameObject.Find("Canvas").GetComponent<UIManager>().loadInterface;
        //loadSprite = GameObject.Find("Canvas").GetComponent<UIManager>().loadingPiggy;
        //progressBar = GameObject.Find("Canvas").GetComponent<UIManager>().progressBar;
        //loadingInterface.SetActive(true);
    }

    public IEnumerator LoadLevel(string level)
    {
        async = SceneManager.LoadSceneAsync(level);
        yield return async;
        if (loadingInterface != null)
        {
            loadingInterface.SetActive(false);
        }
    }

    /*
    internal void LoadScene(object mainMenu)
    {
        throw new NotImplementedException();
    }

    */

    void Update()
    {
        if (async != null)
        {
            //progressBar.value = (float)async.progress;
            //we can have a loading bar here
            if (loadSprite != null)
            {
                loadSprite.anchoredPosition = new Vector2(((float)async.progress * 600) - 300, loadSprite.anchoredPosition.y);
            }
        }


    }
}
