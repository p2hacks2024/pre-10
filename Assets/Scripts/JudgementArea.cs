using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class JudgementArea : MonoBehaviour
{
    //ノーツが落ちてきたときに、キーボードを押したら判定したい
    //・キーボードからにの入力を受け付ける    InputKeyDown
    //・近くにノーツがあるのか                Rayを飛ばして当たったら近い!
    //・どれぐらいの近さなのか => (評価)



    [SerializeField] GameObject textEffectPrefab;//
    [SerializeField] Vector2 judgementAreaSize;
    //[SerializeField] GameObject[] MessageObj; //判定メッセージ
    public Vector2 extendJudgementAreaSize = new Vector2(2, 0);
    public double perfectArea;
    public double goodArea;
    public double badArea;

    int count = 10;
    float reload_time = 2; // リロードにかかる時間
    float real_time = 0;   // リロード時間のカウント用
    int score = 0;
    string score_text = "0";


    private void Start()
    {
        //今は、ジャッジメントエリアは 6 にしてる
        perfectArea = 0.5;//ジャッジバー / 2  の長さ (ジャッジバーの横の長さは目で0.5の長さにした)
        goodArea = 1;
        badArea = 2;  //全体
    }
    private void Update() {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Instantiate(MessageObj[3], new Vector3(0, 0, 4), Quaternion.identity);
        //}

        real_time += Time.deltaTime;

        if (real_time > reload_time) // リロードの時間中はスペースキーを押しても反応しない
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                count--;
                if (count > 0)
                {
                    RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, judgementAreaSize, 0, Vector2.zero);
                    if (hit2D)
                    {
                        //Debug.Log("ノーツがぶつかった");
                        float distance = Mathf.Abs(transform.position.x - hit2D.transform.position.x);


                        Debug.Log(distance);
                        if (distance <= perfectArea)
                        {
                            Debug.Log("execellent!!");
                            GetComponent<AudioSource>().Play();
                            Destroy(hit2D.collider.gameObject);
                            SpawnTextEffect("perfect", transform.position);
                            score += 100;
                            score_text = score.ToString(); // 修正箇所
                            Debug.Log("Score: " + score);
                        }
                        else if (distance < goodArea)
                        {
                            Debug.Log("good");
                            GetComponent<AudioSource>().Play();
                            Destroy(hit2D.collider.gameObject);
                            
                            SpawnTextEffect("good", transform.position);

                            score += 50;
                            score_text = score.ToString(); // 修正箇所
                            Debug.Log("Score: " + score);
                        }
                        else if (distance < badArea)
                        {
                            Debug.Log("bad");
                            GetComponent<AudioSource>().Play();
                            Destroy(hit2D.collider.gameObject);
                            
                            SpawnTextEffect("bad", transform.position);

                            score += 10;
                            score_text = score.ToString(); // 修正箇所
                            Debug.Log("Score: " + score);
                        }
                        
                        //Destroy(hit2D.collider.gameObject);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            count = 10;
            Debug.Log("count: " + count);
            real_time = 0;
        }  
    }

    public void SpawnTextEffect(string message, Vector3 position)
    {
        GameObject effectText = Instantiate(textEffectPrefab, position, Quaternion.identity);
        JudgementEffect judgementEffect = effectText.GetComponent<JudgementEffect>();
        judgementEffect.setText(message);
        Destroy(effectText, 0.5f);
    }

    void OnGUI()
    {
        // 文字列を画面に表示 
        Rect rect = new Rect(10, 10, 300, 50);
        GUI.Label(rect, "SCORE : " + score_text); // スコアを文字列で表示
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
        //Gizmos.DrawCube(transform.position, new Vector2((0.5, 3));
        //Gizmos.DrawCube
    }
}

