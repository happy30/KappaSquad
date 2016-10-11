using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour {

    public StatsManager stats;

    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
    }
    void Update ()
    {
	    if(stats.grapplingHookUnlocked)
        {
            if(Input.GetKeyDown(InputManager.Hook))
            {

            }
        }
	}
}
