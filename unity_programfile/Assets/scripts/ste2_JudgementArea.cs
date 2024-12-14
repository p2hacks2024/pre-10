using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Audio;


public class ste2_JudgementArea : MonoBehaviour
{
    //ノーツが落ちてきたときに、キーボードを押したら判定したい
    //・キーボードからにの入力を受け付ける    InputKeyDown
    //・近くにノーツがあるのか                Rayを飛ばして当たったら近い!
    //・どれぐらいの近さなのか => (評価)

    //[SerializeField] GameObject textEffectPrefab;//判定エフェクトのプレハブの受け取り
    [SerializeField] Vector2 judgementAreaSize;//
    Vector2 extendJudgementAreaSize = new Vector2(2, 0);
    double perfectArea;
    double goodArea;
    double badArea;
    int count = 100;
    float reload_time = 2;
    float real_time = 0;
    float finish_count = 0;
    int score = 0;
    float finish_time = 1;
    string score_text = "";
    string count_text = "10";
    [SerializeField] VideoPlayer videoPlayer;

    AudioSource audioSource;

    public PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;

    double saturationRealTime = 0;
    double countTime = 0.2;


    private void Start()
    {
        //今は、ジャッジメントエリアは 4 にしてる
        perfectArea = 0.25;//ジャッジバー / 2  の長さ (ジャッジバーの横の長さは目で1の長さにした)
        goodArea = 0.5;
        badArea = 1;  //全体

        // PostProcessVolume の初期化
        if (postProcessVolume != null && postProcessVolume.profile != null)
        {
            if (postProcessVolume.profile.TryGetSettings(out colorGrading))
            {
                Debug.Log("Color Grading found!");
            }
            else
            {
                Debug.LogWarning("Color Grading not found in the Post-Processing profile.");
            }
        }
        else
        {
            Debug.LogError("PostProcessVolume or its profile is not assigned.");
        }

        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        real_time += Time.deltaTime;
        saturationRealTime += Time.deltaTime;
        if (saturationRealTime > countTime)
        {
            SetSaturation(-80);
        }
        real_time += Time.deltaTime;
        if (real_time > reload_time)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                count--;
                count_text = count.ToString();
                if (count > 0)
                {
                    Debug.Log("spaceキーを押した");
                    RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, judgementAreaSize, 0, Vector2.zero);
                    if (hit2D)
                    {
                        Debug.Log("ノーツがぶつかった");
                        float distance = Mathf.Abs(transform.position.x - hit2D.transform.position.x);


                        Debug.Log(distance);
                        if (distance <= perfectArea)
                        {
                            Debug.Log("perfect");
                            Destroy(hit2D.collider.gameObject);
                            score += 100;
                            score_text = score.ToString();
                            SetSaturation(0);
                            saturationRealTime = 0;

                        }
                        else if (distance < goodArea)
                        {
                            Debug.Log("good");
                            Destroy(hit2D.collider.gameObject);
                            score += 75;
                            score_text = score.ToString();
                            SetSaturation(0);
                            saturationRealTime = 0;

                        }
                        else if (distance < badArea)
                        {
                            Debug.Log("bad");
                            Destroy(hit2D.collider.gameObject);
                            score += 50;
                            score_text = score.ToString();
                            SetSaturation(0);
                            saturationRealTime = 0;
                        }
                        //Destroy(hit2D.collider.gameObject);
                    }
                }
            }
        }

        Debug.Log(count);
        if (Input.GetKeyDown(KeyCode.R))
        {
            videoPlayer.Play();
            count = 100;
            count_text = count.ToString();
            real_time = 0;
        }
        //テストはできていないけどこの下は動くと思う。
        if (score > 2000)//scoreが一定を超えたらの条件分岐
        {//テキストでクリアとか出したらよさそう。
            finish_count += Time.deltaTime;
            audioSource.volume -= Time.deltaTime;
            if (finish_count > finish_time)
                //SceneManager.LoadScene("clear(ste1)");//New Scene はSceneの名前に書き換える
                FadeManager.Instance.LoadScene("clear(ste2)", 1f);

        }
        count_text = count.ToString();
    }

    private GUIStyle labelStyle;

    private void OnGUI()
    {

        // GUIStyleのインスタンスを作成
        labelStyle = new GUIStyle();

        // フォントサイズを設定
        labelStyle.fontSize = 80; // 文字サイズを大きく
        labelStyle.normal.textColor = Color.white; // 文字色を白に設定（必要に応じて変更）

        Rect scoreRect = new Rect(700, 30, 600, 400);
        Rect countRect = new Rect(700, 200, 600, 400);
        GUI.Label(scoreRect, "SCORE : " + score_text, labelStyle);
        GUI.Label(countRect, "RELOAD : " + count_text, labelStyle);
    }

    //判定エフェクトを表示する関数
    //public void SpawnTextEffect(string message, Vector3 position)
    //{
    //    GameObject effectText = Instantiate(textEffectPrefab, position, Quaternion.identity);
    //    JudgementEffect judgementEffect = effectText.GetComponent<JudgementEffect>();
    //    judgementEffect.setText(message);
    //    Destroy(effectText, 0.5f);
    //}

    public void SetSaturation(float saturationValue)
    {
        if (colorGrading != null)
        {
            colorGrading.saturation.value = saturationValue;
            Debug.Log("satu : " + colorGrading.saturation.value);
        }
    }


    //当たり判定を可視化するための関数
    private void OnDrawGizmosSelected()
    {
        // 当たり判定の外側の範囲（スコアが高い）
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawCube(transform.position, judgementAreaSize + extendjudgementAreaSize);
        // 当たり判定の内側の範囲（スコアが低い）
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, judgementAreaSize); //全体 (bad)
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawCube(transform.position, new Vector2(2, 3)); //
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector2(0.5f, 1));
        //Gizmos.DrawCube

    }
}