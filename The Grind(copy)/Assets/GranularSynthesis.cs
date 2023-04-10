using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GranularSynthesis : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public float volume1;

    public GameObject slider1;
    public GameObject slider2;
    public GameObject slider3;
    public GameObject slider4;
    public GameObject slider5;
    public float position;
    public float size;
    public float randomness;
    public float enveloppe;
    public float offset;
    public float min;

    //[HideInInspector]
    public float timeSinceStart1;
    public float timeSinceStart2;
    public float fps;
    public float maxfps;
    public const float updatesPerSecond = 100000;


    void Start()
    {
        StartCoroutine(playSounds());
    }

    void Update()
    {
        //sliders
        position = slider1.GetComponent<Slider>().value;
        size = slider2.GetComponent<Slider>().value;
        randomness = slider3.GetComponent<Slider>().value;
        enveloppe = slider4.GetComponent<Slider>().value;
        offset = slider5.GetComponent<Slider>().value;


        //sliders max
        slider1.GetComponent<Slider>().maxValue = audioSource1.clip.length - size - randomness;
        slider1.GetComponent<Slider>().minValue = randomness;
        slider2.GetComponent<Slider>().minValue = 0.05f;
        slider2.GetComponent<Slider>().maxValue = 0.5f;
        slider3.GetComponent<Slider>().maxValue = 1;
        slider4.GetComponent<Slider>().minValue = 2;
        slider4.GetComponent<Slider>().maxValue = 10;
        slider5.GetComponent<Slider>().minValue = 0;
        slider5.GetComponent<Slider>().maxValue = (1 / enveloppe);
        //enveloppe
        float cnt = updatesPerSecond * Time.deltaTime;
        for (int i = 0; i < cnt; i++)
        {
            if (timeSinceStart1 < size / enveloppe)
            {
                audioSource1.volume = (timeSinceStart1 * enveloppe) / (size);
            }
            if (timeSinceStart1 > (size-(size / enveloppe)))
            {
                audioSource1.volume = (-timeSinceStart1 * enveloppe) / (size) + enveloppe;
            }

            if (timeSinceStart2 < size / enveloppe)
            {
                audioSource2.volume = (timeSinceStart2 * enveloppe) / (size);
            }
            if (timeSinceStart2 > (size - (size / enveloppe)))
            {
                audioSource2.volume = (-timeSinceStart2 * enveloppe) / (size) + enveloppe;
            }



            fps++;
        }

        timeSinceStart1 += 1 * Time.deltaTime;
        timeSinceStart2 += 1 * Time.deltaTime;



    }

    IEnumerator playSounds()
    {
        audioSource1.time = position + Random.Range(-randomness, randomness);
        StartCoroutine(playSound1());
        yield return new WaitForSeconds(size - (size/enveloppe) + offset );
        audioSource2.time = position + Random.Range(-randomness, randomness);
        StartCoroutine(playSound2());
        yield return new WaitForSeconds(size - (size /enveloppe) + offset);
        StartCoroutine(playSounds());

    }
    IEnumerator playSound1()
    {
        timeSinceStart1 = 0;
        maxfps = fps;
        fps = 0;
        audioSource1.Play();
        yield return new WaitForSeconds(size);
        audioSource1.Stop();
        yield return null;

    }
    IEnumerator playSound2()
    {
        timeSinceStart2 = 0;
        audioSource2.Play();
        yield return new WaitForSeconds(size);
        audioSource2.Stop();
        yield return null;
    }

}
