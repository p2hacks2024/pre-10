using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class JudgementArea : MonoBehaviour
{
    //�m�[�c�������Ă����Ƃ��ɁA�L�[�{�[�h���������画�肵����
    //�E�L�[�{�[�h����ɂ̓��͂��󂯕t����    InputKeyDown
    //�E�߂��Ƀm�[�c������̂�                Ray���΂��ē���������߂�!
    //�E�ǂꂮ�炢�̋߂��Ȃ̂� => (�]��)



    [SerializeField] GameObject textEffectPrefab;//
    [SerializeField] Vector2 judgementAreaSize;
    //[SerializeField] GameObject[] MessageObj; //���胁�b�Z�[�W
    public Vector2 extendJudgementAreaSize = new Vector2(2, 0);
    public double perfectArea;
    public double goodArea;
    public double badArea;

    int count = 10;
    float reload_time = 2; // �����[�h�ɂ����鎞��
    float real_time = 0;   // �����[�h���Ԃ̃J�E���g�p
    int score = 0;
    string score_text = "0";


    private void Start()
    {
        //���́A�W���b�W�����g�G���A�� 6 �ɂ��Ă�
        perfectArea = 0.5;//�W���b�W�o�[ / 2  �̒��� (�W���b�W�o�[�̉��̒����͖ڂ�0.5�̒����ɂ���)
        goodArea = 1;
        badArea = 2;  //�S��
    }
    private void Update() {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Instantiate(MessageObj[3], new Vector3(0, 0, 4), Quaternion.identity);
        //}

        real_time += Time.deltaTime;

        if (real_time > reload_time) // �����[�h�̎��Ԓ��̓X�y�[�X�L�[�������Ă��������Ȃ�
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                count--;
                if (count > 0)
                {
                    RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, judgementAreaSize, 0, Vector2.zero);
                    if (hit2D)
                    {
                        //Debug.Log("�m�[�c���Ԃ�����");
                        float distance = Mathf.Abs(transform.position.x - hit2D.transform.position.x);


                        Debug.Log(distance);
                        if (distance <= perfectArea)
                        {
                            Debug.Log("execellent!!");
                            GetComponent<AudioSource>().Play();
                            Destroy(hit2D.collider.gameObject);
                            SpawnTextEffect("perfect", transform.position);
                            score += 100;
                            score_text = score.ToString(); // �C���ӏ�
                            Debug.Log("Score: " + score);
                        }
                        else if (distance < goodArea)
                        {
                            Debug.Log("good");
                            GetComponent<AudioSource>().Play();
                            Destroy(hit2D.collider.gameObject);
                            
                            SpawnTextEffect("good", transform.position);

                            score += 50;
                            score_text = score.ToString(); // �C���ӏ�
                            Debug.Log("Score: " + score);
                        }
                        else if (distance < badArea)
                        {
                            Debug.Log("bad");
                            GetComponent<AudioSource>().Play();
                            Destroy(hit2D.collider.gameObject);
                            
                            SpawnTextEffect("bad", transform.position);

                            score += 10;
                            score_text = score.ToString(); // �C���ӏ�
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
        // ���������ʂɕ\�� 
        Rect rect = new Rect(10, 10, 300, 50);
        GUI.Label(rect, "SCORE : " + score_text); // �X�R�A�𕶎���ŕ\��
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

