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
    public LayerMask layerMask;
    private float currentAngle;


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




        if (CarController.racer != gameObject.transform.parent)
        {
            if (gameObject.transform.parent.transform.rotation.z >= 10)
            {
                currentAngle = throttleClimbing;
            }
            else
            {
                currentAngle = throttleStraight;
            }
            
            RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), layerMask);
            if (hit1)
            {
                if (hit1.collider.gameObject.layer == 7)
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 100, Color.green);
                    botAI.GetComponent<BotAI>().brake = false;
                    botAI.GetComponent<BotAI>().throttle = currentAngle;
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 100, Color.yellow);
                    botAI.GetComponent<BotAI>().brake = false;
                    botAI.GetComponent<BotAI>().throttle = currentAngle;
                }
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
