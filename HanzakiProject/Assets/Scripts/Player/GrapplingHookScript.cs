using UnityEngine;
using System.Collections;

public class GrapplingHookScript : MonoBehaviour
{
    public GrapplingHook hook;
    public StatsManager stats;

    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        hook = GameObject.Find("Player").GetComponent<GrapplingHook>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player" && stats.grapplingHookUnlocked)
        {
            Debug.Log("HOOK");
            Camera.main.GetComponent<CameraController>().hookObject = gameObject;
            hook.canHook = true;
            hook.hook = transform;

        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player" && stats.grapplingHookUnlocked)
        {
            Camera.main.GetComponent<CameraController>().hookObject = null;
            hook.canHook = false;
        }
    }

}
