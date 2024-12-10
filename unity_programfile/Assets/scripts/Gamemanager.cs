using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("ingame");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("ƒeƒXƒg");
    }

    void Update()
    {
  
    }
}
