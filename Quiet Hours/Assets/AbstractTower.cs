using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
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
    public float range;

    public enum towerType { SingleTarget, AOE, Cocount };

    public towerType myTowerType;


    public float timeStamp;
    public float fireCoolDown = 0.2F;
    private float nextFireTime = 0.0F;
    public MainLoop theLoop;

    private AudioSource audiosrc;
    private bool IsFiring;


    //Plays the set audio clip one time
    void playAudio()
    {
        audiosrc.PlayOneShot(TowerBeat, audioLevel);
        //yield WaitForSeconds(BeatRate)
    }
	// Use this for initialization
	void Start () {
        theLoop = GameObject.Find("MainGame").GetComponent<MainLoop>();
        if (theLoop != null) Debug.Log("MainGame object found");

        audiosrc = GetComponent<AudioSource>(); //assigns audio source to be played
        if(audiosrc != null) Debug.Log("Audiosource found");
    }
	// Update is called once per frame
	void Update () {
        //Cooldown code
        if (Time.time > nextFireTime)
        {
            if (myTowerType == towerType.SingleTarget)
            {
                Enemy tempEnemy = theLoop.getBestTarget(gameObject);
                doDamage(tempEnemy);
            }
            else if (myTowerType == towerType.AOE)
            {
                theLoop.damageAllInArea(gameObject);
            }
            else if (myTowerType == towerType.Cocount)
            {
                //Fill this in later
            }
            nextFireTime = Time.time + fireCoolDown;
        }
        else Debug.Log("Bass Canon cooling down: " + nextFireTime + " | " + Time.time);
    }

    public void doDamage(Enemy someEnemy)
    {
        timeStamp = Time.time + fireCoolDown;
        //someEnemy.takeDamage(BaseDamage);
        Debug.Log("Ima firin ma lazer");
    }
    class Projectile
    {
        public List<Enemy> targetList; //= new List<Enemy>();

        public Enemy currentTarget; //Assigns the current target of the bounce
        public GameObject projectile;
        public AbstractTower parentTower;

        public int bounceNumber; //Number of bounces before sound wave is absorbed/is dampened
        public double cooldown;

        public float BlastRadius;
        public float range;
        public float Velocity;
        public float rotSpeed = 90.0F;
        public Transform mTarget;
        private Transform mTransform;

        public void addTarget(Enemy e)
        {
            targetList.Add(e);
        }
        public void onCollisionEnter(Collision col)
        {
            parentTower.playAudio();
        }
        //BongoCongo blat blat....MUMUMUMUMULTI-KILL_KILl_kill
        public void bounceAttack()
        {
            int currentBounces = 0;
            foreach(Enemy partypooper in targetList) {
                if(currentBounces < bounceNumber && partypooper != null)
                {
                    parentTower.doDamage(partypooper);
                    currentBounces++;
                    nextCustomer();
                }
            }
        }
        /**
         * Moves Projectile to the next enemy
         **/
        public void nextCustomer() 
        {
            Vector3 nextTarget = mTarget.position - mTransform.position;
            Quaternion rot = Quaternion.LookRotation(nextTarget);
            mTransform.rotation = Quaternion.RotateTowards(mTransform.rotation, rot, rotSpeed * Time.deltaTime);
            mTransform.position += mTransform.forward * Velocity * Time.deltaTime;
        }


    }
}
