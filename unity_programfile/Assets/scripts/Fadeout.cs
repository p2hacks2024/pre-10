using UnityEngine;
using System.Collections;
using System;
using UnityEditor.XR;
using Unity.VisualScripting;

public class Fadeout : MonoBehaviour
{
    AudioSource audioSource;
    public bool IsFade;
    public double FadeOutSeconds = 1.0;
    bool IsFadeOut = true;
    double FadeDeltaTime = 0;
    bool r = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            r = true;
        }
        if (r)
        {
            audioSource.volume -= Time.deltaTime;
        }

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    audioSource.volume = 0;
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    r = true;
        //}

        //if (r)
        //{
        //    if (IsFadeOut)
        //    {

        //        Debug.Log("press!");
        //        FadeDeltaTime += Time.deltaTime;

        //        if (FadeDeltaTime >= FadeOutSeconds)
        //        {
        //            FadeDeltaTime = FadeOutSeconds;
        //            IsFadeOut = false;

        //        }
        //        if (FadeDeltaTime>5)
        //        {
        //            audioSource.volume = (float)(1.0 - FadeOutSeconds / FadeDeltaTime);
        //            IsFadeOut = false;
        //        }
        //    }
        //}
        //Debug.Log("volume : " + audioSource.volume);
    }
}