//EnemyMovement by Alieke
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;

    public List<Vector3> wayPoints = new List<Vector3>();
    public float walkSpeed;
    public float runSpeed;
    public bool topDownView;

    private  int currentWayPoint;
    public float rangeOffSet;
    public float maxWayPoints;
    private Animator anim;
    public bool isAlive;
    private float lookOutTimer;
    public float minLookOutTime;
    public float maxLookOutTime;

    public float attackRange;
    public int attackDamage;
    private float attackRate;
    public float restartAttack;
    private float mayAttack;
    public RaycastHit hit;
    public float rayDis;

    public int health;
    private Image image;
    public GameObject healthSprite;
    public List<Sprite> spriteArray = new List<Sprite>();

    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //image = healthSprite.GetComponent<Image>();
        for (int i = 0; i < maxWayPoints; i++)
        {
            if (topDownView)
            {
                wayPoints.Add(new Vector3(Random.Range(transform.position.x - rangeOffSet, transform.position.x + rangeOffSet), Random.Range(transform.position.x - rangeOffSet, transform.position.y + rangeOffSet), Random.Range(transform.position.z - rangeOffSet, transform.position.z + rangeOffSet)));
            }
            else
            {
                wayPoints.Add(new Vector3(Random.Range(transform.position.x - rangeOffSet, transform.position.x + rangeOffSet), transform.position.y, transform.position.z));
            }
        }
    }

    void Update()
    {
        if (isAlive)
        {
            if (gameObject.GetComponent<EnemySight>().playerInView == true && isAlive)
            {
                Chase();
            }
            else
            {
                Patrol();
            }
        }
        else
        {
            agent.speed = 0;
        }
    }

    void Patrol()
    {
        if(currentWayPoint >= wayPoints.Count)
        {
            ResetPatrol();
        }
        agent.SetDestination(wayPoints[currentWayPoint]);

        float distance = Vector3.Distance(wayPoints[currentWayPoint], transform.position);
        if(distance < 6)
        {
            agent.speed = 0;
            anim.SetFloat("WalkSpeed", 0);
            lookOutTimer -= Time.deltaTime;
            if (lookOutTimer <= 0)
            {
                currentWayPoint++;
                lookOutTimer = Random.Range(minLookOutTime, maxLookOutTime);
            }
        }
        else
        {
            agent.speed = walkSpeed;
            anim.SetFloat("WalkSpeed", agent.speed);
        }
    }

    void ResetPatrol()
    {
        currentWayPoint = 0;
        for (int i = 0; i < wayPoints.Count - 1; i++)
        {
            if (topDownView) {
                wayPoints[i] = new Vector3(Random.Range(transform.position.x - rangeOffSet, transform.position.x + rangeOffSet), Random.Range(transform.position.x - rangeOffSet, transform.position.y + rangeOffSet), Random.Range(transform.position.z - rangeOffSet, transform.position.z + rangeOffSet));
            }
            else
            {
                wayPoints.Add(new Vector3(Random.Range(transform.position.x - rangeOffSet, transform.position.x + rangeOffSet), transform.position.y, transform.position.z));
            }
        }
    }

    void Chase()
    {
        agent.speed = runSpeed;
        anim.SetFloat("WalkSpeed", agent.speed);
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(player.position, transform.position);
        if(distance < attackRange)
        {
            Attacking();
        }
    }

    void Attacking()
    {
        if (attackRate <= 0)
        {
            anim.SetBool("AttackRange", true);
            attackRate = restartAttack;
            if(Physics.Raycast(transform.position,transform.forward,out hit, rayDis))
            {
                if(hit.transform.tag == "Player")
                {
                    //  hit.transform.GetComponent<PlayerStats>().GetHit(attackDamage);
                }
            }
        }
        else
        {
            if (mayAttack < anim.runtimeAnimatorController.animationClips.Length)
            {
                anim.SetBool("AttackRange", false);
                mayAttack = anim.runtimeAnimatorController.animationClips.Length + 2;
            }
            attackRate -= Time.deltaTime;
            mayAttack -= Time.deltaTime;
        }
    }

    public void GetHit(int damageGet)
    {
        health -= damageGet;
        if (health <= 0)
        {
            anim.SetBool("Death", true);
            GetComponent<EnemyMovement>().isAlive = false;
        }
        else
        {
            UpdatedHealth();
        }
    }

    void UpdatedHealth()
    {
        image.sprite = spriteArray[health];
    }
}
