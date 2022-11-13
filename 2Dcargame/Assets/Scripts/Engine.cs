using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
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
    public AnimationCurve curve;


    public Rigidbody2D rb;
    public static float KM_H;
    public CameraFollow _cameraFollow;

    public AudioSource audioSource;
    public AudioClip starterSound;
    public AudioClip idleSound;





    private void Update()
    {
        gearRatio = (_gears[gearSelected + 1].gearRatio)/5;

        hp = curve.Evaluate(rpm);

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
        Debug.Log(hp);
        if (Input.GetKeyDown("v"))// && (engineStarting = false))
        {
            if (engineOn == true)
            {
                engineOn = false;
            }
            else
            {
                engineStarting = true;
            }

        }
        if (engineStarting)
        {
            StartCoroutine(PlaySound());
        }
        if (engineOn)
        {
            if(rpm < idleRpm)
            {
                rpm += 800 * Time.deltaTime;
            }

            if (gearSelected != 0)
            {
                rpm = Mathf.Abs((CarController.rotationalSpeed) * gearRatio * 3 + idleRpm);
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
                rpm += 3000 * Time.deltaTime;
            }
            else
            {
                rpm -= ((rpm - idleRpm) / 1.5f) * Time.deltaTime;
            }
        }


        IEnumerator PlaySound()
        {
            engineStarting = false;
            audioSource.clip = starterSound;
            audioSource.Play();
            audioSource.loop = false;
            yield return new WaitForSeconds(starterSound.length);
            engineOn = true;
            audioSource.clip = idleSound;
            audioSource.Play();
            audioSource.loop = true;
        }
    }
    private void FixedUpdate()
    {
        
    }



}
