using UnityEngine;

public class Option_ButtonManager : MonoBehaviour
{

    [SerializeField]
    private GameObject objectToToggle;

    public void ToggleObject()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        if (objectToToggle != null)
        {
            objectToToggle.SetActive(!objectToToggle.activeSelf);
        }
    }
}
