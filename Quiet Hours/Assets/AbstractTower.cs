using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AbstractTower : MonoBehaviour {

    public double BaseCost;
    public double fireRate;
    public double UpgradeCost;
    public double BaseDamage;
    public double BaseFirerate;
    public int BeatRate; //Sponsored by DJ Lucio
    public GameObject CurrentTarget;
    public bool isSelected = false;
    public AudioClip TowerBeat;
    public float audioLevel;

    private AudioSource audiosrc;
    private bool IsFiring;


    //Plays the set audio clip one time
    void playAudio()
    {
        if (IsFiring == true)
        {
            audiosrc.PlayOneShot(TowerBeat, audioLevel);
            //yield WaitForSeconds(BeatRate);
        }
    }
	// Use this for initialization
	void Start () {
        audiosrc = GetComponent<AudioSource>(); //assigns audio source to be played
    }
	// Update is called once per frame
	void Update () {
		
	}
}
