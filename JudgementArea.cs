using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class JudgementArea : MonoBehaviour
{
    [SerializeField] Vector2 judgementAreaSize;
    public Vector2 extendjudgementAreaSize = new Vector2(1, 0);
    int count = 10;
    float reload_time = 2; // リロードにかかる時間
    float real_time = 0;   // リロード時間のカウント用
    int score = 0;
    string score_text = "a";

    private void Update()
    {
        real_time += Time.deltaTime;

        if (real_time > reload_time) // リロードの時間中はスペースキーを押しても反応しない
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                count--;
                Debug.Log("count: " + count);

                if (count > 0)
                {
                    RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, judgementAreaSize, 0, Vector2.zero);
                    if (hit2D)
                    {
                        score += 1;
                        score_text = score.ToString(); // 修正箇所
                        Debug.Log("Score: " + score);
                        Destroy(hit2D.collider.gameObject);
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

    void OnGUI()
    {
        // 文字列を画面に表示 
        Rect rect = new Rect(10, 10, 300, 50);
        GUI.Label(rect,score_text); // スコアを文字列で表示
    }

    // 当たり判定を可視化するための関数
    private void OnDrawGizmosSelected()
    {
        // 当たり判定の外側の範囲（スコアが高い）
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, judgementAreaSize + extendjudgementAreaSize);

        // 当たり判定の内側の範囲（スコアが低い）
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, judgementAreaSize);
    }
} // クラス全体を閉じる
