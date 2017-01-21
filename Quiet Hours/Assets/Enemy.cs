using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    //Given to this object
    public float speed;
    public int MaxHealth;
    public int damageDoneToHouse;

    //Used by this object
    public int currentHealth;
    public MainLoop theLoop;
    public Vector3 target;
    public int stage;

	// Use this for initialization
	void Start () {
        currentHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        if (Vector3.Distance(transform.position, target) < .05)
        {
            changeTarget();
        }
	}

    void changeTarget()
    {
        theLoop.giveNextWaypoint(this);
    }

    public void takeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            killMe();
        }
        Debug.Log("The unit was damaged!");
    }

    private void killMe()
    {
        theLoop.killEnemy(this.gameObject);
    }

    public void killMyself()
    {
        Destroy(this);
    }
}
