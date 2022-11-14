using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform car;
    public Vector3 offset;
    public Vector3 translate;
    public Camera mainCamera;
    public float desiredDuration = 0.5f;
    private float elapsedTime;
    private float percentageComplete;
    public float camFactor;
    public AnimationCurve zoomCurve;
    public Vector3 vibration;
    public static float timeSinceStart;
    public AnimationCurve pan;

    private void Start()
    {
        camFactor = 3;
    }
    void Update()
    {
        timeSinceStart = Time.realtimeSinceStartup;

         elapsedTime += Time.deltaTime;
        percentageComplete = elapsedTime / desiredDuration;
    }

    public void zoom(float velocity)
    {
        float KM_H = velocity * 3.6f;

        transform.position = car.position + offset + vibration + translate * camFactor;

        vibration.y = (Mathf.Sin((timeSinceStart) * 50) * ((KM_H + 0.01f)/20000));

        vibration.x = (Mathf.Sin((timeSinceStart) * 50) * ((KM_H + 0.01f)/20000));

        vibration.x = (Mathf.Sin(timeSinceStart) / pan.Evaluate(KM_H));
       

        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, zoomCurve.Evaluate(KM_H), 0.020f);

        offset.x = Mathf.Lerp(offset.x,zoomCurve.Evaluate(KM_H) * 1.5f -3.5f, 0.0020f);

        offset.y = Mathf.Lerp(offset.y, zoomCurve.Evaluate(KM_H)/2.5f - 1, 0.0020f);


    }
}
