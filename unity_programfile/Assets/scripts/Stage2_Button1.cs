using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2_Button : MonoBehaviour
{
    public void stage2_button()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        Invoke(nameof(sele2), 0.5f);
    }
    public void sele2()
    {
        SceneManager.LoadScene("Stage2(NeedU20NeedU)");
    }
}
