using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class JudgementArea : MonoBehaviour
{
    [SerializeField] Vector2 judgementAreaSize;
    public Vector2 extendjudgementAreaSize = new Vector2(1, 0);
    int count = 10;
    float reload_time = 2; // �����[�h�ɂ����鎞��
    float real_time = 0;   // �����[�h���Ԃ̃J�E���g�p
    int score = 0;
    string score_text = "a";

    private void Update()
    {
        real_time += Time.deltaTime;

        if (real_time > reload_time) // �����[�h�̎��Ԓ��̓X�y�[�X�L�[�������Ă��������Ȃ�
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
                        score_text = score.ToString(); // �C���ӏ�
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
        // ���������ʂɕ\�� 
        Rect rect = new Rect(10, 10, 300, 50);
        GUI.Label(rect,score_text); // �X�R�A�𕶎���ŕ\��
    }

    // �����蔻����������邽�߂̊֐�
    private void OnDrawGizmosSelected()
    {
        // �����蔻��̊O���͈̔́i�X�R�A�������j
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, judgementAreaSize + extendjudgementAreaSize);

        // �����蔻��̓����͈̔́i�X�R�A���Ⴂ�j
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, judgementAreaSize);
    }
} // �N���X�S�̂����
