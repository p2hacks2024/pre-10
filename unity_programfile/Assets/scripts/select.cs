using UnityEngine;
using UnityEngine.SceneManagement;

public class select : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("readyKittysPlayTime");//New Scene ‚ÍScene‚Ì–¼‘O‚É‘‚«Š·‚¦‚é
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("readyNeedU20NeedU");//New Scene ‚ÍScene‚Ì–¼‘O‚É‘‚«Š·‚¦‚é
        }
    }
}
