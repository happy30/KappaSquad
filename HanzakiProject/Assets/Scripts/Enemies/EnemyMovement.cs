//EnemyMovement by Alieke
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;

    public List<Transform> wayPoints = new List<Transform>();
    public float walkSpeed;
    public float runSpeed;

    public int currentWayPoint;
    public float rangeOffSet;
    public float maxWayPoints;
    private Animator anim;
    public bool isAlive;
    public bool isChasing;
    public float lookOutTimer;
    public float minLookOutTime;
    public float maxLookOutTime;

    public float attackRange;
    public int attackDamage;
    public float restartAttack;
    public float testFloat;
    public float mayAttack;
    public RaycastHit hit;
    public float rayDis;

    public int health;
    private Image image;
    public GameObject healthSprite;
    public List<Sprite> spriteArray = new List<Sprite>();
    public float distance;

    void Awake()
    { 
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        image = healthSprite.GetComponent<Image>();
    }

    void Update()
    {
        if (isAlive)
        {
            if (gameObject.GetComponent<EnemySight>().playerInView == true)
            {
                isChasing = true;
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
        if (isChasing)
        {
            Chase();
        }
    }

    void Patrol()
    {
        if(currentWayPoint >= wayPoints.Count)
        {
            currentWayPoint = 0;
        }
        agent.SetDestination(wayPoints[currentWayPoint].position);

        distance = Vector3.Distance(wayPoints[currentWayPoint].position, transform.position);
        if(distance < 3)
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

    void Chase()
    {
        agent.speed = runSpeed;
        anim.SetFloat("WalkSpeed", agent.speed);
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < attackRange)
        {
            Attacking();
        }
        else
        {
            mayAttack = restartAttack;
            anim.SetBool("AttackRange", false);
            agent.speed = walkSpeed;
            anim.SetFloat("WalkSpeed", walkSpeed);
            isChasing = false;
        }
    }

    void Attacking()
    {
        if (mayAttack <= 0)
        {
            anim.SetBool("AttackRange", true);
            agent.speed = 0;

            float distance = Vector3.Distance(player.position, transform.position);
            if (distance > attackRange)
            {
             
            }
        }
        else
        {
            mayAttack -= Time.deltaTime;
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDis))
        {
            if (hit.transform.tag == "Player")
            {
                hit.transform.GetComponent<PlayerController>().GetHit(attackDamage);
            }

        }

    }

    void GetHit(int damageGet)
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
