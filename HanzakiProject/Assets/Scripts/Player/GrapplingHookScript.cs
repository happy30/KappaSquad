using UnityEngine;
using System.Collections;

public class GrapplingHookScript : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("HOOK");
            Camera.main.GetComponent<CameraController>().hookObject = gameObject;

        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            Camera.main.GetComponent<CameraController>().hookObject = null;
        }
    }

}
