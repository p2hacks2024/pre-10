using UnityEngine;
using UnityEngine.UIElements;

public class JudgementArea : MonoBehaviour
{
    //�m�[�c�������Ă����Ƃ��ɁA�L�[�{�[�h���������画�肵����
    //�E�L�[�{�[�h����ɂ̓��͂��󂯕t����    InputKeyDown
    //�E�߂��Ƀm�[�c������̂�                Ray���΂��ē���������߂�!
    //�E�ǂꂮ�炢�̋߂��Ȃ̂�(�]��)

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space�L�[��������");
            RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, Vector2.zero);
            if (hit2D)
            {
                Debug.Log("�m�[�c���Ԃ�����");
                Destroy(hit2D.collider.gameObject);

            }

        }

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Debug.Log("a!!");
        //}
    }




}
