using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AbstractTower : MonoBehaviour {

    public double BaseCost;
    public double UpgradeCost;
    public int BaseDamage;
    public double BaseFirerate;
    public int BeatRate; //Sponsored by DJ Lucio
    public GameObject CurrentTarget;
    public bool isSelected = false;
    public AudioClip TowerBeat;
    public float audioLevel;

    public float timeStamp;
    public float fireCoolDown = 1.0F;
    public MainLoop theLoop;

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
        //Every few seconds
        timeStamp = Time.time + fireCoolDown;
        if(timeStamp <= Time.time)
        {
            //TODO : Timing and when to actually fire
            Enemy tempEnemy = theLoop.getBestTarget(gameObject);
            // doDamage(tempEnemy);
        }

    }

    public void doDamage(Enemy someEnemy)
    {
        someEnemy.takeDamage(BaseDamage);
    }

    class Projectile
    {
        public List<Enemy> targetList = new List<Enemy>();
        public Enemy currentTarget; //Assigns the current target of the bounce
        public GameObject projectile;
        public int bounceNumber; //Number of bounces before sound wave is absorbed/is dampened
        public float BlastRadius;
        public float range;
        public double cooldown;

        public void addTarget(Enemy e)
        {
            targetList.Add(e);
        }

        //BongoCongo blat blat....MUMUMUMUMULTI-KILL_KILl_kill
        public void bounceAttack()
        {
            int currentBounces = 0;
            foreach(Enemy partypooper in targetList) {
                if(currentBounces < bounceNumber)
                {
                    
                }
            }
        }


    }
}
