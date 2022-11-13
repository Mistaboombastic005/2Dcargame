using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    private GameObject car;
    public GameObject player;
    public static bool gas;
    public static bool brake;
    public static bool _startEngine;
    private bool startClicked;


    public void Update()
    {
        Debug.Log("_startEngine: " + _startEngine);

        
            
    }

    public void GasInput(bool gassing)
    {
        gas = gassing;
    }

    public void BrakeInput(bool braking)
    {
        brake = braking;
    }

    public void startEngine()
    {
        _startEngine = true;
        StartCoroutine("startEngineI");
    }

    public IEnumerator startEngineI()
    {
        if (_startEngine)
        {
            yield return new WaitForSeconds(0.0001f);
            _startEngine = false;
        }
    }
}
