//CameraController by Jordi

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerController;
    float cameraOffsetX;
    public float cameraOffsetY;

    public float followTime;
    public bool inCutscene;
    public bool inPuzzle;

    public GameObject followObject;
    public GameObject hookObject;

    public float cutsceneZ;

    public Vector3 cameraRot;


	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cutsceneZ = transform.position.z - 1f;

        followTime = 1.5f;

        followTime = 2.5f;

        if(playerController.levelType == PlayerController.LevelType.TD)
        {
            cameraOffsetY = 10;
            cameraRot = new Vector3(30, 0, 0);
        }
        else
        {
            cameraOffsetY = 1;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!inCutscene)
        {
            FollowPlayer();
        }
        else
        {
            if(followObject != null)
            {
                FollowObject(followObject);
            }
        }

        
    }

    //Make the camera follow the player. If the player moves the camera offset will change so that it gives a better vision of what's in front of the player.
    public void FollowPlayer()
    {
        if(playerController.xMovement > 0.01)
        {
            cameraOffsetX = 5;
        }
        else if (playerController.xMovement < -0.01)
        {
            cameraOffsetX = -5;
        }
        else
        {
            cameraOffsetX = 0;
        }
        if(!inPuzzle)
        {
            if(hookObject == null)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + cameraOffsetX, player.transform.position.y + cameraOffsetY, player.transform.position.z - 10f), followTime * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(hookObject.transform.position.x, (hookObject.transform.position.y - player.transform.position.y) / 2  + cameraOffsetY, player.transform.position.z - 15f), followTime * Time.deltaTime);
            }
            
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, cameraRot, followTime * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + cameraOffsetX, player.transform.position.y + 30, player.transform.position.z), followTime * Time.deltaTime);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(80, 0, 0), followTime * Time.deltaTime);
           
        }
        
        //transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + cameraOffsetX, player.transform.position.y + cameraOffsetY, player.transform.position.z - 20f), followTime * Time.deltaTime);

    }

    //Focus the camera on an object
    public void FollowObject(GameObject followThis)
    {
        if(playerController.levelType == PlayerController.LevelType.SS)
        {
            //transform.position = Vector3.Lerp(transform.position, new Vector3(followThis.transform.position.x, transform.position.y, cutsceneZ), followTime * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, followThis.transform.position, followTime * Time.deltaTime);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, followThis.transform.eulerAngles, followTime * Time.deltaTime);
        }
        else
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(30, 0, 0), followTime * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, new Vector3(followThis.transform.position.x, followThis.transform.position.y + 5, followThis.transform.position.z -8f), followTime * Time.deltaTime);
        }      
    }
}
