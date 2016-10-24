// GrapplingHook script by Jordi

using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour {


    //Components
    StatsManager stats;
    public Rigidbody _rb;
    public Transform hook;
    

    //Claw and rope
    public GameObject claw;
    public float clawSpeed;
    GameObject spawnedClaw;
    LineRenderer _line;
    public Vector3[] linePositions;

    //Timers
    public bool canHook;
    bool grabbing;
    float grabTimer;
    public float hookCooldown;

    
    public Transform playerModel;

    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        _rb = GetComponent<Rigidbody>();
        _line = GetComponent<LineRenderer>();
        playerModel = GameObject.Find("PlayerModel").transform;
    }
    void Update ()
    {
        //Can use hook if it's enabled in statsmanager and hook nearby.
	    if(stats.grapplingHookUnlocked)
        {
            if(Input.GetKeyDown(InputManager.Hook) && canHook && hookCooldown <= 0)
            {
                if (spawnedClaw != null)
                {
                    Destroy(spawnedClaw);
                }
                spawnedClaw = (GameObject)Instantiate(claw, transform.position, Quaternion.identity);
                spawnedClaw.transform.LookAt(hook);
                if(!grabbing)
                {
                    
                    grabbing = true;
                }
                else 
                {
                    _rb.velocity = (playerModel.transform.forward * 5) + new Vector3(0, 10, 0);
                    grabTimer = 0;
                    grabbing = false;
                    linePositions[0] = new Vector3(0, 0, 0);
                    linePositions[1] = new Vector3(0, 0, 0);
                    _line.SetPositions(linePositions);
                    grabbing = false;
                }
                
            }
            else if (Input.GetKeyDown(InputManager.Hook))
            {
                if(spawnedClaw != null)
                {
                    Destroy(spawnedClaw);
                    grabTimer = 0;
                    grabbing = false;
                    linePositions[0] = new Vector3(0, 0, 0);
                    linePositions[1] = new Vector3(0, 0, 0);
                    _line.SetPositions(linePositions);
                    _rb.velocity = (playerModel.transform.forward * 5) + new Vector3(0, 10, 0);
                }
                
            }
        }


        if(hookCooldown > 0)
        {
            hookCooldown -= Time.deltaTime;
        }

        //Move the claw towards the hook, if it hits move the player along.
        if(grabbing)
        {
            spawnedClaw.transform.position = Vector3.MoveTowards(spawnedClaw.transform.position, hook.transform.position, clawSpeed * Time.deltaTime);
            linePositions[0] = transform.position;
            linePositions[1] = spawnedClaw.transform.position;
            _line.SetPositions(linePositions);
            grabTimer += Time.deltaTime;
            hookCooldown = 3;

            if(Vector3.Distance(spawnedClaw.transform.position, hook.transform.position) < 0.1f)
            {
                _rb.velocity = (hook.transform.position - transform.position);
                if (grabTimer < 1)
                {
                    _rb.velocity = ((hook.transform.position - transform.position) * 2);
                }
                else
                {
                    _rb.velocity = (playerModel.transform.forward * 5) + new Vector3(0, 10, 0);

                    Destroy(spawnedClaw);
                    grabTimer = 0;
                    grabbing = false;
                    linePositions[0] = new Vector3(0,0,0);
                    linePositions[1] = new Vector3(0, 0, 0);
                    _line.SetPositions(linePositions);
                }
            }
        }
        else
        {
            
        }
	}
}
