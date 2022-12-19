using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSide : MonoBehaviour
{
    public GameObject fadeScreen;
    private GameObject car;
    private Vector3 size;

    private void Start()
    {
        car = GameObject.FindGameObjectWithTag("Car");
    }

    private void Update()
    {
        size = car.transform.localScale;
    }

    public void SwitchSideClicked()
    {
        StartCoroutine("ChangeSide");
    }

    IEnumerator ChangeSide()
    {
        fadeScreen.GetComponent<Animation>().Play("FadeAnimation");
        yield return new WaitForSeconds(1);

        if (size.x > 0)
        {
            car.transform.localScale = new Vector3(car.transform.localScale.x * -1, car.transform.localScale.y, car.transform.localScale.z);
        }

        if (size.x < 0)
        {
            car.transform.localScale = new Vector3(car.transform.localScale.x * -1, car.transform.localScale.y, car.transform.localScale.z);
        }
    }
}
