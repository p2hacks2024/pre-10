using UnityEngine;
using UnityEngine.SceneManagement;

public class ste2_JudgementArea : MonoBehaviour
{
    //�m�[�c�������Ă����Ƃ��ɁA�L�[�{�[�h���������画�肵����
    //�E�L�[�{�[�h����ɂ̓��͂��󂯕t����    InputKeyDown
    //�E�߂��Ƀm�[�c������̂�                Ray���΂��ē���������߂�!
    //�E�ǂꂮ�炢�̋߂��Ȃ̂� => (�]��)

    [SerializeField] GameObject textEffectPrefab;//����G�t�F�N�g�̃v���n�u�̎󂯎��
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
        //���́A�W���b�W�����g�G���A�� 4 �ɂ��Ă�
        perfectArea = 0.25;//�W���b�W�o�[ / 2  �̒��� (�W���b�W�o�[�̉��̒����͖ڂ�1�̒����ɂ���)
        goodArea = 0.5;
        badArea = 1;  //�S��
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
                    Debug.Log("space�L�[��������");
                    RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, judgementAreaSize, 0, Vector2.zero);
                    if (hit2D)
                    {
                        Debug.Log("�m�[�c���Ԃ�����");
                        float distance = Mathf.Abs(transform.position.x - hit2D.transform.position.x);


                        Debug.Log(distance);
                        if (distance <= perfectArea)
                        {
                            Debug.Log("execellent!!");
                            Destroy(hit2D.collider.gameObject);
                            GetComponent<AudioSource>().Play();//�J�����̃V���b�^�[��
                            //SpawnTextEffect("perfect", transform.position);//����G�t�F�N�g�̕\��
                            Instantiate(textEffectPrefab, new Vector3(325, 185, 0), Quaternion.identity);
                            score += 100;
                            score_text = score.ToString();

                        }
                        else if (distance < goodArea)
                        {
                            Debug.Log("good");
                            Destroy(hit2D.collider.gameObject);
                            GetComponent<AudioSource>().Play();//�J�����̃V���b�^�[��
                            //SpawnTextEffect("good", transform.position);//����G�t�F�N�g�̕\��
                            score += 75;
                            score_text = score.ToString();

                        }
                        else if (distance < badArea)
                        {
                            Debug.Log("bad");
                            Destroy(hit2D.collider.gameObject);
                            GetComponent<AudioSource>().Play();//�J�����̃V���b�^�[��
                            //SpawnTextEffect("bad", transform.position);//����G�t�F�N�g�̕\��
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
        //�e�X�g�͂ł��Ă��Ȃ����ǂ��̉��͓����Ǝv���B
        if (score > 2000)//score�����𒴂�����̏�������
        {//�e�L�X�g�ŃN���A�Ƃ��o������悳�����B
            finish_count += Time.deltaTime;
            if (finish_count > finish_time)
                SceneManager.LoadScene("clear(ste2)");//New Scene ��Scene�̖��O�ɏ���������

        }
        count_text = count.ToString();
    }

    private GUIStyle labelStyle;

    private void OnGUI()
    {

        // GUIStyle�̃C���X�^���X���쐬
        labelStyle = new GUIStyle();

        // �t�H���g�T�C�Y��ݒ�
        labelStyle.fontSize = 80; // �����T�C�Y��傫��
        labelStyle.normal.textColor = Color.white; // �����F�𔒂ɐݒ�i�K�v�ɉ����ĕύX�j

        Rect scoreRect = new Rect(700, 30, 600, 400);
        Rect countRect = new Rect(700, 200, 600, 400);
        GUI.Label(scoreRect, "SCORE : " + score_text, labelStyle);
        GUI.Label(countRect, "RELOAD : " + count_text, labelStyle);
    }

    //����G�t�F�N�g��\������֐�
    //public void SpawnTextEffect(string message, Vector3 position)
    //{
    //    GameObject effectText = Instantiate(textEffectPrefab, position, Quaternion.identity);
    //    JudgementEffect judgementEffect = effectText.GetComponent<JudgementEffect>();
    //    judgementEffect.setText(message);
    //    Destroy(effectText, 0.5f);
    //}


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
        Gizmos.DrawCube(transform.position, new Vector2(0.5f, 1));
        //Gizmos.DrawCube

    }
}