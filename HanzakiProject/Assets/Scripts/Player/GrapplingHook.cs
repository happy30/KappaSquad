using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour {

    public StatsManager stats;
    public bool canHook;
    public Transform hook;
    public float clawSpeed;

    public GameObject claw;
    GameObject spawnedClaw;
    LineRenderer _line;
    public Vector3[] linePositions;

    public bool grabbing;
    public float grabTimer;
    public float hookCooldown;

    public Rigidbody _rb;
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
