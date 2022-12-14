using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CarCustomization : MonoBehaviour
{
    [System.Serializable]
    public class body
    {
        public string name;
        public GameObject bodyGO;
    }
    public body[] _body;
    [System.Serializable]
    public class wheels
    {
        public string name;
        public GameObject wheelGO;
    }
    public wheels[] _wheel;
    
    public GameObject currentCarBody;
    public GameObject CurrentFWheel;
    public GameObject CurrentRWheel;
    private GameObject InstanceFWheel;
    private GameObject InstanceRWheel;
    public int wheelNumber;
    public int carNumber;



    // Start is called before the first frame update
    void Awake()
    {
        currentCarBody = _body[carNumber].bodyGO;
        currentCarBody = (GameObject)Instantiate(currentCarBody, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);



        CurrentFWheel = _wheel[wheelNumber].wheelGO;
        CurrentRWheel = _wheel[wheelNumber].wheelGO;
        InstanceRWheel = (GameObject)Instantiate(CurrentRWheel, currentCarBody.transform.GetChild(1).position, Quaternion.identity);
        InstanceRWheel.transform.parent = currentCarBody.transform.GetChild(1);
        InstanceFWheel = (GameObject)Instantiate(CurrentFWheel, currentCarBody.transform.GetChild(0).position, Quaternion.identity);
        InstanceFWheel.transform.parent = currentCarBody.transform.GetChild(0);
    }

    private void Start()
    {
        ChangeWheel();
    }


    void Update()
    {
        transform.position = currentCarBody.transform.position;        
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            wheelNumber++;
            ChangeWheel();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            wheelNumber--;
            ChangeWheel();
        }

        wheelNumber = Mathf.Clamp(wheelNumber, 0, 3);
        CurrentFWheel = _wheel[wheelNumber].wheelGO;
        CurrentRWheel = _wheel[wheelNumber].wheelGO;
    }

    public void ChangeWheel()
    {
        Destroy(InstanceRWheel);
        Destroy(InstanceFWheel);
        InstanceRWheel = (GameObject)Instantiate(CurrentRWheel, currentCarBody.transform.GetChild(1).position, Quaternion.identity);
        InstanceFWheel = (GameObject)Instantiate(CurrentFWheel, currentCarBody.transform.GetChild(0).position, Quaternion.identity);
        InstanceRWheel.transform.parent = currentCarBody.transform.GetChild(1);
        InstanceFWheel.transform.parent = currentCarBody.transform.GetChild(0);
    }
}
