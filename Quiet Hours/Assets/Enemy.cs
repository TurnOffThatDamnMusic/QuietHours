﻿using System.Collections;
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
        Debug.Log("I'm Alive: " + currentHealth);
	}
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        if (Vector3.Distance(transform.position, target) < .05)
        {
            changeTarget();
            Vector3 dir = target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
        }
	}

    void changeTarget()
    {
        theLoop.giveNextWaypoint(this);
        if (stage == 8)
        {
            //Damage the house before you die
            killMe();
        }
    }

    public void takeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            Debug.Log("I is kill. RIP.");
            killMe();
        }
        Debug.Log("The unit was damaged! Current Health: " + currentHealth);
    }

    private void killMe()
    {
        theLoop.killEnemy(gameObject);
        Debug.Log("Call to mainloop to kill an enemy");
    }

    public void killMyself()
    {
        Debug.Log("Call to destroy enemy game object");
        Destroy(gameObject);
    }
}
