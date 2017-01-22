using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

[RequireComponent(typeof(AudioSource))]
public class AbstractTower : MonoBehaviour {

    public double BaseCost;
    public double UpgradeCost;
    public int BaseDamage;
    public double BaseFirerate;
    public int BeatRate; //Sponsored by DJ Lucio
    public GameObject CurrentTarget;
    public bool isSelected = false;
    public AudioClip[] TowerBeats = new AudioClip[1]; //Holds all audioclips for tower/projectile sounds
    public float audioLevel; //Sound level
    public float range;

    public enum towerType { SingleTarget, AOE, Cocount };

    public towerType myTowerType;


    public float timeStamp;
    public float fireCoolDown = 0.2F; //Time between shots for a tower
    private float nextFireTime = 0.0F;
    public MainLoop theLoop;

    private AudioSource audiosrc;
    private bool IsFiring;

    public GameObject coconut;
    public float coconutRange;

    private List<Coco> theCocos;
    /**
      *  Projectile Variables
      *  
     **/  
    public Enemy currentTarget; //Assigns the current target of the bounce
    public GameObject projectile;
    public Transform mTarget;
    private Transform mTransform;

    public int bounceNumber; //Number of bounces before sound wave is absorbed/is dampened
    public double cooldown;

    public float BlastRadius;
    public float Velocity;
    public float rotSpeed = 90.0F;

    //AudioSource aSource = GameObject.Find("MainGame").AddComponent<AudioSource>();
    //Plays the set audio clip one time
    void playAudio()
    {
        //audiosrc = aSource;
        //AudioClip currentBeat;
        if (TowerBeats.Length > 1)
        {
            int rIndex = Random.Range(0, TowerBeats.Length); //Randomly select an audioclip to play 
            audiosrc.PlayOneShot(TowerBeats[rIndex], audioLevel);

        } else
        {
            audiosrc.PlayOneShot(TowerBeats[0], audioLevel);
        }
    }
	// Use this for initialization
	void Start () {
        theCocos = new List<Coco>();
        //theLoop = GameObject.Find("MainGame").GetComponent<MainLoop>();
        if (theLoop != null) Debug.Log("MainGame object found");

        audiosrc = GetComponent<AudioSource>(); //assigns audio source to be played
        if (audiosrc != null) Debug.Log("Audiosource found"); else Debug.Log("Audiosource NOT found!!!");
    }
    //BongoCongo blat blat....MUMUMUMUMULTI-KILL_KILl_kill
    public void CoconutAttack(Coco aCoco)
    {
        aCoco.currentBounce++;
        GameObject jumpTarget = theLoop.getRandomInRange(aCoco.theCoco, coconutRange);
        doDamage(jumpTarget.GetComponent<Enemy>());
        //playAudio();
        if (jumpTarget != null)
        {
            Debug.Log("We are getting a legit jump target");
            //tempCoco.transform.position = Vector3.MoveTowards(tempCoco.transform.position, jumpTarget.transform.position, step);

            //Vector3 dir2 = jumpTarget.transform.position - aCoco.theCoco.transform.position;
            //float angle2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;
            //aCoco.theCoco.transform.rotation = Quaternion.Euler(0f, 0f, angle2 - 90);

            aCoco.currentTarget = jumpTarget;
        }
        else
        {
            Destroy(aCoco.theCoco);
            theCocos.Remove(aCoco);
        }

    }

