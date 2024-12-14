using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage4_Button : MonoBehaviour
{
    public void Stage4_button()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        Invoke(nameof(Sele4), 0.5f);
    }
    public void Sele4()
    {
        SceneManager.LoadScene("Stage4(KittysPlayTime)");
    }
}
