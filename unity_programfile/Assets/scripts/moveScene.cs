using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
public class moveScene : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("sele(title - 1)");//New Scene ÇÕSceneÇÃñºëOÇ…èëÇ´ä∑Ç¶ÇÈ
        }
    }
}