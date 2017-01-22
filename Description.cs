using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Description : MonoBehaviour {
    public Text test;
    public bool visible;

	// Use this for initialization
	void Start () {
        test = GetComponent<Text>();
        visible = false;
        test.color = new Color(1, 1, 1, 0);
        test.text = "SUPERTEST";
	}
	
	// Update is called once per frame
	void Update () {
        if (visible)
        {
            test.color = new Color(1, 1, 1, 1);
        }
        else
        {
            test.color = new Color(1, 1, 1, 0);
        }
    }

    public void setText(string body)
    {
        test.text = body;
    }
}
