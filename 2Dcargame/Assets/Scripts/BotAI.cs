using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State { Parked, Cruise, Racing}
public enum BotDriveTrain { AWD, RWD, FWD}
public class BotAI : MonoBehaviour
{
    public State state;
    public BotDriveTrain driveTrain;
    
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
    public float speedLimit;
    public int directionY;
    public float KM_H;

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
        player = GameObject.FindGameObjectWithTag("Player");
        if (gameObject.transform.rotation.y == 0)
        {
            directionY = 1;
        }
        if (gameObject.transform.rotation.y == 1)
        {
            directionY = -1;
        }
    }


    public void Update()
    {
        KM_H = car.velocity.magnitude * 3.6f;
        
        
        if (gameObject.transform.position.x > player.transform.position.x + TrafficGenerator.maxDistanceStatic)
        {
            Destroy(gameObject);
            TrafficGenerator.currentCarNumber--;
        }
        if (gameObject.transform.position.x < player.transform.position.x - TrafficGenerator.maxDistanceStatic)
        {
            Destroy(gameObject);
            TrafficGenerator.currentCarNumber--;
        }
        
        
        throttle *= directionY;
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
                    if (player.transform.position.x - CarController.racer.transform.position.x <= 1)
                    {
                        brake = true;
                        throttle *= 0;
                    }

                    if (player.transform.position.x - CarController.racer.transform.position.x >= -1)
                    {
                        throttle *= 1;
                        brake = false;
                    }
                }
            }  
        }


        if (state == State.Cruise)
        {
            if (driveTrain == BotDriveTrain.AWD)
            {
                frontTire.AddTorque(_hp * (throttle / 1.5f) * gearRatio * -1 * Time.deltaTime);
                backTire.AddTorque(_hp * (throttle / 1.5f) * gearRatio * -1 * Time.deltaTime);
            }

            if (driveTrain == BotDriveTrain.FWD)
            {
                frontTire.AddTorque(_hp * (throttle / 1.5f) * gearRatio * -1 * Time.deltaTime);
            }

            if (driveTrain == BotDriveTrain.RWD)
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
            if (driveTrain == BotDriveTrain.AWD)
            {
                frontTire.AddTorque(_hp * throttle * gearRatio * -1 * Time.deltaTime);
                backTire.AddTorque(_hp * throttle * gearRatio * -1 * Time.deltaTime);
            }

            if (driveTrain == BotDriveTrain.FWD)
            {
                frontTire.AddTorque(_hp * throttle * gearRatio * -1 * Time.deltaTime);
            }

            if (driveTrain == BotDriveTrain.RWD)
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            transform.position += Vector3.up * Time.deltaTime * 1000;
        }
    }
}
