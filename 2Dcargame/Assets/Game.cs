using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Game : MonoBehaviour
{
    public Light2D _light;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _light.intensity = (Mathf.Sin   (    ((Time.realtimeSinceStartup)/600)*Mathf.PI*2    )+1)/2.857f+0.3f;
    }
}
