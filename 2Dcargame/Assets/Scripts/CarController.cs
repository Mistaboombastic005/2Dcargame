using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;


public enum PlayerDriveTrain { AWD, RWD, FWD }

public class CarController : MonoBehaviour
{
    public Rigidbody2D backTire;
    public Rigidbody2D frontTire;
    public Rigidbody2D rb;
    public float Enginehp;
    public PlayerDriveTrain playerDriveTrain;
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
    public static float gearRatio;
    public AnimationCurve TorqueCurve;
    public AnimationCurve soundVolumeCurve;
    public float breakForce;
    public float factor;


    public Rigidbody2D rb1;
    public static float KM_H;
    public CameraFollow _cameraFollow1;
    public GameObject slider;
    public float gasValue;
    public static bool startRace;
    public static GameObject racer;
    public static bool stagingRace = false;
    public float stagingTime;
    public float stagingTimeRemaining;
    public Light2D backLight;
    private float initialIntensity;
    private float Intensity;


    private void Start()
    {
        engineOn = false;
        _cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        gearSelected = 0;

        slider = GameObject.FindGameObjectWithTag("Slider");

        stagingTimeRemaining = stagingTime;
        stagingRace = false;
        initialIntensity = backLight.intensity;
    }

    void Update()
    {
        gearSelected = Mathf.Clamp(gearSelected, -1, highestGear);
        if(rpm > rpmLimit)
        {
            factor = 0;
            rpm = rpmLimit;
        }
        else
        {
            factor = 1;
        }

        gasValue = slider.GetComponent<Slider>().value;

        if (stagingRace)
        {
            if (transform.position.x - racer.transform.position.x <= 5 && transform.position.x - racer.transform.position.x >= -5 && racer.GetComponent<BotAI>().state == State.Cruise)
            {
                stagingTimeRemaining = stagingTime;
                stagingTimeRemaining -= Time.time;
                Debug.Log(stagingTimeRemaining);
            }
            else
            {
                stagingTimeRemaining = stagingTime;
            }
        }
        if (stagingTimeRemaining <= 0)
        {
            startRace = true;
            stagingRace = false;
        }
        else
        {
            startRace = false;
        }

        if (startRace)
        {
            racer.GetComponent<BotAI>().state = State.Racing;
        }
        

        staticRPM = rpm;

        Intensity = initialIntensity + 1f;
        if (Input.GetKey("a") || MobileInput.brake)
        {
            backLight.intensity = Mathf.Lerp(backLight.intensity, Intensity, 0.3f);

            if (rotationalSpeed < 0)//forward
            {
                if (velocity> 0.1)
                {
                    frontTire.angularVelocity -= breakForce;
                    backTire.angularVelocity -= breakForce;

                }
                else
                {
                    frontTire.freezeRotation = true;
                    backTire.freezeRotation = true;
                }

            }
            if (rotationalSpeed > 0)//backward
            {
                if (velocity< -0.1)
                {
                    frontTire.angularVelocity += breakForce;
                    backTire.angularVelocity += breakForce;
                }
                else
                {
                    frontTire.freezeRotation = true;
                    backTire.freezeRotation = true;
                }
            }
        }
        else
        {
            backLight.intensity = Mathf.Lerp(backLight.intensity, initialIntensity, 0.3f); ;
            frontTire.freezeRotation = false;
            backTire.freezeRotation = false;
        }
        rotationalSpeed = -1 * frontTire.angularVelocity;

        //awd
        if (engineOn && transEngaged && !EngineSound.startEngine)
        {
            if (playerDriveTrain == PlayerDriveTrain.AWD)
            {
                frontTire.AddTorque(hp * gasValue * gearRatio * factor * -1 * Time.deltaTime);
                backTire.AddTorque(hp * gasValue * gearRatio * factor * -1 * Time.deltaTime);
            }








            //rwd

            if (playerDriveTrain == PlayerDriveTrain.RWD)
            {
                backTire.AddTorque(Engine.hp * gasValue * Engine.gearRatio * factor * -1 * Time.deltaTime);
            }







            //fwd

            if (playerDriveTrain == PlayerDriveTrain.FWD)
            {
                frontTire.AddTorque(hp * gasValue * gearRatio * factor * -1 * Time.deltaTime);
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
                rpm = Mathf.Lerp(rpm, 7000 * gasValue,0.01f);
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
