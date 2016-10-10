//Made by Sascha Greve

using UnityEngine;
using System.Collections;

public class DestructibleScript : MonoBehaviour
{
    public GameObject particleObject;
    GameObject spawnedParticleObject;


    public void Destroy ()
    {
        Destroy(spawnedParticleObject = (GameObject)Instantiate(particleObject, transform.position, Quaternion.identity), 3);
        Destroy(gameObject);
    }
}
