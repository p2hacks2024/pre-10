using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1_Button : MonoBehaviour
{
    public void stage1_button()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        Invoke(nameof(sele1), 0.5f);
    }
    public void sele1()
    {
        SceneManager.LoadScene("Stage1(KittysPlayTime)");
    }
}
