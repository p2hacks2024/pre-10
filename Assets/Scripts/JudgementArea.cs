using UnityEngine;
using UnityEngine.UIElements;

public class JudgementArea : MonoBehaviour
{
    //�m�[�c�������Ă����Ƃ��ɁA�L�[�{�[�h���������画�肵����
    //�E�L�[�{�[�h����ɂ̓��͂��󂯕t����    InputKeyDown
    //�E�߂��Ƀm�[�c������̂�                Ray���΂��ē���������߂�!
    //�E�ǂꂮ�炢�̋߂��Ȃ̂� => (�]��)

    [SerializeField] Vector2 judgementAreaSize;
    public Vector2 extendjudgementAreaSize = new Vector2(1, 0);
    private void Update(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space�L�[��������");
            RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, judgementAreaSize, 0, Vector2.zero);
            if (hit2D)
            {
                Debug.Log("�m�[�c���Ԃ�����");

                Destroy(hit2D.collider.gameObject);

            }

        }
    }

    //�����蔻����������邽�߂̊֐�
    private void OnDrawGizmosSelected()
    {
        // �����蔻��̊O���͈̔́i�X�R�A�������j
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, judgementAreaSize + extendjudgementAreaSize);
        // �����蔻��̓����͈̔́i�X�R�A���Ⴂ�j
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, judgementAreaSize);
    }





}
