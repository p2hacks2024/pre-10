using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Text;
using UnityEngine.SceneManagement;

public class JudgementArea : MonoBehaviour
{
    //�m�[�c�������Ă����Ƃ��ɁA�L�[�{�[�h���������画�肵����
    //�E�L�[�{�[�h����ɂ̓��͂��󂯕t����    InputKeyDown
    //�E�߂��Ƀm�[�c������̂�                Ray���΂��ē���������߂�!
    //�E�ǂꂮ�炢�̋߂��Ȃ̂� => (�]��)

    [SerializeField] Vector2 judgementAreaSize;
    public Vector2 extendJudgementAreaSize = new Vector2(2, 0);
    public double perfectArea;
    public double goodArea;
    public double badArea;
    int count = 10;
    float reload_time = 2;
    float real_time = 0;
    float finish_count = 0;
    int score = 0;
    float finish_time=5;
    string score_text = "";


    private void Start()
    {
        //���́A�W���b�W�����g�G���A�� 6 �ɂ��Ă�
        perfectArea = 0.5;//�W���b�W�o�[ / 2  �̒��� (�W���b�W�o�[�̉��̒����͖ڂ�0.5�̒����ɂ���)
        goodArea = 1;
        badArea = 2;  //�S��
    }
    private void Update()
    {
        real_time += Time.deltaTime;
        if (real_time > reload_time)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                count--;
                if (count > 0)
                {
                    Debug.Log("space�L�[��������");
                    RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, judgementAreaSize, 0, Vector2.zero);
                    if (hit2D)
                    {
                        //Debug.Log("�m�[�c���Ԃ�����");
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
                            score += 100;
                            score_text = score.ToString();
                        }
                        else if (distance < goodArea)
                        {
                            Debug.Log("good");
                            Destroy(hit2D.collider.gameObject);
                            score += 75;
                            score_text = score.ToString();
                        }
                        else if (distance < badArea)
                        {
                            Debug.Log("bad");
                            Destroy(hit2D.collider.gameObject);
                            score += 50;
                            score_text = score.ToString();
                        }
                        //Destroy(hit2D.collider.gameObject);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            count = 10;
            real_time = 0;
        }
        //�e�X�g�͂ł��Ă��Ȃ����ǂ��̉��͓����Ǝv���B
        if (score > 5000)//score�����𒴂�����̏�������
        {//�e�L�X�g�ŃN���A�Ƃ��o������悳�����B
            finish_count += Time.deltaTime;
            if (finish_count > finish_time)
                SceneManager.LoadScene("clear");//New Scene ��Scene�̖��O�ɏ���������

        }
    }
    private void OnGUI()
    {
        Rect rect = new Rect(10, 10, 300, 50);
        GUI.Label(rect, score_text);
    }
    //�����蔻����������邽�߂̊֐�
    private void OnDrawGizmosSelected()
    {
        // �����蔻��̊O���͈̔́i�X�R�A�������j
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawCube(transform.position, judgementAreaSize + extendjudgementAreaSize);
        // �����蔻��̓����͈̔́i�X�R�A���Ⴂ�j
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, judgementAreaSize); //�S�� (bad)
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawCube(transform.position, new Vector2(2, 3)); //
        Gizmos.color = Color.red;
        //Gizmos.DrawCube(transform.position, new Vector2((0.5, 3));
        //Gizmos.DrawCube

    }
}