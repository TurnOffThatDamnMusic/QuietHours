using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	public void LoadScene(int level)
    {
        Application.LoadLevel(level);
        
    }
}
