using UnityEngine;
using UnityEngine.SceneManagement;

public class clear4 : MonoBehaviour
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
            SceneManager.LoadScene("End");//New Scene ‚ÍScene‚Ì–¼‘O‚É‘‚«Š·‚¦‚é
        }
    }
}