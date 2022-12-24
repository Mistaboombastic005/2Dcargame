using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public Rigidbody2D backTire;
    public Rigidbody2D frontTire;
    public Rigidbody2D rb;
    public float Enginehp;
    public string driveTrain;
    public float velocity;
    public CameraFollow _cameraFollow;
    [SerializeField] Speedometer _speedometer;
    public static float rotationalSpeed;
    public static float speed;



    [System.Serializable]
    public class gears
    {
        public int GearNumber;
        public float gearRatio;
    }
    public gears[] _gears;


    public float rpm;
    public static float staticRPM;
    public float rpmLimit;
    public float idleRpm;
    public static int gearSelected = 0;
    public int highestGear;
    public static bool transEngaged;
    public static bool engineOn = false;
    public bool engineStarting;
    public static float hp;
    public float peakHp;
    public static float gearRatio;
    public float peakRpm;
    public AnimationCurve TorqueCurve;
    public AnimationCurve soundVolumeCurve;
    public float breakForce;


    public Rigidbody2D rb1;
    public static float KM_H;
    public CameraFollow _cameraFollow1;
    public GameObject slider;
    public float gasValue;
    public static bool startRace;
    public static GameObject racer;
    public static bool stagingRace = false;


    private void Start()
    {
        engineOn = false;
        _cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        gearSelected = 0;

        slider = GameObject.FindGameObjectWithTag("Slider");


    }

    void Update()
    {
        gearSelected = Mathf.Clamp(gearSelected, -1, highestGear);

        gasValue = slider.GetComponent<Slider>().value;

        if (stagingRace)
        {
            
        }
        
        if (startRace)
        {
            startRace = false;
            
        }
        

        staticRPM = rpm;


        if (Input.GetKey("a") || MobileInput.brake)
        {
            if (rotationalSpeed > 0)
            {
                if (rotationalSpeed < breakForce)
                {
                    frontTire.angularVelocity -= frontTire.angularVelocity * Time.deltaTime;
                }
                else
                {
                    frontTire.angularVelocity += breakForce * Time.deltaTime;
                }
            }
            if (rotationalSpeed < 0)
            {
                if (rotationalSpeed > breakForce)
                {
                    frontTire.angularVelocity += frontTire.angularVelocity * Time.deltaTime;
                }
                else
                {
                    frontTire.angularVelocity -= breakForce * Time.deltaTime;
                }

            }
        }        
        rotationalSpeed = -1 * frontTire.angularVelocity;

        //awd
        if (engineOn && transEngaged && !EngineSound.startEngine)
        {
            if (driveTrain == "awd")
            {
                frontTire.AddTorque(hp * gasValue * gearRatio * -1 * Time.deltaTime);
                backTire.AddTorque(hp * gasValue * gearRatio * -1 * Time.deltaTime);
            }








            //rwd

            if (driveTrain == "rwd")
            {
                backTire.AddTorque(Engine.hp * gasValue * Engine.gearRatio * -1 * Time.deltaTime);
            }







            //fwd

            if (driveTrain == "fwd")
            {
                frontTire.AddTorque(hp * gasValue * gearRatio * -1 * Time.deltaTime);
            }
        }   
        
        
        
        speed = velocity;
        GameObject Car = gameObject.transform.GetChild(1).gameObject;

        frontTire = gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
        backTire = gameObject.transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>();


        
        velocity = 3.6f * (rb.velocity.magnitude);

        _cameraFollow.zoom(velocity);


        gearRatio = (_gears[gearSelected + 1].gearRatio) / 3.4f;

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
        if (Input.GetKeyDown("v"))
        {
            if (!engineOn)
            {
                engineOn = true;
            }
            else
            {
                engineOn = false;
            }
        }
        if (engineOn)
        {
            
            if (rpm < idleRpm)
            {
                rpm += 800 * Time.deltaTime;
            }

            if (gearSelected != 0)
            {
                rpm = Mathf.Lerp(rpm, Mathf.Abs(rotationalSpeed * gearRatio) * 2.2f + idleRpm, 0.05f);
            }
        }
        if (gearSelected != 0)
        {
            transEngaged = true;
        }
        else
        {
            transEngaged = false;
            if (rpm < rpmLimit)
            {
                rpm = Mathf.Lerp(rpm, 7000 * gasValue,0.05f);
            }
            else
            {
                if (engineOn)
                {
                    rpm -= ((rpm - idleRpm) / 1.5f) * Time.deltaTime;
                }
            }
        }


        

    }
}
