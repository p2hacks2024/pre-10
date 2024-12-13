using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class SaturationController : MonoBehaviour
{
    public PostProcessVolume postProcessVolume; // Post Process Volumeを割り当てる
    private ColorGrading colorGrading;          // Color Gradingエフェクトへの参照

    void Start()
    {
        // VolumeからColor Gradingエフェクトを取得
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
            colorGrading.saturation.value = saturationValue; // 彩度を変更
        }
    }
}
