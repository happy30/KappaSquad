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
    public float rotateSpeed;
    public Slider progressBar;
    AsyncOperation async;

    //Load the scene
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
        loadingInterface.SetActive(true);
        //loadSprite = GameObject.Find("Canvas").GetComponent<UIManager>().loadingPiggy;
        //progressBar = GameObject.Find("Canvas").GetComponent<UIManager>().progressBar;
        //loadingInterface.SetActive(true);
    }

    public IEnumerator LoadLevel(string level)
    {
        async = SceneManager.LoadSceneAsync(level);
        
        if (loadingInterface != null)
        {
            loadingInterface.SetActive(false);
        }
        yield return async;
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
            progressBar.value = (float)async.progress;
            //we can have a loading bar here
            if (loadSprite != null)
            {
                loadSprite.anchoredPosition = new Vector2(((float)async.progress * 1750) - 870, loadSprite.anchoredPosition.y);
                loadSprite.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
            }
        }


    }
}
