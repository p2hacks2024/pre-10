using UnityEngine;
using UnityEngine.SceneManagement;

public class ste2_JudgementArea : MonoBehaviour
{
    //ノーツが落ちてきたときに、キーボードを押したら判定したい
    //・キーボードからにの入力を受け付ける    InputKeyDown
    //・近くにノーツがあるのか                Rayを飛ばして当たったら近い!
    //・どれぐらいの近さなのか => (評価)

    [SerializeField] GameObject textEffectPrefab;//判定エフェクトのプレハブの受け取り
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
    float finish_time = 5;
    string score_text = "";
    string count_text = "10";


    private void Start()
    {
        //今は、ジャッジメントエリアは 4 にしてる
        perfectArea = 0.25;//ジャッジバー / 2  の長さ (ジャッジバーの横の長さは目で1の長さにした)
        goodArea = 0.5;
        badArea = 1;  //全体
    }
    private void Update()
    {
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
                            Debug.Log("execellent!!");
                            Destroy(hit2D.collider.gameObject);
                            GetComponent<AudioSource>().Play();//カメラのシャッター音
                            //SpawnTextEffect("perfect", transform.position);//判定エフェクトの表示
                            Instantiate(textEffectPrefab, new Vector3(325, 185, 0), Quaternion.identity);
                            score += 100;
                            score_text = score.ToString();

                        }
                        else if (distance < goodArea)
                        {
                            Debug.Log("good");
                            Destroy(hit2D.collider.gameObject);
                            GetComponent<AudioSource>().Play();//カメラのシャッター音
                            //SpawnTextEffect("good", transform.position);//判定エフェクトの表示
                            score += 75;
                            score_text = score.ToString();

                        }
                        else if (distance < badArea)
                        {
                            Debug.Log("bad");
                            Destroy(hit2D.collider.gameObject);
                            GetComponent<AudioSource>().Play();//カメラのシャッター音
                            //SpawnTextEffect("bad", transform.position);//判定エフェクトの表示
                            score += 50;
                            score_text = score.ToString();

                        }
                        //Destroy(hit2D.collider.gameObject);
                    }
                }
            }
        }

        Debug.Log(count);
        if (Input.GetKeyDown(KeyCode.R))
        {
            count = 100;
            count_text = count.ToString();
            real_time = 0;
        }
        //テストはできていないけどこの下は動くと思う。
        if (score > 2000)//scoreが一定を超えたらの条件分岐
        {//テキストでクリアとか出したらよさそう。
            finish_count += Time.deltaTime;
            if (finish_count > finish_time)
                SceneManager.LoadScene("clear(ste2)");//New Scene はSceneの名前に書き換える

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