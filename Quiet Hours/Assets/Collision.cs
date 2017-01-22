
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
 
