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


	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        followTime = 1.5f;

        followTime = 2.5f;

        if(playerController.levelType == PlayerController.LevelType.TD)
        {
            cameraOffsetY = 10;
            transform.eulerAngles = new Vector3(30, 0, 0);
        }
        else
        {
            cameraOffsetY = 5;
            transform.eulerAngles = new Vector3(8, 0, 0);
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
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + cameraOffsetX, player.transform.position.y + cameraOffsetY, player.transform.position.z - 15f), followTime * Time.deltaTime);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(30, 0, 0), followTime * Time.deltaTime);
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
            transform.position = Vector3.Lerp(transform.position, new Vector3(followThis.transform.position.x, transform.position.y, -4f), followTime * Time.deltaTime);
        }
        else
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(30, 0, 0), followTime * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, new Vector3(followThis.transform.position.x, followThis.transform.position.y + 5, -8f), followTime * Time.deltaTime);

        }      
    }
}
