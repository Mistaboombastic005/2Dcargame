using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody2D backTire;
    public Rigidbody2D frontTire;
    public Rigidbody2D rb;
    public float Enginehp;
    public string drivetrain;
    public float velocity;
    public CameraFollow _cameraFollow;
    [SerializeField] Speedometer _speedometer;
    public static float rotationalSpeed;



    [System.Serializable]
    public class gears
    {
        public int GearNumber;
        public float gearRatio;
    }
    public gears[] _gears;


    public float rpm;
    public float rpmLimit;
    public float idleRpm;
    public int gearSelected;
    public int highestGear;
    public static bool transEngaged;
    public static bool engineOn;
    public bool engineStarting;
    public static float hp;
    public float peakHp;
    public static float gearRatio;
    public float peakRpm;
    public AnimationCurve TorqueCurve;
    public AnimationCurve soundVolumeCurve;


    public Rigidbody2D rb1;
    public static float KM_H;
    public CameraFollow _cameraFollow1;

    public AudioSource audioSource1;
    private AudioClip currentClip1;
    private AudioClip currentClip2;
    public AudioClip starterSound;
    public AudioClip idleSound;
    public AudioClip firstSound;
    private bool playSound;

    private void Start()
    {
        //currentClip1 = idleSound;
    }

    void FixedUpdate()
    {

        rotationalSpeed = -1 * backTire.angularVelocity;
        //awd
        if (engineOn && transEngaged)
        {
            if (Input.GetKey("d") && drivetrain == "awd")
            {

                frontTire.AddTorque(hp * gearRatio * -1 * Time.deltaTime);
                backTire.AddTorque(hp * gearRatio * -1 * Time.deltaTime);
            }








            //rwd

            if (Input.GetKey("d") && drivetrain == "rwd")
            {
                backTire.AddTorque(Engine.hp * Engine.gearRatio * -1 * Time.deltaTime);
            }







            //fwd

            if (Input.GetKey("d") && drivetrain == "fwd")
            {
                frontTire.AddTorque(Engine.hp * Engine.gearRatio * -1 * Time.deltaTime);
            }
        }
    }
    
    private void PlaySound()
    {
        if (playSound)
        {
            audioSource1.Play();
            playSound = false;
        }
    }
    
    public void Update()
    {
        GameObject Car = gameObject.transform.GetChild(1).gameObject;

        frontTire = gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
        backTire = gameObject.transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>();


        audioSource1.clip = currentClip1;

        if (engineOn)
        {
            audioSource1.pitch = rpm / 5000 + 0.84f;
            if (!Input.GetKey("d") && rpm >= idleRpm && rpm <= 1000)
            {
                currentClip1 = idleSound;
            }
            if (Input.GetKey("d") && rpm >= 1000 && rpm <= 3000)
            {
                currentClip2 = firstSound;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                audioSource1.Play();
            }

        }

        
        velocity = 3.6f * (rb.velocity.magnitude);

        //_cameraFollow.zoom(velocity);


        gearRatio = (_gears[gearSelected + 1].gearRatio) / 2;

        hp = TorqueCurve.Evaluate(rpm);

        if (Input.GetKeyDown("x"))
        {
            if (gearSelected < highestGear)
            {
                gearSelected++;
            }
        }
        if (Input.GetKeyDown("z"))
        {
            if (gearSelected > -1)
            {
                gearSelected--;
            }
        }
        //Debug.Log(hp);
        if (Input.GetKeyDown("v") && (engineStarting == false))
        {
            Debug.Log("KYS");
            
            if (engineOn == true)
            {
                engineOn = false;
            }
            else
            {
                engineStarting = true;
                Debug.Log("WTF");
            }

        }
        if (engineStarting)
        {
            StartCoroutine(PlaySound());
        }
        if (engineOn)
        {
            if (rpm < idleRpm)
            {
                rpm += 800 * Time.deltaTime;
            }

            if (gearSelected != 0)
            {
                rpm = Mathf.Abs((CarController.rotationalSpeed) * gearRatio * 5 + idleRpm);
            }
        }
        if (gearSelected != 0)
        {
            transEngaged = true;
        }
        else
        {
            transEngaged = false;
            if (Input.GetKey("d") && (rpm < rpmLimit))
            {
                rpm += 7000 * Time.deltaTime;
            }
            else
            {
                rpm -= ((rpm - idleRpm) / 1.5f) * Time.deltaTime;
            }
        }


        IEnumerator PlaySound()
        {
            engineStarting = false;
            currentClip1 = starterSound;
            audioSource1.clip = currentClip1;
            audioSource1.Play();
            audioSource1.loop = false;
            yield return new WaitForSeconds(starterSound.length);
            engineOn = true;
            audioSource1.clip = idleSound;
            audioSource1.Play();
            audioSource1.loop = true;
        }

    }
}
