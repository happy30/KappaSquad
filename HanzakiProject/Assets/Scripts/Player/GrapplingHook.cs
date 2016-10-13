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

    public Rigidbody _rb;

    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        _rb = GetComponent<Rigidbody>();
        _line = GetComponent<LineRenderer>();
    }
    void Update ()
    {
	    if(stats.grapplingHookUnlocked)
        {
            if(Input.GetKeyDown(InputManager.Hook) && canHook)
            {
                spawnedClaw = (GameObject)Instantiate(claw, transform.position, Quaternion.identity);
                spawnedClaw.transform.LookAt(hook);
                grabbing = true;
            }
        }

        if(grabbing)
        {
            spawnedClaw.transform.position = Vector3.MoveTowards(spawnedClaw.transform.position, hook.transform.position, clawSpeed * Time.deltaTime);
            linePositions[0] = transform.position;
            linePositions[1] = spawnedClaw.transform.position;
            _line.SetPositions(linePositions);

            if(Vector3.Distance(spawnedClaw.transform.position, hook.transform.position) < 0.1f)
            {
                _rb.velocity = (hook.transform.position - transform.position)
            }
        }
        else
        {
            
        }
	}
}
