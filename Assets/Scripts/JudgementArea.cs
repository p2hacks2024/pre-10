using UnityEngine;
using UnityEngine.UIElements;

public class JudgementArea : MonoBehaviour
{
    //ノーツが落ちてきたときに、キーボードを押したら判定したい
    //・キーボードからにの入力を受け付ける    InputKeyDown
    //・近くにノーツがあるのか                Rayを飛ばして当たったら近い!
    //・どれぐらいの近さなのか => (評価)

    [SerializeField] Vector2 judgementAreaSize;
    public Vector2 extendjudgementAreaSize = new Vector2(1, 0);
    private void Update(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("spaceキーを押した");
            RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, judgementAreaSize, 0, Vector2.zero);
            if (hit2D)
            {
                Debug.Log("ノーツがぶつかった");

                Destroy(hit2D.collider.gameObject);

            }

        }
    }

    //当たり判定を可視化するための関数
    private void OnDrawGizmosSelected()
    {
        // 当たり判定の外側の範囲（スコアが高い）
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, judgementAreaSize + extendjudgementAreaSize);
        // 当たり判定の内側の範囲（スコアが低い）
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, judgementAreaSize);
    }





}
