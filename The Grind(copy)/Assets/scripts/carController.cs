using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    public float rpm;
    public Rigidbody2D Fwheel;
    public Rigidbody2D Rwheel;
    public AudioSource AudioSource1;
    public AudioSource AudioSource2;
    public AudioClip Idle;
    public AudioClip High;

    void Start()
    {
        
    }


    void Update()
    {
        if (rpm < 2000)
        {
            if (Input.GetKey("d"))
            {
                Fwheel.AddTorque(-1);
            }
            if (Input.GetKey("a"))
            {
                Fwheel.AddTorque(1);
            }
        }

        rpm = Mathf.Abs(Fwheel.angularVelocity * 2)+800;


        AudioSource1.loop = true;
        AudioSource1.mute = false;
        AudioSource1.pitch = (rpm +1200) / 2000;
        AudioSource2.loop = true;
        AudioSource2.mute = false;
        AudioSource2.pitch = (rpm + 3200) / 4000;
    }
}
