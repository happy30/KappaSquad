//Katana script by Jordi

using UnityEngine;
using System.Collections;

public class Katana : MonoBehaviour
{

    public enum SwordType
    {
        PracticeSword,
        Katana
    };
    public SwordType swordType;

    public Transform playerModel;
    public PlayerController playerController;
    public GameObject SlashedObject;

    public int attackPower = 1;
    public float dashPower;
    public float coolDown;

    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerModel = GameObject.Find("PlayerModel").transform;
    }

    void Update()
    {
        if (Input.GetKey(InputManager.Slash) && !Camera.main.GetComponent<CameraController>().inCutscene && coolDown <= 0)
        {
            //Animator.playanimation
            playerController.Dash(dashPower);
            coolDown = 0.5f;
        }

        if(coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }

    }


    void Slash(int attackMultiplier)
    {
        RaycastHit hit;
        if(Physics.Raycast(playerModel.position, playerModel.forward, out hit, 2))
        {
            if(hit.collider.tag == "Enemy" || hit.collider.tag == "Destructible")
            {
                SlashedObject = hit.collider.gameObject;
                /*
                if (SlashedObject.GetComponent<EnemyMovement>() != null)
                {
                    SlashedObject.GetComponent<EnemyMovement>().GetHit(attackPower * attackMultiplier);
                }
                if (SlashedObject.GetComponent<DestructibleScript>() != null)
                {
                    SlashedObject.GetComponent<DestructibleScript>().Destroy();
                }
                */
            }
        }
        else
        {
            SlashedObject = null;
        }
    }

    public void UpgradeWeapon()
    {
        swordType = SwordType.Katana;
        attackPower = 2;
    }

}
