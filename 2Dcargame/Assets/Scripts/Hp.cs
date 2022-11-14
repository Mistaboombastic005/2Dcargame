using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hp : MonoBehaviour
{
    public TextMeshProUGUI horsepower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horsepower.text = Engine.hp.ToString();
    }
}
