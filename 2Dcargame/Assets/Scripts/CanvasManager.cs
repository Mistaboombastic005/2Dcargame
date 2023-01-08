using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private GameObject car;
    public GameObject lambaniCanvas;
    private GameObject currentCanvas;
    public GameObject currentTitleSection;
    public GameObject currentBuySection;




    void Start()
    {
        
    }

    

    void Update()
    {
        car = GameObject.FindGameObjectWithTag("Car");
    }


    public void OpenDealerShip(GameObject dealership)
    {
        
        if (car.GetComponent<Rigidbody2D>().velocity.magnitude <= 10 / 3.6)
        {
            
            if (dealership.name == "LambaniDealership")
            {
                currentCanvas = lambaniCanvas;
                currentCanvas.SetActive(true);
                dealership.SetActive(false);
                currentTitleSection = currentCanvas.transform.Find("TitleSection").gameObject;
                currentTitleSection.SetActive(true);
                currentBuySection = currentCanvas.transform.Find("BuySection").gameObject;
                currentBuySection.SetActive(false);
            }
        }
    }
    public void CloseDealerShip(GameObject dealership)
    {
        currentCanvas.SetActive(false);
        Debug.Log(dealership.name);
        dealership.SetActive(true);
    }

    public void OpenBuySection(GameObject buySection)
    {
        currentBuySection = buySection;
        currentTitleSection.SetActive(false);
        currentBuySection.SetActive(true);
    }
}
