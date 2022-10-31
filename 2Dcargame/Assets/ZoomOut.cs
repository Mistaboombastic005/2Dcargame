using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    public Camera _cameraMain;
    public Camera newCamera;
    public float maxZoom;
    public float zoomSpeed;
    public Vector2 maxPos;
    
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (newCamera.orthographicSize < maxZoom)
        {
            newCamera.orthographicSize += zoomSpeed * zoomSpeed * zoomSpeed * Time.deltaTime;
        }
        else
        {
            //newCamera.orthographicSize +=
        }

        _cameraMain.enabled = false;
        newCamera.enabled = true;
    }
}
