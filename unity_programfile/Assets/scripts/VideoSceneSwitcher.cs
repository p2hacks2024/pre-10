using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneSwitcher : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // VideoPlayer コンポーネントを取得
        videoPlayer = GetComponent<VideoPlayer>();

        // ビデオが終了したときに呼び出されるイベントを設定
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // 別のシーンに移動
        SceneManager.LoadScene("sele(title - 1)"); // "NextScene" を移動先のシーン名に変更
    }
}
