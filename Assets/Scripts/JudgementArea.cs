using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class JudgementArea : MonoBehaviour
{
    //ノーツが落ちてきたときに、キーボードを押したら判定したい
    //・キーボードからにの入力を受け付ける    InputKeyDown
    //・近くにノーツがあるのか                Rayを飛ばして当たったら近い!
    //・どれぐらいの近さなのか => (評価)

    [SerializeField] Vector2 judgementAreaSize;
    public Vector2 extendJudgementAreaSize = new Vector2(2, 0);
    public double perfectArea;
    public double goodArea;
    public double badArea;

    private void Start()
    {
        //今は、ジャッジメントエリアは 6 にしてる
        perfectArea = 0.5;//ジャッジバー / 2  の長さ (ジャッジバーの横の長さは目で0.5の長さにした)
        goodArea = 1;
        badArea = 2;  //全体
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("spaceキーを押した");
            RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, judgementAreaSize, 0, Vector2.zero);
            if (hit2D)
            {
                //Debug.Log("ノーツがぶつかった");
                float distance = Mathf.Abs(transform.position.x - hit2D.transform.position.x);
                //if (distance >= -1)
                //{
                //    if (distance < judgementAreaSize.x)
                //    {
                //        Debug.Log("GREATE!!");
                //    }
                //    else if (distance < judgementAreaSize.x + 3)
                //    {
                //        Debug.Log("EARLY");
                //    }
                //}
                //else
                //{
                //    Debug.Log("LATER...");
                //}

                Debug.Log(distance);
                if (distance <= perfectArea)
                {
                    Debug.Log("execellent!!");
                    Destroy(hit2D.collider.gameObject);
                }
                else if (distance < goodArea)
                {
                    Debug.Log("good");
                    Destroy(hit2D.collider.gameObject);
                }
                else if(distance < badArea)
                {
                    Debug.Log("bad");
                    Destroy(hit2D.collider.gameObject);
                }
                //Destroy(hit2D.collider.gameObject);
            }
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
        //Gizmos.DrawCube(transform.position, new Vector2((0.5, 3));
        //Gizmos.DrawCube

    }
}