	// Update is called once per frame
	void Update () {
        //Cooldown code

        //Updating the cocos and keeping them moving
        foreach (Coco aCoco in theCocos)
        {
            if (aCoco.currentBounce > aCoco.maxBounce)
            {
                Destroy(aCoco.theCoco);
                theCocos.Remove(aCoco);
            }
            else
            {
                if (aCoco != null && aCoco.currentTarget != null)
                {
                    float step = aCoco.speed * Time.deltaTime;
                    aCoco.theCoco.transform.position = Vector3.MoveTowards(aCoco.theCoco.transform.position, aCoco.currentTarget.transform.position, step);

                    if (Vector3.Distance(aCoco.theCoco.transform.position, aCoco.currentTarget.transform.position) < .1)
                    {
                        CoconutAttack(aCoco);
                    }
                } else
                {
                    Debug.Log("Something is null: " + aCoco + " | " + aCoco.currentTarget);
                    CoconutAttack(aCoco);
                }
            }
        }

        if (Time.time > nextFireTime)
        {
            if (myTowerType == towerType.SingleTarget)
            {
                GameObject theEnemy = theLoop.getBestTarget(gameObject, range);
                
                if (theEnemy != null)
                {
                    Enemy tempEnemy = theEnemy.GetComponent<Enemy>();
                    doDamage(tempEnemy);
                    //playAudio();

                    GameObject tempCoco = (GameObject)Instantiate(coconut, transform.position, Quaternion.identity);

                    Coco aCoco = new Coco(tempCoco, theEnemy, 0, 15f);
                    theCocos.Add(aCoco);
                    //Debug.Log("The enemy position is x : " + theEnemy.transform.position.x + " y :  " + theEnemy.transform.position.y + " z : " + theEnemy.transform.position.z);

                    //Look at the target
                    Vector3 dir = theEnemy.transform.position - transform.position; 
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; 
                    transform.GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
                    

                    Debug.Log("The single target tower is firing");
                    nextFireTime = Time.time + fireCoolDown;
                }
            }

            else if (myTowerType == towerType.AOE)
            {
                theLoop.damageAllInArea(gameObject);
                Debug.Log("The AOE tower is firing");
                nextFireTime = Time.time + fireCoolDown;
            }

            else if (myTowerType == towerType.Cocount)
            {
                GameObject theEnemy = theLoop.getBestTarget(gameObject, range);

                if (theEnemy != null)
                {
                    Enemy tempEnemy = theEnemy.GetComponent<Enemy>();
                    doDamage(tempEnemy);
                    //playAudio();

                    //Look at the target
                    //Vector3 dir = theEnemy.transform.position - transform.position;
                    //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    //transform.GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);

                    GameObject tempCoco = (GameObject)Instantiate(coconut, transform.position, Quaternion.identity);

                    Coco aCoco = new Coco(tempCoco, theEnemy, 3, 10f);
                    theCocos.Add(aCoco);

                    nextFireTime = Time.time + fireCoolDown;

                }
            }
        }
    }

    public class Coco
    {
        public int currentBounce;
        public GameObject theCoco;
        public GameObject currentTarget;
        public int maxBounce;
        public float speed;

        public Coco(GameObject aCoco, GameObject aTarget, int max, float spee)
        {
            theCoco = aCoco;
            currentBounce = 0;
            currentTarget = aTarget;
            maxBounce = max;
            speed = spee;
        }
    }

    public void cocoBounce(GameObject tempCoco)
    {
        Debug.Log("We are coco bouncing");
        for (int i = 0; i < 3; i++)
        {
            GameObject jumpTarget = theLoop.getBestTarget(tempCoco, coconutRange);

            float step = 3f * Time.deltaTime;
            tempCoco.transform.position = Vector3.MoveTowards(tempCoco.transform.position, jumpTarget.transform.position, step);

            //Vector3 dir2 = jumpTarget.transform.position - tempCoco.transform.position;
            //float angle2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;
            //tempCoco.transform.rotation = Quaternion.Euler(0f, 0f, angle2 - 90);

            while (Vector3.Distance(tempCoco.transform.position, jumpTarget.transform.position) > .05)
            {
                //Do nothing and wait for it to get to the place
            }

        }
    }

    public void doDamage(Enemy someEnemy)
    {
        //timeStamp = Time.time + fireCoolDown;
        someEnemy.takeDamage(BaseDamage);
        //Debug.Log("Ima firin ma lazer");
    }
}
