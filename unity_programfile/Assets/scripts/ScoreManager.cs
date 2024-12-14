using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private Text scoreText;
   
    void Start()
    {
        Debug.Log("test");
        scoreText = GetComponentInChildren<Text>();
        scoreText.text = "111";
    }

    void Update()
    {
        
    }
}
