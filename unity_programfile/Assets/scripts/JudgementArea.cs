using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Audio;

public class JudgementArea : MonoBehaviour
{
    //�m�[�c�������Ă����Ƃ��ɁA�L�[�{�[�h���������画�肵����
    //�E�L�[�{�[�h����ɂ̓��͂��󂯕t����    InputKeyDown
    //�E�߂��Ƀm�[�c������̂�                Ray���΂��ē���������߂�!
    //�E�ǂꂮ�炢�̋߂��Ȃ̂� => (�]��)

    [SerializeField] GameObject textEffectPrefab;//����G�t�F�N�g�̃v���n�u�̎󂯎��
    [SerializeField] Vector2 judgementAreaSize;//
    Vector2 extendJudgementAreaSize = new Vector2(2, 0);
    double perfectArea = 0.5;
    double goodArea = 1;
    double badArea = 2;
    int count = 100;
    float reload_time = 2;
    float real_time = 0;
    float finish_count = 0;
    int score = 0;
    float finish_time=1;
    string score_text = "0";
    string count_text = "10";
    //double realTime = 0;
    double saturatioRealTime = 0;
    double countTime = 0.2;



    public PostProcessVolume postProcessVolume; // Post Process Volume�����蓖�Ă�
    private ColorGrading colorGrading;          // Color Grading�G�t�F�N�g�ւ̎Q��
    AudioSource audioSource;


    //private void Start()
    //{
    //    //���́A�W���b�W�����g�G���A�� 4 �ɂ��Ă�
    //    perfectArea = 0.5;//�W���b�W�o�[ / 2  �̒��� (�W���b�W�o�[�̉��̒����͖ڂ�1�̒����ɂ���)
    //    goodArea = 1;
    //    badArea = 2;  //�S��
    //}

    void Start()
    {
        // Volume����Color Grading�G�t�F�N�g���擾
        if (postProcessVolume.profile.TryGetSettings(out colorGrading))
        {
            Debug.Log("Color Grading found!");
        }
        else
        {
            Debug.LogError("Color Grading not found in Post Process Volume.");
        }
        audioSource = GetComponent<AudioSource>();
    }



    private void Update()
    {

        real_time += Time.deltaTime;
        saturatioRealTime += Time.deltaTime;
        if (saturatioRealTime > countTime) //�ʓx��߂��Ă���
        {
            SetSaturation(-40);
        }

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
                            //GetComponent<AudioSource>().Play();//�J�����̃V���b�^�[��
                            //SpawnTextEffect("perfect", transform.position);//����G�t�F�N�g�̕\��
                            //Instantiate(textEffectPrefab, new Vector3(325, 185, 0), Quaternion.identity);
                            score += 100;
                            score_text = score.ToString();

                            SetSaturation(0);
                            saturatioRealTime = 0;

                        }
                        else if (distance < goodArea)
                        {
                            Debug.Log("good");
                            Destroy(hit2D.collider.gameObject);
                            //GetComponent<AudioSource>().Play();//�J�����̃V���b�^�[��
                            //SpawnTextEffect("good", transform.position);//����G�t�F�N�g�̕\��
                            score += 75;
                            score_text = score.ToString();

                            SetSaturation(0);
                            saturatioRealTime = 0;

                        }
                        else if (distance < badArea)
                        {
                            Debug.Log("bad");
                            Destroy(hit2D.collider.gameObject);
                            //GetComponent<AudioSource>().Play();//�J�����̃V���b�^�[��
                            //SpawnTextEffect("bad", transform.position);//����G�t�F�N�g�̕\��
                            score += 50;
                            score_text = score.ToString();

                            SetSaturation(0);
                            saturatioRealTime = 0;

                        }
                        //Destroy(hit2D.collider.gameObject);
                    }
                }
            }
        }

        Debug.Log(count);
        if (Input.GetKeyDown(KeyCode.R))
        {
            count = 10;
            count_text = count.ToString();
            real_time = 0;
        }
        //�e�X�g�͂ł��Ă��Ȃ����ǂ��̉��͓����Ǝv���B
        if (score > 500)//score�����𒴂�����̏�������
        {//�e�L�X�g�ŃN���A�Ƃ��o������悳�����B
            finish_count += Time.deltaTime;
            audioSource.volume -= Time.deltaTime;
            if (finish_count > finish_time)
            {
                //SceneManager.LoadScene("clear");//New Scene ��Scene�̖��O�ɏ���������
                FadeManager.Instance.LoadScene("clear", 1f);
            }

        }
        count_text = count.ToString();
    }
    private void OnGUI()
    {
        Rect scoreRect = new Rect(10, 10, 300, 50);
        Rect countRect = new Rect(10, 30, 300, 50);
        GUI.Label(scoreRect, "SCORE : " + score_text);
        GUI.Label(countRect, "RELOAD : " + count_text);
    }

    //����G�t�F�N�g��\������֐�
    //public void SpawnTextEffect(string message, Vector3 position)
    //{
    //    GameObject effectText = Instantiate(textEffectPrefab, position, Quaternion.identity);
    //    JudgementEffect judgementEffect = effectText.GetComponent<JudgementEffect>();
    //    judgementEffect.setText(message);
    //    Destroy(effectText, 0.5f);
    //}

    public void SetSaturation(float saturationValue)
    {

        if (colorGrading != null)
        {
            colorGrading.saturation.value = saturationValue; // �ʓx�𖾂邭����

            
            Debug.Log(colorGrading.saturation.value);
        }
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
        Gizmos.DrawCube(transform.position, new Vector2(2, 5));
        //Gizmos.DrawCube

    }
}