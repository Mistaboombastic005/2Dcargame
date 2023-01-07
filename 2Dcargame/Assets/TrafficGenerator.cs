using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficGenerator : MonoBehaviour
{
    public GameObject Player;
    public int maxCarNumber;
    public static int currentCarNumber;
    public float spaceBefore;
    public float spaceAfter;
    public static int pointTowardsBool;
    public int pointTowards;
    public int frontOrBackBool;
    public static int maxDistanceStatic;
    public int maxDistance;


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


    private void Start()
    {
        currentCarNumber = 0;
    }
    private void Update()
    {
        maxDistanceStatic = maxDistance;
        if (currentCarNumber < maxCarNumber)
        {
            SpawnCar();
            frontOrBackBool = 2;
            pointTowardsBool = 2;
            pointTowards = 0;
        }

        spaceBefore = Player.transform.position.x - 1000;
        spaceAfter = Player.transform.position.x + 1000;
    }

    public void SpawnCar()
    {
        pointTowardsBool = Random.Range(0, 2);
        frontOrBackBool = Random.Range(0, 2);
        if (pointTowardsBool == 1)
        {
            pointTowards = 180;
        }
        else
        {
            if (pointTowardsBool == 0)
            {
                pointTowards = 0;
            }
        }
        if (frontOrBackBool == 0)
        {
            GameObject carInstance = Instantiate(_body[Random.Range(0, _body.Length)].bodyGO, new Vector3(Random.Range(Player.transform.position.x + 100, spaceAfter), 0, 0), Quaternion.Euler(0, pointTowards, 0));
            currentCarNumber++;
        }
        else
        {
            GameObject carInstance = Instantiate(_body[Random.Range(0, _body.Length)].bodyGO, new Vector3(Random.Range(Player.transform.position.x - 100, spaceBefore), 0, 0), Quaternion.Euler(0, pointTowards, 0));
            currentCarNumber++;
        }
    }
}
