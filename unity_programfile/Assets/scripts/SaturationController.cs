using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class SaturationController : MonoBehaviour
{
    public PostProcessVolume postProcessVolume; // Post Process Volume�����蓖�Ă�
    private ColorGrading colorGrading;          // Color Grading�G�t�F�N�g�ւ̎Q��

    void Start()
    {
        // Volume����Color Grading�G�t�F�N�g���擾
        if (postProcessVolume.profile.TryGetSettings(out colorGrading))
        {
            Debug.Log("Color Grading found!");
        }
        else
        {
            Debug.LogError("Color Grading not found in Post Process Volume.");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            SetSaturation(0);
            Debug.Log(colorGrading.saturation.value);
        }
    }

    public void SetSaturation(float saturationValue)
    {

        if (colorGrading != null)
        {
            colorGrading.saturation.value = saturationValue; // �ʓx��ύX
        }
    }
}
