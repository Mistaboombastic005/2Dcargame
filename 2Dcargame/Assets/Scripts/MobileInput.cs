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
    public static bool acceptRace;
    public GameObject raceButton;


    

    public void GasInput(bool gassing)
    {
        gas = gassing;
    }

    public void BrakeInput(bool braking)
    {
        brake = braking;
    }

    public void acceptRaceInput()
    {
        acceptRace = true;
    }

    public void startEngine()
    {
        if (!CarController.engineOn)
        {
            _startEngine = true;
            CarController.engineOn = true;
        }
        else
        {
            _startEngine = false;
            CarController.engineOn = false;
        }
    }

    private void Update()
    {
        if (CarController.engineOn)
        {
            _startEngine = false;
        }
    }




    public void UpGear()
    {
        CarController.gearSelected++;
    }

    public void DownGear()
    {
        CarController.gearSelected--;
    }
}
