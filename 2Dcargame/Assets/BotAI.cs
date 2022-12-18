using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAI : MonoBehaviour
{
    public Rigidbody2D frontTire;
    public Rigidbody2D backTire;
    public static float _hp;
    public Physics2D physics2D;

    public void Gas(float hp)
    {
        frontTire.AddTorque(hp * -1 * Time.deltaTime);
        backTire.AddTorque(hp * -1 * Time.deltaTime);
        _hp = hp;
    }

    public void Update()
    {
        Debug.Log(backTire.angularVelocity);
        
        
        if (transform.rotation.z == -5)
        {
            Gas(_hp - 100);
        }
        if (frontTire.angularVelocity <= -3000)
        {
            Gas(_hp - 100);
        }
        if (backTire.angularVelocity <= -3000)
        {
            Gas(_hp - 100);
        }
    }

}
