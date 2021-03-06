﻿//Shuriken by Jordi

using UnityEngine;
using System.Collections;

public class Shuriken : MonoBehaviour {

    public StatsManager stats;
    public int attackPower;
    public float reloadTimer;
    public bool reloading;

    public GameObject shurikenObject;
    GameObject spawnedShurikenObject;



    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
    }

	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKey(InputManager.Shuriken) && stats.shurikenUnlocked && stats.shurikenAmount > 0 && !reloading)
        {
            //Animatorplay blabla
            ThrowShuriken(attackPower);
            reloading = true;   
        }
        if(reloading)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer > 1)
            {
                reloading = false;
                reloadTimer = 0;
            }
        }
	}


    public void ThrowShuriken(int attackPower)
    {
        Destroy(spawnedShurikenObject = (GameObject)Instantiate(shurikenObject, transform.position, Quaternion.identity), 3);
        spawnedShurikenObject.GetComponent<ShurikenObject>().attackPower = attackPower;
        stats.shurikenAmount--;	
    }
}
