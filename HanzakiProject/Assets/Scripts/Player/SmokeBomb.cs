//Smokebomb by Jordi

using UnityEngine;
using System.Collections;

public class SmokeBomb : MonoBehaviour
{

    public StatsManager stats;
    public bool reloading;
    public float reloadTimer;

    public GameObject particleObject;
    GameObject spawnedParticleObject;

    public PlayerController _playerController;

    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        _playerController = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(InputManager.SmokeBomb) && stats.smokeBombUnlocked && stats.smokeBombAmount > 0 && !reloading)
        {
            //Animatorplay blabla
            ThrowSmokeBomb();
            reloading = true;
        }
        if (reloading)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer > 1)
            {
                reloading = false;
                reloadTimer = 0;
            }
        }
    }

    void ThrowSmokeBomb()
    {
        _playerController.GetInvulnerable();    
        Destroy(spawnedParticleObject = (GameObject)Instantiate(particleObject, _playerController.transform.position, Quaternion.identity), 3);
    }
}
