using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastAI : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;
    public BotAI botAI;



    void Update()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 7);
        if (hit1 && hit1.collider.name == "CityMap")
        {
            Debug.Log("hit : " + hit1.collider.name);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 100, Color.blue);
            Debug.DrawRay(pos1.position, pos1.position - pos1.position, Color.blue);
            botAI.Gas(40);
        }
        else
        {
            RaycastHit2D hit2 = Physics2D.Raycast(pos1.position, pos1.TransformDirection(Vector2.right), 7);
            Debug.DrawRay(pos1.position, pos1.TransformDirection(Vector2.right) * 100, Color.blue);
            Debug.DrawRay(transform.position, transform.position - transform.position, Color.red);
            botAI.Gas(60);
        }
        RaycastHit2D hit3 = Physics2D.Raycast(pos2.position, pos2.TransformDirection(Vector2.right), 7);
        if (hit3 && hit3.collider.name == "CityMap")
        {
            Debug.DrawRay(pos2.position, pos2.TransformDirection(Vector2.right) * 100, Color.green);
        }
        else
        {
            Debug.DrawRay(pos2.position, pos2.TransformDirection(Vector2.right) * 100, Color.red);
            botAI.Gas(-10);
        }
    }
}
