using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Button : MonoBehaviour
{
    public void start_button()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        Invoke(nameof(movie), 0.5f);
    }
    public void movie()
    {
        SceneManager.LoadScene("OP_movie");
    }
}
