using UnityEngine;
using System.Collections;

public class LocationChanger : MonoBehaviour
{
    public GameObject distanceCalculator;
    GameObject player;
    public float distance;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            distance = Vector3.Distance(distanceCalculator.transform.position, other.transform.position);
            
        }
    }
}
