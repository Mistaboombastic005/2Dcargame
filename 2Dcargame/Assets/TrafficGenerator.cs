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


    public class body
    {
        public string name;
        public GameObject bodyGO;
    }
    public body[] _body;

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
            GameObject carInstance = Instantiate(_body[Random.Range(0, _body.Length)].bodyGO, new Vector3(Random.Range(Player.transform.position.x - 10, spaceBefore), 0, 0), Quaternion.identity);
        }

        spaceBefore = Player.transform.position.x - 100;
    }
}
