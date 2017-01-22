using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyScript : MonoBehaviour {
    public MainLoop theLoop;
    public GameObject theBuildObject;

	// Use this for initialization
	void Start ()
    {
        theLoop = GameObject.Find("MainGame").GetComponent<MainLoop>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        theLoop.readyToBuild(theBuildObject);
    }
}
