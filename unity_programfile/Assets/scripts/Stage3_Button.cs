using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3_Button : MonoBehaviour
{
    public void Stage3_button()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        Invoke(nameof(Sele3), 0.5f);
    }
    public void Sele3()
    {
        SceneManager.LoadScene("Stage3(KittysPlayTime)");
    }
}
