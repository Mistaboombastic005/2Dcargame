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
    public Rigidbody2D car;
    public float _hp;
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
    public GameObject Arrow;
    public GameObject Player;
    public float speedLimit;

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
        Arrow.SetActive(false);
        car = gameObject.GetComponent<Rigidbody2D>();
        CarController.racer = null;
    }


    public void Update()
    {
        if (state == State.Cruise)
        {
            if (car.velocity.magnitude > speedLimit / 3.6f)
            {
                throttle = throttle * 0;
            }
            if (car.velocity.magnitude > (speedLimit + 10) / 3.6f)
            {
                brake = true;
            }


            if (transform.position.x - player.transform.position.x <= 10f && transform.position.x - player.transform.position.x >= -10f)
            {
                Arrow.SetActive(true);


                if (Input.GetKeyDown(KeyCode.Space) && state == State.Cruise || MobileInput.acceptRace && state == State.Cruise)
                {
                    CarController.racer = gameObject;
                    CarController.stagingRace = true;
                    Debug.Log("Race Accepted");
                    MobileInput.acceptRace = false;
                }

            }
            else
            {
                Arrow.SetActive(false);

                if (CarController.stagingRace && CarController.racer == gameObject)
                {
                    if (Player.transform.position.x - CarController.racer.transform.position.x <= 1)
                    {
                        brake = true;
                        throttle *= 0;
                    }

                    if (Player.transform.position.x - CarController.racer.transform.position.x >= -1)
                    {
                        throttle *= 1;
                        brake = false;
                    }
                }
            }  
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
