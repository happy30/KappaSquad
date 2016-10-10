using UnityEngine;
using System.Collections;

public class ShurikenObject : MonoBehaviour
{
    public int attackPower;
    public float projectileSpeed;
    public Transform player;

    public EnemyMovement enemy;

    public Vector3 shurikenDirection;

    void Awake()
    {
        player = GameObject.Find("PlayerModel").transform;
    }

    void Start()
    {
        shurikenDirection = player.transform.forward;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.Translate(shurikenDirection * projectileSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Enemy")
        {
            projectileSpeed = 0;
            transform.SetParent(col.gameObject.transform);
        } 
    }
}
