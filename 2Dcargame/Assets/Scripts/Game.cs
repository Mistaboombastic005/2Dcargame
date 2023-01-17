using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Light2D _light;
    public static float mainSound;
    private Slider soundSlider;
    void Start()
    {
        soundSlider = GetComponent<Slider>();
    }


    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            _light.intensity = (Mathf.Sin(((Time.realtimeSinceStartup) / 600) * Mathf.PI * 2) + 1) / 2.857f + 0.3f;
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainSound = soundSlider.value;
        }
        
    }
}
