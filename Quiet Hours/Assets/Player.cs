using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    int startingHealth = 25;
    int power = 0;
    int roundNumber = 0;
	// Use this for initialization
	void Start () {
   //     power = startingPower();
        roundNumber = 1;
	}



    // Update is called once per frame
    void Update () {
    //check damage

    //check alive

    //check turret purchase

    }

    //Checks what the player is clicking on
    /*public void OnMouseDown()
    {
        if (isTurret())
        {

        }
        else if (selectedTurret())
        {

        }
    }*/

    //Checks if there is a turret there
    public bool canPlaceTurret()
    {
        return false;
    }

    /*//checks to see if you have the funds to buy a turret
    public bool canBuyTurret(GameObject Turret)
    {
        if (Turret.GetComponent<turret>().cost()<power)
        {
            power = power - Turret.GetComponent<turret>().cost();
            return true;
        }
        return false;
    }*/
    //selects the turret
    public bool selectedTurret()
    {
        return false;
    }
    //Player takes damage
    /*public int damage(GameObject monster)
    {
        //if the monster collides with the house it deals damage to you equal to its damage value
        int damage = 0;
        if (monster.GetComponent<enemy>().damage())
        {
            damage = monster.GetComponent<enemy>().damage());
        }
        startingHealth = startingHealth - damage;
        if (isDead())
        {
            //end the game
        }
        return startingHealth;
    }*/
    //checks to see if you are dead
    public bool isDead()
    {
        if (startingHealth <= 0)
        {
            return true;
        }
        else {
            return false;
        }
    }
    //checks to see if you have selected a turret
    public bool isTurret()
    {
        return false;
    }

    public int startingPower()
    {
        int startingPower = 100 * roundNumber;
        return startingPower;
    }
    
    
}
