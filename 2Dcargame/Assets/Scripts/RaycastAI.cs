using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastAI : MonoBehaviour
{
    public Transform pos1;
    public GameObject botAI;
    public float anglefront;
    public AnimationCurve angleCurve;
    private Rigidbody2D botRigidBody;
    public GameObject Player;


    [Header("Throttle AI")]
    [Range(0f, 2f)]
    [SerializeField]
    private float throttleClimbing;

    [Space]
    [Range(0f, 2f)]
    [SerializeField]
    private float throttleStraight;
    

    private void Start()
    {
        botRigidBody = botAI.GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        anglefront = angleCurve.Evaluate(botRigidBody.velocity.magnitude);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, anglefront);
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 7);
        if (!CarController.stagingRace && !CarController.racer == gameObject.transform.parent)
        {
            if (hit1 && hit1.collider.gameObject.layer == 7)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 100, Color.blue);
                Debug.DrawRay(pos1.position, pos1.position - pos1.position, Color.blue);
                botAI.GetComponent<BotAI>().throttle = throttleClimbing;
            }
            else
            {
                RaycastHit2D hit2 = Physics2D.Raycast(pos1.position, pos1.TransformDirection(Vector2.right), 7);
                Debug.DrawRay(pos1.position, pos1.TransformDirection(Vector2.right) * 100, Color.blue);
                Debug.DrawRay(transform.position, transform.position - transform.position, Color.red);
                botAI.GetComponent<BotAI>().throttle = throttleStraight;
            }
            RaycastHit2D hit3 = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 7);
            if (hit3 && hit3.collider.gameObject.layer == 7)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 100, Color.green);
                botAI.GetComponent<BotAI>().brake = false;
                botAI.GetComponent<BotAI>().throttle = throttleClimbing;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 100, Color.red);
                botAI.GetComponent<BotAI>().brake = true;
            }
        }



        if (CarController.stagingRace)
        {
            

            
        }
    }
}
