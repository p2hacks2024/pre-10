using UnityEngine;
using UnityEngine.SceneManagement;

public class readyNeedU20NeedU : MonoBehaviour
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
            SceneManager.LoadScene("NeedU20NeedU");//New Scene ‚ÍScene‚Ì–¼‘O‚É‘‚«Š·‚¦‚é
        }
    }
}
