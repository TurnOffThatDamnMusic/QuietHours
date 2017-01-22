using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainLoop : MonoBehaviour {

    private List<EnemyClass> myEnemies;
    private List<TowerClass> myTowers;
    public GameObject baseEnemy;
    public GameObject redBox;
    public GameObject greenBox;

    //Used by the enemies to pathfind
    public Vector3 Waypoint1;
    public Vector3 Waypoint2;
    public Vector3 Waypoint3;
    public Vector3 Waypoint4;
    public Vector3 Waypoint5;
    public Vector3 Waypoint6;
    public Vector3 Waypoint7;

    //List of boxes to destroy later
    private List<GameObject> boxes;
    //Useable Squares
    private List<Square> squares;

    public GameObject[] randEnemies = new GameObject[5];

    float timeGo;

	// Use this for initialization
	void Start () {
        myEnemies = new List<EnemyClass>();
        myTowers = new List<TowerClass>();
        boxes = new List<GameObject>();
        squares = new List<Square>();
        Waypoint1 = new Vector3(-4.0f, 0.0f);
        Waypoint2 = new Vector3(-4.0f, 5.0f);
        Waypoint3 = new Vector3(0.0f, 5.0f);
        Waypoint4 = new Vector3(0.0f, -5.0f);
        Waypoint5 = new Vector3(4.0f, -5.0f);
        Waypoint6 = new Vector3(4.0f, 0.0f);
        Waypoint7 = new Vector3(8.0f, 0.0f);

        instantiateEnemy();

        for (int i = -7; i < 8; i++)
        {
            for (int j = -10; j < 12; j++)
            {
                //Waypoint 0 to Waypoint 1
                if((j <= -4 && j>=-8) && (i == 0)){
                    squares.Add(new Square(new Vector3(j, i), true));
                }
                //Waypoint 1 to Waypoint 2
                else if ((j == -4) && (i <=5 && i >= 0))
                {
                    squares.Add(new Square(new Vector3(j, i), true));
                }
                //Waypoint 2 to Waypoint 3
                else if((j<=0 && j >= -4) &&(i == 5)){
                    squares.Add(new Square(new Vector3(j, i), true));
                }
                //Waypoint 3 to Waypoint 4
                else if((j==0) && (i >= -5 && i <=5)){
                    squares.Add(new Square(new Vector3(j, i), true));
                }        
                //Waypoint 4 to Waypoint 5
                else if((j >= 0 && j <= 4) && (i == -5)){
                    squares.Add(new Square(new Vector3(j, i), true));
                }
                //Waypoint 5 to Waypoint 6
                else if((j == 4) && (i >= -5 && i <= 0)){
                    squares.Add(new Square(new Vector3(j, i), true));
                }
                //Waypoint 6 to Waypoint 7
                else if((j >= 4 && j <= 8) && (i == 0)){
                    squares.Add(new Square(new Vector3(j, i), true));
                }
                else
                {
                    squares.Add(new Square(new Vector3(j, i), false));
                }
            }
        }

        /*
        foreach (Square aSquare in squares)
        {
            if (aSquare.inUse)
            {
                GameObject tempBox = (GameObject)Instantiate(redBox, aSquare.position, Quaternion.identity);
            }
            else
            {
                GameObject tempBox = (GameObject)Instantiate(greenBox, aSquare.position, Quaternion.identity);
            }
        }
         * */

        timeGo = Time.time;

	}

    // Update is called once per frame
    // Spawn enemies
    int maxEnemies = 15;
    int currentEnemies = 1;
    void Update () {
        float currentTime = Time.time;
		if(currentEnemies < maxEnemies)
        {
            if (Time.time - timeGo > .2)
            {
                currentEnemies++;
                instantiateEnemy();
                timeGo = Time.time;
            }
        }
	}

    public void instantiateTower(GameObject someObject)
    {
        
    }

    public void instantiateEnemy()
    {
        int rIndex = UnityEngine.Random.Range(0, randEnemies.Length);
        GameObject whatEnemy = randEnemies[rIndex];
        GameObject tempEnemy = (GameObject)Instantiate(whatEnemy);
        EnemyClass anotherTemp = new EnemyClass(tempEnemy);
        anotherTemp.enemyScript.theLoop = this;
        anotherTemp.enemyScript.target = Waypoint1;
        anotherTemp.enemyScript.stage = 1;
        myEnemies.Add(anotherTemp);
    }

    public Enemy getClosestTarget(GameObject theTower)
    {
        EnemyClass closestEnemy = null;
        foreach (EnemyClass anEnemy in myEnemies)
        {
            if (Vector3.Distance(theTower.transform.position, anEnemy.theEnemy.transform.position) < Vector3.Distance(theTower.transform.position, closestEnemy.theEnemy.transform.position))
            {
                closestEnemy = anEnemy;
            }
        }

        return closestEnemy.enemyScript;
    }


    public void damageAllInArea(GameObject theTower){
        foreach (EnemyClass anEnemy in myEnemies)
        {
            if (Vector3.Distance(theTower.transform.position, anEnemy.theEnemy.transform.position) < theTower.GetComponent<AbstractTower>().range)
            {
                anEnemy.enemyScript.takeDamage(theTower.GetComponent<AbstractTower>().BaseDamage);
            }
        }
    }

    public GameObject getRandomInRange(GameObject anObject, float range)
    {
        List<EnemyClass> inRangeEnemies = new List<EnemyClass>();

        if (myEnemies.Count != 0)
        {
            foreach (EnemyClass anEnemy in myEnemies)
            {
                if (Vector3.Distance(anObject.transform.position, anEnemy.theEnemy.transform.position) < range)
                {
                    inRangeEnemies.Add(anEnemy);
                }
            }
            int rand = (int)(UnityEngine.Random.value * inRangeEnemies.Count);

            //Debug.Log("Random is : " + rand);
            return inRangeEnemies[rand].theEnemy;

        }
        else
        {
            return null;
        }
    }


    //TODO CHANGE DAMAGE TO RANGE
    public GameObject getBestTarget(GameObject theTower, float range)
    {
        List<EnemyClass> inRangeEnemies = new List<EnemyClass>();
        if (myEnemies.Count != 0)//Range checking will ensure a swift victory
        {
            EnemyClass bestEnemy = null;

            foreach (EnemyClass anEnemy in myEnemies)
            {
                if (Vector3.Distance(theTower.transform.position, anEnemy.theEnemy.transform.position) < range)
                {
                    Debug.Log("Enemy added");
                    inRangeEnemies.Add(anEnemy);
                }
            }
            int bestStage = 0;
            float lowestDistance = 1000000f; //Oh no. Lookout its a Snip...! *thud*

            //TODO; Optimize
            foreach (EnemyClass anEnemy in inRangeEnemies)
            {
                if (anEnemy.enemyScript.stage > bestStage)
                {
                    bestStage = anEnemy.enemyScript.stage;
                }
            }

            foreach (EnemyClass anEnemy in inRangeEnemies)
            {
                if (anEnemy.enemyScript.stage == bestStage)
                {
                    float tempDist = Vector3.Distance(anEnemy.enemyScript.target, anEnemy.theEnemy.transform.position);
                    if (tempDist < lowestDistance)
                    {
                        lowestDistance = tempDist;
                        bestEnemy = anEnemy;
                    }
                }
            }
            if (bestEnemy != null)
            {
                Debug.Log("Best Enemy Found");
                return bestEnemy.theEnemy;
            }
            else
            {
                return null;
            }
        } else {
            return null;
        }
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
        if (myEnemies.Count != 0) //Range checking will ensure a swift victory
        {
            foreach (EnemyClass Enmy in myEnemies)
            {
                if (Enmy.theEnemy == someObject)
                {
                    myEnemies.Remove(Enmy);
                    someObject.GetComponent<Enemy>().killMyself();
                }
            }
        }
        else Debug.Log("No enemies to drop beats on");

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
        public AbstractTower towerScript;

        public TowerClass(GameObject aTower)
        {
            theTower = aTower;
            towerScript = aTower.GetComponent<AbstractTower>();
        }
    }

    public class Square
    {
        public Vector3 position;
        public bool inUse;

        public Square(Vector3 squarePosition, bool use)
        {
            position = squarePosition;
            inUse = use;
        }
    }
}
