using UnityEngine;
using UnityEngine.SceneManagement;

public class readyKittysPlayTime : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("KittysPlayTime");//New Scene はSceneの名前に書き換える
        }
    }
}
