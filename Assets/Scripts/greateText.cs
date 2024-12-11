using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine;
using UnityEngine.UI;

public class greateText : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

        Text t = GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>();

        int score = 100;

        t.text = "testÅF" + score.ToString();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
