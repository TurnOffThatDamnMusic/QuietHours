using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoop : MonoBehaviour {

    private List<EnemyClass> myEnemies;
    private List<TowerClass> myTowers;
    public GameObject baseEnemy;

    //Used by the enemies to pathfind
    public Vector3 Waypoint1;
    public Vector3 Waypoint2;
    public Vector3 Waypoint3;
    public Vector3 Waypoint4;
    public Vector3 Waypoint5;
    public Vector3 Waypoint6;
    public Vector3 Waypoint7;

	// Use this for initialization
	void Start () {
        myEnemies = new List<EnemyClass>();
        myTowers = new List<TowerClass>();
        Waypoint1 = new Vector3(-4.0f, 0.0f);
        Waypoint2 = new Vector3(-4.0f, 5.0f);
        Waypoint3 = new Vector3(0.0f, 5.0f);
        Waypoint4 = new Vector3(0.0f, -5.0f);
        Waypoint5 = new Vector3(4.0f, -5.0f);
        Waypoint6 = new Vector3(4.0f, 0.0f);
        Waypoint7 = new Vector3(8.0f, 0.0f);

        instantiateEnemy();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void intantiateTower(Object someObject)
    {
        
    }

    public void instantiateEnemy()
    {
        GameObject tempEnemy = (GameObject)Instantiate(baseEnemy);
        EnemyClass anotherTemp = new EnemyClass(tempEnemy);
        anotherTemp.enemyScript.theLoop = this;
        anotherTemp.enemyScript.target = Waypoint1;
        anotherTemp.enemyScript.stage = 1;
        myEnemies.Add(anotherTemp);
    }

    public Enemy getBestTarget(GameObject theTower)
    {
        EnemyClass closestEnemy = myEnemies[0];
        foreach(EnemyClass anEnemy in myEnemies)
        {
            if (Vector3.Distance(theTower.transform.position, anEnemy.theEnemy.transform.position) < Vector3.Distance(theTower.transform.position, closestEnemy.theEnemy.transform.position))
            {
                closestEnemy = anEnemy;
            }
        }

        return closestEnemy.enemyScript;
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
        else if(someEnemy.stage == 5)
        {
            someEnemy.target = Waypoint6;
        }
        else
        {
            someEnemy.target = Waypoint7;
        }

        someEnemy.stage++;
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
