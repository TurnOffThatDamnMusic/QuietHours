using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoop : MonoBehaviour {

    private List<EnemyClass> myEnemies;
    private List<TowerClass> myTowers;

    public Transform Waypoint1;
    public Transform Waypoint2;
    public Transform Waypoint3;
    public Transform Waypoint4;
    public Transform Waypoint5;
    public Transform Waypoint6;

	// Use this for initialization
	void Start () {
        myEnemies = new List<EnemyClass>();
        myTowers = new List<TowerClass>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void intantiateTower(Object someObject)
    {
        
    }

    public void instantiateEnemy()
    {

    }

    public void giveNextWaypoint(Enemy someEnemy)
    {
        if (someEnemy.stage == 1)
        {
            someEnemy.target = Waypoint2;
        }
        else if(someEnemy.stage == 2){
            someEnemy.target = Waypoint3;
        }
        else if (someEnemy.stage == 3)
        {
            someEnemy.target = Waypoint4;
        }
        else if (someEnemy.stage == 4)
        {
            someEnemy.target = Waypoint5;
        }
        else
        {
            someEnemy.target = Waypoint6;
        }
    }



    //Kill an enemy. They call this function usually
    public void killEnemy(GameObject someObject)
    {
        foreach(EnemyClass Enmy in myEnemies){
            if (Enmy.theEnemy == someObject) {
                myEnemies.Remove(Enmy);
                someObject.GetComponent<Enemy>().killMyself();
            }
        }
    }

    public void killTower(GameObject someObject)
    {
        foreach (TowerClass Tow in myTowers)
        {
            if (Tow.theTower == someObject)
            {
                myTowers.Remove(Tow);
                //TODO : Tell it to destroy itself
            }
        }
    }

    //The classes hold both an the script to call their functions easily and the gameobject
    //It just makes stuff a bit easier and cleaner
    private class EnemyClass
    {
        public GameObject theEnemy;
        public Enemy enemyScript;
        
        public EnemyClass(GameObject anEnemy)
        {
            theEnemy = anEnemy;
            enemyScript = anEnemy.GetComponent<Enemy>();
        }
    }


    private class TowerClass
    {
        public GameObject theTower;
        //public Tower towerScript;

        public TowerClass(GameObject aTower)
        {
            theTower = aTower;
            //towerScript = aTower.GetComponent<Enemy>();
        }
    }
}
