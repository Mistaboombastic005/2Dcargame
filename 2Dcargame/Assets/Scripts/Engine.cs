using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public float rpm;
    public float rpmLimit;
    public float idleRpm;
    public int nbGears;
    public int gearSelected;
    public int highestGear;
    public bool engineOn;
    public bool engineStarting;
    public float hp;


    public Rigidbody2D rb;
    public static float KM_H;
    public CameraFollow _cameraFollow;

    public AudioSource audioSource;
    public AudioClip starterSound;
    public AudioClip idleSound;




    private void Start()
    {
        engineOn = false;
        gearSelected = 0;
        engineStarting = false;
    }

    private void FixedUpdate()
    {

        if(Input.GetKeyDown("v"))// && (engineStarting = false))
        {
            if(engineOn == true)
            {
                engineOn = false;
            }
            else
            {
                engineStarting = true;
            }
            audioSource.clip = starterSound;
            audioSource.Play();
            StartCoroutine(PlaySound());
        }

        if (Input.GetKey("x"))
        {
            if(gearSelected < highestGear)
            {
                gearSelected += 1;
            }
        }
        if(Input.GetKey("z"))
        {
            if(gearSelected > -1)
            {
                if (rpm < rpmLimit - 1000)
                {
                    gearSelected -= 1;
                }
            }
        }
        IEnumerator PlaySound()
        {
            audioSource.clip = starterSound;
            audioSource.Play();
            yield return new WaitForSeconds(starterSound.length);
            engineStarting = false;
            audioSource.clip = idleSound;
            audioSource.Play();
            audioSource.loop = true;
        }
    }



}
