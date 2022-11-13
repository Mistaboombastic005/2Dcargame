using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    private const float maxSpeedAngle = -20f;
    private const float zeroSpeedAngle = 210f;
    public Transform needleTransform;
    public TextMeshProUGUI Km_HText;
    public TextMeshProUGUI gearText;
    public float Rpm;
    public int gear;
    private void Awake()
    {
        needleTransform = transform.Find("Needle");
    }

    private void Update()
    {
        Rpm = CarController.staticRPM;

        gear = CarController.gearSelected;

        gearText.text = gear.ToString();

        needleTransform.eulerAngles = new Vector3(0, 0, GetSpeedRotation(Rpm));

        Km_HText.text = ((int)(CarController.speed)).ToString();
    }

    public float GetSpeedRotation(float speed)
    {
        float totalAngleSize = zeroSpeedAngle - maxSpeedAngle;

        return zeroSpeedAngle - speed / 10000 * totalAngleSize;
    }
}
