using UnityEngine;

public class CameraSound : MonoBehaviour
{
   public void cameraSoundPlay()
    {
        GetComponent<AudioSource>().Play();
    }
}
