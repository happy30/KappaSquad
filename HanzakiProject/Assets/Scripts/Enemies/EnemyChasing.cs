using UnityEngine;
using System.Collections;

public class EnemyChasing : MonoBehaviour {
    NavMeshAgent agent;
    public Transform target;
    public int damage;
    private Animator anim;
    public float timer; 

	void Awake()
    {
	    agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
	}
	
	public void Running (Transform target)
    {
        agent.SetDestination(target.position);
	}

    void OnCollisionStay (Collision trigger)
    {
        if(trigger.transform.tag == "Player")
        {
       //   trigger.GetComponent<PlayerStats>().GetHit(damage);
        }
        if(trigger.transform.tag == "Rock")
        {
            anim.SetBool("Death", true);
            if(timer > anim.runtimeAnimatorController.animationClips.Length)
            {
                Destroy(gameObject);
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
