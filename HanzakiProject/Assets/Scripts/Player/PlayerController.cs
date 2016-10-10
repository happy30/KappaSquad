// PlayerController by Jordi

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //Components
    Rigidbody _rb;
    StatsManager stats;
    UIManager ui;
    GameObject playerModel;
    OptionsSettings optionsSettings;

    //What Level
    public enum LevelType
    {
        SS,
        TD
    };
    public LevelType levelType;

    //Movement
    float speed;
    public float jumpHeight;
    public float xMovement;
    public float zMovement;
    public float modelWidth;
    bool inAir;

    public bool onSlipperyTile;
    public bool onSlipperyTileNearWall;

    public int buttonCount;
    public KeyCode lastKey;
    public float doubleTapTime;
    public float dashSpeed;
    public float dashCooldown;

    //Combat
    public bool invulnerable;
    public float invulnerableTime;
    float invulnerableTimer;


    //Gather components
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        optionsSettings = GameObject.Find("GameManager").GetComponent<OptionsSettings>();
        playerModel = GameObject.Find("PlayerModel");
        modelWidth = 1;
    }

    //Set the right controls
    void Start()
    {
        ChangeControlsDependingOnLevelType();
    }
	
	void Update ()
    {
        SetMovement();
        CheckForWall();
        Move();
        CheckForDash();
        CheckForSlipperyTile();
        CheckVulnerability();
        Debug.DrawRay(playerModel.transform.position, playerModel.transform.forward);
    }


    void ChangeControlsDependingOnLevelType()
    {
        if (levelType == LevelType.SS)
        {
            InputManager.Jump = InputManager.JumpSS;
        }
        else if (levelType == LevelType.TD)
        {
            InputManager.Jump = InputManager.JumpTD;
        }
    }

    //Change speed to runspeed if Shift is pressed
    void SetMovement()
    {
        speed = Input.GetKey(KeyCode.LeftShift) ? stats.runSpeed : stats.walkSpeed;
        if(!onSlipperyTile)
        {
            xMovement = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
            if (xMovement > 0)
            {
                playerModel.transform.eulerAngles = new Vector3(0, 90, 0);
                zMovement = 0;
            }
            if (xMovement < 0)
            {
                playerModel.transform.eulerAngles = new Vector3(0, -90, 0);
                zMovement = 0;
            }
            if (levelType == LevelType.TD)
            {
                zMovement = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
                if (zMovement > 0)
                {
                    playerModel.transform.eulerAngles = new Vector3(0, -0, 0);
                    xMovement = 0;
                }
                if (zMovement < 0)
                {
                    playerModel.transform.eulerAngles = new Vector3(0, 180, 0);
                    xMovement = 0;
                }
            }
        }      
    }

    void CheckForDash()
    {
        if(levelType == LevelType.TD)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                CheckForDoubleTap(KeyCode.UpArrow);
                lastKey = KeyCode.UpArrow;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                CheckForDoubleTap(KeyCode.DownArrow);
                lastKey = KeyCode.DownArrow;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CheckForDoubleTap(KeyCode.RightArrow);
            lastKey = KeyCode.RightArrow;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CheckForDoubleTap(KeyCode.LeftArrow);
            lastKey = KeyCode.LeftArrow;
        }

        if (doubleTapTime < 0)
        {
            doubleTapTime = 0;
            buttonCount = 0;
        }
        else
        {
            doubleTapTime -= Time.deltaTime;
        }

        if(dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }
        else
        {
            dashCooldown = 0;
        }
    }


    void CheckForDoubleTap(KeyCode key)
    {
        Debug.Log(key);
        if(buttonCount == 1)
        {
            if(lastKey == key)
            {
                Debug.Log("dash!!");
                if(dashCooldown <= 0)
                {
                    Dash(dashSpeed);
                    dashCooldown = 2f;
                }
                buttonCount = 0;
            }
            else
            {
                buttonCount = 0;
            }
            
        }
        else
        {
            buttonCount++;
            doubleTapTime = 0.5f;
        }
    }

    void CheckForSlipperyTile()
    {
        if (IsTouching(20) != null)
        {
            if (IsTouching(20).tag == "Slippery")
            {
                onSlipperyTile = true;
            }
            else
            {
                if(IsTouching(2) != null)
                {
                    if (IsTouching(2).tag == "Ground")
                    {
                        onSlipperyTile = false;
                    }
                } 
            }
        }
        if(onSlipperyTile && !onSlipperyTileNearWall)
        {
            if (playerModel.transform.eulerAngles == new Vector3(0, 90, 0))
            {
                xMovement =  stats.runSpeed * Time.deltaTime;
            }
            else if (playerModel.transform.eulerAngles == new Vector3(0, -90, 0))
            {
                xMovement = -stats.runSpeed * Time.deltaTime;
            }
            else if (playerModel.transform.eulerAngles == new Vector3(0, 0, 0))
            {
                zMovement = stats.runSpeed * Time.deltaTime;
            }
            else if (playerModel.transform.eulerAngles == new Vector3(0, 180, 0))
            {
                zMovement = -stats.runSpeed * Time.deltaTime;
            }
        }
        if(onSlipperyTile && onSlipperyTileNearWall)
        {
            onSlipperyTile = false;
            if(xMovement != 0 || zMovement != 0)
            {
                onSlipperyTile = true;
            }
        }

    }

    public void Dash(float distance)
    {
        _rb.velocity = playerModel.transform.forward * distance;
    }

    //Move the player and let it jump
    void Move()
    {
        if (!Camera.main.gameObject.GetComponent<CameraController>().inCutscene)
        {
            transform.Translate(new Vector3(xMovement, 0, zMovement));
            if (Input.GetKey(InputManager.Jump))
            {
                //Check if player is standing on Ground
                if (IsTouching(2) != null)
                {
                    if (IsTouching(2).tag == "Ground")
                    {
                        Debug.Log("jump");
                        if (!CheckIfJumping() && !inAir)
                        {
                            Jump();
                        }
                    }
                }
            }
        }
    }

    //Make the player stop moving if it's humping a wall
    void CheckForWall()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.right, out hit, modelWidth))
        {
            onSlipperyTileNearWall = true;
            if (xMovement < 0)
            {
                xMovement = 0;
            }
        }
        if (Physics.Raycast(transform.position, transform.right, out hit, modelWidth))
        {
            onSlipperyTileNearWall = true;
            if (xMovement > 0)
            {
                xMovement = 0;
            }
        }
        if(levelType == LevelType.TD)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, modelWidth))
            {
                onSlipperyTileNearWall = true;
                if (zMovement > 0)
                {
                    zMovement = 0;
                }
            }
            if (Physics.Raycast(transform.position, -transform.forward, out hit, modelWidth))
            {
                onSlipperyTileNearWall = true;
                if (zMovement < 0)
                {
                    zMovement = 0;
                }
            }
        }
    }

    //Check what object is beneath the player
    public GameObject IsTouching(int range)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, range))
        {
            return hit.collider.gameObject;
        }
        else
        {
            inAir = true;
        }
        return null;
    }

    //Throw the player in the air
    public void Jump()
    {
        _rb.velocity = new Vector3(0, jumpHeight, 0);
    }

    public bool CheckIfJumping()
    {
        if(_rb.velocity.y > jumpHeight -0.1)
        {
            return true;
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1))
        {
            if(hit.collider.tag == "Ground")
            {
                inAir = false;
            }
        }
        return false;
    }

    void CheckVulnerability()
    {
        if(invulnerable)
        {
            invulnerableTimer += Time.deltaTime;
            if(invulnerableTimer > invulnerableTime)
            {
                invulnerable = false;
                invulnerableTimer = 0;
            }
        }
    }

    public void GetHit(int damage)
    {
        if(!invulnerable)
        {
            stats.health--;
            //knockback maybe.
        }
    }

    public void GetInvulnerable()
    {
        invulnerable = true;
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Interact")
        {
            if(col.gameObject.GetComponent<InteractScript>().interactType == InteractScript.InteractType.OnTrigger)
            {
                if(col.gameObject.GetComponent<InteractScript>().isHint)
                {
                    if (optionsSettings.displayHints)
                    {
                        col.gameObject.GetComponent<InteractScript>().Activate();
                    }
                }
                else
                {
                    col.gameObject.GetComponent<InteractScript>().Activate();
                }
                
            }
            else if(col.gameObject.GetComponent<InteractScript>().interactType == InteractScript.InteractType.OnInput)
            {
                ui.ChangeInteractText(col.gameObject.GetComponent<InteractScript>());
                if(Input.GetKey(InputManager.Slash))
                {
                    col.gameObject.GetComponent<InteractScript>().Activate();
                }
                if(col.gameObject.GetComponent<InteractScript>().linkedObject.GetComponent<Activate>().activated)
                {
                    ui.RemoveInteractText();
                }
            }
            
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Ground")
        {
            inAir = false;
        }
    }

    void OnTriggerExit()
    {
        ui.RemoveInteractText();
    }
}
