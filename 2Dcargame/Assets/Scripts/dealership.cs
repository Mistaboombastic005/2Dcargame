using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dealership : MonoBehaviour
{
    public CanvasManager canvasManager;

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name);
        canvasManager.OpenDealerShip(gameObject);
    }
}
