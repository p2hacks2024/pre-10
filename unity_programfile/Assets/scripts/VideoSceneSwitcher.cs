using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneSwitcher : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // VideoPlayer �R���|�[�l���g���擾
        videoPlayer = GetComponent<VideoPlayer>();

        // �r�f�I���I�������Ƃ��ɌĂяo�����C�x���g��ݒ�
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // �ʂ̃V�[���Ɉړ�
        SceneManager.LoadScene("sele(title - 1)"); // "NextScene" ���ړ���̃V�[�����ɕύX
    }
}
