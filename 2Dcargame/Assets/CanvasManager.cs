using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private GameObject car;
    public GameObject lambaniButton;
    public GameObject lambaniCanvas;
    private GameObject currentCanvas;




    void Start()
    {
        
    }

    

    void Update()
    {
        car = GameObject.FindGameObjectWithTag("Car");
    }


    public void OnMouseDown()
    {
        
        if (car.GetComponent<Rigidbody2D>().velocity.magnitude >= 10 / 3.6)
        {
            OpenDealerShip(lambaniCanvas);
            currentCanvas = lambaniCanvas;
            lambaniButton.SetActive(false);
        }
    }

    public void OpenDealerShip(GameObject dealership)
    {
        dealership.SetActive(true);
    }
    public void CloseDealerShip()
    {
        currentCanvas.SetActive(false);
    }

<<<<<<< HEAD
    
=======
    private void OnMouseEnter()
    {
        
    }
>>>>>>> parent of d92848f (Canvas manager/dealership)
}
