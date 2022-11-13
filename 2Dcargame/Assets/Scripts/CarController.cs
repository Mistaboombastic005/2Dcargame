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
    public static int gearSelected;
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
    public AnimationCurve breakForce;


    public Rigidbody2D rb1;
    public static float KM_H;
    public CameraFollow _cameraFollow1;


    void Update()
    {
        staticRPM = rpm;
        
        
        if(Input.GetKey("a") || MobileInput.brake)
        {
            frontTire.angularVelocity = Mathf.Lerp(frontTire.angularVelocity, 0, 5f);
        }
        else
        {
            frontTire.angularDrag = 0;
        }
        
        rotationalSpeed = -1 * backTire.angularVelocity;
        //awd
        if (engineOn && transEngaged)
        {
            if ((Input.GetKey("d") || MobileInput.gas) && drivetrain == "awd")
            {
                Debug.Log("HUH");

                frontTire.AddTorque(hp * gearRatio * -1 * Time.deltaTime);
                backTire.AddTorque(hp * gearRatio * -1 * Time.deltaTime);
            }








            //rwd

            if ((Input.GetKey("d") || MobileInput.gas) && drivetrain == "rwd")
            {
                backTire.AddTorque(Engine.hp * Engine.gearRatio * -1 * Time.deltaTime);
            }







            //fwd

            if ((Input.GetKey("d") || MobileInput.gas) && drivetrain == "fwd")
            {
                frontTire.AddTorque(hp * gearRatio * -1 * Time.deltaTime);
            }
        }   
        
        
        
        speed = velocity;
        GameObject Car = gameObject.transform.GetChild(1).gameObject;

        frontTire = gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
        backTire = gameObject.transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>();


        
        velocity = 3.6f * (rb.velocity.magnitude);

        //_cameraFollow.zoom(velocity);


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
        if (Input.GetKeyDown("v") || MobileInput._startEngine)
        {
            Debug.Log("engine started");
            engineOn = true;
        }
        if (engineOn)
        {
            
            if (rpm < idleRpm)
            {
                //rpm += 800 * Time.deltaTime;
            }

            if (gearSelected != 0)
            {
                rpm = Mathf.Lerp(rpm, Mathf.Abs((CarController.rotationalSpeed) * gearRatio * 2.2f + idleRpm), 0.05f);
            }
        }
        if (gearSelected != 0)
        {
            transEngaged = true;
        }
        else
        {
            transEngaged = false;
            if ((Input.GetKey("d") || MobileInput.gas) && (rpm < rpmLimit))
            {
                rpm += 7000 * Time.deltaTime;
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
