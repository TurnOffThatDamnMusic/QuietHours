using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {
    public int ID;
    public Description body;
    public Transform Turret;
    private Ray ray;
    private RaycastHit hit;
    public string thing;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        /* if(Input.mousePosition.x>0 && Input.mousePosition.x < box.size.x
             && Input.mousePosition.y > 0 && Input.mousePosition.y < box.size.y){
             body.setText("This tower goes pew");
             body.visible = true;
         }
         else
         {
             body.visible = false;
         }*/

        /*ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast)*/
    }

    public void vis()
    {
        body.setText(thing);
        body.visible = true;
    }
    public void invis()
    {
        body.visible = false;
    }
}
