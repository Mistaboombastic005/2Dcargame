using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViaducEntrance : MonoBehaviour
{
    public GameObject body;
    public bool active;

    private void Start()
    {
        active = false;
    }

    private void Update()
    {
        body.SetActive(active);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                active = true;  
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                active = false;
            }
        }
    }
}
