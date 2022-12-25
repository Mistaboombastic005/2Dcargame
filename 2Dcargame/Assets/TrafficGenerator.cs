using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficGenerator : MonoBehaviour
{
    public GameObject Player;
    public int maxCarNumber;
    private int currentCarNumber;
    private float spaceBefore;
    private float spaceAfter;
    private int pointTowardsBool;
    private int pointTowards;
    private int frontOrBackBool;


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


    private void Update()
    {
        if (currentCarNumber <= maxCarNumber)
        {
            StartCoroutine(SpawnCar());
        }

        spaceBefore = Player.transform.position.x - 1000;
        spaceAfter = Player.transform.position.x + 1000;
    }

    IEnumerator SpawnCar ()
    {
        frontOrBackBool = 2;
        pointTowardsBool = 2;
        pointTowards = 0;
        pointTowardsBool = Random.Range(0, 1);
        frontOrBackBool = Random.Range(0, 1);
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
            GameObject carInstance = Instantiate(_body[Random.Range(0, _body.Length - 1)].bodyGO, new Vector3(Random.Range(Player.transform.position.x + 50, spaceAfter), 0, 0), Quaternion.Euler(0, pointTowards, 0));
            currentCarNumber++;
        }
        else
        {
            GameObject carInstance = Instantiate(_body[Random.Range(0, _body.Length - 1)].bodyGO, new Vector3(Random.Range(Player.transform.position.x - 50, spaceBefore), 0, 0), Quaternion.Euler(0, pointTowards, 0));
            currentCarNumber++;
        }

        yield return new WaitForSeconds(1);
    }
}
