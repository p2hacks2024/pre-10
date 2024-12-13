using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Text;

public class play : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            videoPlayer.Play();
        }  
    }
}