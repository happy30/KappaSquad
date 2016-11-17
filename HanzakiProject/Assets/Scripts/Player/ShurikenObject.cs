// ShurikenObject by jordi

using UnityEngine;
using System.Collections;

public class ShurikenObject : MonoBehaviour
{
    public int attackPower;
    public float projectileSpeed;
    public Transform player;

    public Transform model;
    public Transform model2;

    public bool hit;

    public EnemyMovement enemy;

    public Vector3 shurikenDirection;

    void Awake()
    {
        player = GameObject.Find("PlayerModel").transform;
    }

    void Start()
    {
        if(player.gameObject.GetComponent<PlayerController>().xMovement > 0)
        shurikenDirection = player.transform.forward;
        transform.eulerAngles = new Vector3(90, 0, 0);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(!hit)
        {
            transform.Translate(shurikenDirection * projectileSpeed * Time.deltaTime);
            model2.Rotate(0, 0, 2000 * Time.deltaTime);
            model.Rotate(200 * Time.deltaTime, 0, 0);
        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Destructible")
        {
            projectileSpeed = 0;
            transform.SetParent(col.gameObject.transform);
            model2.Rotate(0, 0, 0);
            model.Rotate(0, 0, 0);
            hit = true;

            if(col.gameObject.tag == "Destructible")
            {
                col.gameObject.GetComponent<DestructibleScript>().DestroyObject();
            }
        } 
    }
}
