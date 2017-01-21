using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public double BaseCost;
    public double UpgradeCost;
    public int BaseDamage;
    public double BaseFirerate;
    public int BeatRate; //Sponsored by DJ Lucio

    public float timeStamp;
    public float fireCoolDown = 1.0F;
    public MainLoop theLoop;

    private bool IsFiring;

	// Use this for initialization
    void Start()
    {
        theLoop = GameObject.Find("MainGame").GetComponent<MainLoop>();
    }
	
	// Update is called once per frame
    void Update()
    {
        //Every few seconds
        timeStamp = Time.time + fireCoolDown;
        while (timeStamp >= Time.time)
        {

        }

        theLoop.damageAllInArea(gameObject);

    }

    public void doDamage(Enemy someEnemy)
    {
        someEnemy.takeDamage(BaseDamage);
    }
}
