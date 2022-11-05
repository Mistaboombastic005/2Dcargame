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

    private void Start()
    {
        camFactor = 3;
    }
    void Update()
    {
       

        elapsedTime += Time.deltaTime;
        percentageComplete = elapsedTime / desiredDuration;
    }

    public void zoom(float velocity)
    {
        float KM_H = velocity * 3.6f;

        transform.position = car.position + offset + translate * camFactor;

        mainCamera.orthographicSize = camFactor;


        if (KM_H <= 50) 
        {
            camFactor = (KM_H * KM_H) / 1000 + 3;
            
        }
        else
        {
            camFactor = (-250 / KM_H) + 10.5f;

            
        }
        
    }
}
