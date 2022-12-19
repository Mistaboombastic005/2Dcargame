using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State { Parked, Cruise, Racing}
public enum DriveTrain { AWD, RWD, FWD}
public class BotAI : MonoBehaviour
{
    public State state;
    public DriveTrain driveTrain;
    
    public Rigidbody2D frontTire;
    public Rigidbody2D backTire;
    public static float _hp;
    public Physics2D physics2D;
    public int gearSelected;
    public float rpm;
    public static float staticRpm;
    public float gearRatio;
    public int idleRpm;
    public float rotationalSpeed;
    public GameObject player;
    public float throttle;
    public AnimationCurve torqueCurve;
    public float stagingTime;
    public float stagingTimeRemaining;

    [System.Serializable]
    public class gears
    {
        public int GearNumber;
        public float gearRatio;
    }
    public gears[] _gears;
    public float breakForce;
    public bool brake;

    private void Start()
    {
        state = State.Cruise;
        gearSelected = 1;
    }


    public void Update()
    {
        staticRpm = rpm;
        
        if (state == State.Cruise && transform.position.x - player.transform.position.x <= 10f && transform.position.y - player.transform.position.y <= 10f)
        {
            Debug.Log("Wanna race? (Press <SPACE> to race)");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stagingTimeRemaining = stagingTime - Time.deltaTime;
                CarController.stagingRace = true;
            }
        }
        if (stagingTimeRemaining == 0)
        {
            state = State.Racing;
            CarController.startRace = true;
            CarController.racer = gameObject;
            CarController.stagingRace = false;
        }

        if (state == State.Cruise)
        {
            if (driveTrain == DriveTrain.AWD)
            {
                frontTire.AddTorque(_hp * (throttle / 1.5f) * gearRatio * -1 * Time.deltaTime);
                backTire.AddTorque(_hp * (throttle / 1.5f) * gearRatio * -1 * Time.deltaTime);
            }

            if (driveTrain == DriveTrain.FWD)
            {
                frontTire.AddTorque(_hp * (throttle / 1.5f) * gearRatio * -1 * Time.deltaTime);
            }

            if (driveTrain == DriveTrain.RWD)
            {
                backTire.AddTorque(_hp * (throttle / 1.5f) * gearRatio * -1 * Time.deltaTime);
            }

            if (rpm >= 4000 && gearSelected < _gears.Length - 2)
            {
                gearSelected++;
            }
            if (rpm <= 2000 && gearSelected > 1)
            {
                gearSelected--;
            }
        }

        if (state == State.Racing)
        {
            if (driveTrain == DriveTrain.AWD)
            {
                frontTire.AddTorque(_hp * throttle * gearRatio * -1 * Time.deltaTime);
                backTire.AddTorque(_hp * throttle * gearRatio * -1 * Time.deltaTime);
            }

            if (driveTrain == DriveTrain.FWD)
            {
                frontTire.AddTorque(_hp * throttle * gearRatio * -1 * Time.deltaTime);
            }

            if (driveTrain == DriveTrain.RWD)
            {
                backTire.AddTorque(_hp * throttle * gearRatio * -1 * Time.deltaTime);
            }

            if (rpm >= 7000 && gearSelected < _gears.Length - 2)
            {
                gearSelected++;
            }
            if (rpm <= 3000 && gearSelected > 1)
            {
                gearSelected--;
            }
        }

        _hp = torqueCurve.Evaluate(rpm);

        rotationalSpeed = -1 * frontTire.angularVelocity;
        
        gearRatio = (_gears[gearSelected + 1].gearRatio) / 3.4f;


        if (gearSelected != 0)
        {
            rpm = Mathf.Lerp(rpm, Mathf.Abs(rotationalSpeed * gearRatio) * 2.2f + idleRpm, 0.05f);
        }



        if (brake)
        {
            if (rotationalSpeed > 0)
            {
                if (rotationalSpeed < breakForce)
                {
                    frontTire.angularVelocity -= frontTire.angularVelocity;
                }
                else
                {
                    frontTire.angularVelocity += breakForce;
                }
            }
            if (rotationalSpeed < 0)
            {
                if (rotationalSpeed > breakForce)
                {
                    frontTire.angularVelocity += frontTire.angularVelocity;
                }
                else
                {
                    frontTire.angularVelocity -= breakForce;
                }

            }
        }
    }

}
