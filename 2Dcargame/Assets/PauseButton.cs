using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public static bool paused = false;

    private void Update()
    {
        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void clicked()
    {
        if (!paused)
        {
            paused = true;
        }
        else
        {
            paused = false;
        }
    }
}
