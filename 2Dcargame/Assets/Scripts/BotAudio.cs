using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAudio : MonoBehaviour
{
    public AudioSource _audioSource1;
    public AudioSource _audioSource2;
    public AudioSource _audioSource3;
    public AudioSource _audioSource4;
    public AudioClip start;
    public AudioClip idle;
    public AudioClip low;
    public AudioClip medium;
    public AudioClip high;
    public AnimationCurve volumeIdle;
    public AnimationCurve volumeLow;
    public AnimationCurve volumeMed;
    public AnimationCurve volumeHigh;
    public AnimationCurve pitchLow;
    public AnimationCurve pitchMed;
    public AnimationCurve pitchHigh;
    public static bool startEngine;

    private void Start()
    {
        StartCoroutine("playSound1");
        startEngine = true;
    }

    private void Update()
    {
        _audioSource1.pitch = Mathf.Clamp(_audioSource1.pitch, 0.9f, 4);
        _audioSource2.pitch = Mathf.Clamp(_audioSource2.pitch, 0.9f, 4);
        _audioSource3.pitch = Mathf.Clamp(_audioSource3.pitch, 0.9f, 4);
        _audioSource4.pitch = Mathf.Clamp(_audioSource4.pitch, 0.9f, 4);


        _audioSource1.volume = volumeIdle.Evaluate(gameObject.GetComponent<BotAI>().rpm) * Game.mainSound;
        _audioSource2.volume = volumeLow.Evaluate(BotAI.staticRpm) * Game.mainSound;
        _audioSource3.volume = volumeMed.Evaluate(BotAI.staticRpm) * Game.mainSound;
        _audioSource4.volume = volumeHigh.Evaluate(BotAI.staticRpm) * Game.mainSound;

        _audioSource1.pitch = pitchLow.Evaluate(BotAI.staticRpm);
        _audioSource2.pitch = pitchLow.Evaluate(BotAI.staticRpm);
        _audioSource3.pitch = pitchMed.Evaluate(BotAI.staticRpm);
        _audioSource4.pitch = pitchHigh.Evaluate(BotAI.staticRpm);
    }

    IEnumerator playSound1()
    {
        startEngine = true;
        _audioSource1.loop = false;
        _audioSource1.clip = start;
        _audioSource1.Play();
        yield return new WaitForSeconds(start.length);
        _audioSource2.mute = false;
        _audioSource3.mute = false;
        _audioSource4.mute = false;
        StartCoroutine("playSound2");
        StartCoroutine("playSound3");
        StartCoroutine("playSound4");
        _audioSource1.clip = idle;
        _audioSource1.loop = true;
        _audioSource1.Play();
    }

    public IEnumerator playSound2()
    {
        _audioSource2.loop = true;
        _audioSource2.clip = low;
        _audioSource2.Play();
        yield return new WaitForSeconds(start.length);
    }

    IEnumerator playSound3()
    {
        _audioSource3.loop = true;
        _audioSource3.clip = medium;
        _audioSource3.Play();
        yield return new WaitForSeconds(start.length);
    }

    IEnumerator playSound4()
    {
        _audioSource4.loop = true;
        _audioSource4.clip = high;
        _audioSource4.Play();
        yield return new WaitForSeconds(start.length);
    }
}
