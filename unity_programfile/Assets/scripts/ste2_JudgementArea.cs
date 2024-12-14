using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Audio;


public class ste2_JudgementArea : MonoBehaviour
{
    //�m�[�c�������Ă����Ƃ��ɁA�L�[�{�[�h���������画�肵����
    //�E�L�[�{�[�h����ɂ̓��͂��󂯕t����    InputKeyDown
    //�E�߂��Ƀm�[�c������̂�                Ray���΂��ē���������߂�!
    //�E�ǂꂮ�炢�̋߂��Ȃ̂� => (�]��)

    //[SerializeField] GameObject textEffectPrefab;//����G�t�F�N�g�̃v���n�u�̎󂯎��
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
    float finish_time = 1;
    string score_text = "";
    string count_text = "10";
    [SerializeField] VideoPlayer videoPlayer;

    AudioSource audioSource;

    public PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;

    double saturationRealTime = 0;
    double countTime = 0.2;


    private void Start()
    {
        //���́A�W���b�W�����g�G���A�� 4 �ɂ��Ă�
        perfectArea = 0.25;//�W���b�W�o�[ / 2  �̒��� (�W���b�W�o�[�̉��̒����͖ڂ�1�̒����ɂ���)
        goodArea = 0.5;
        badArea = 1;  //�S��

        // PostProcessVolume �̏�����
        if (postProcessVolume != null && postProcessVolume.profile != null)
        {
            if (postProcessVolume.profile.TryGetSettings(out colorGrading))
            {
                Debug.Log("Color Grading found!");
            }
            else
            {
                Debug.LogWarning("Color Grading not found in the Post-Processing profile.");
            }
        }
        else
        {
            Debug.LogError("PostProcessVolume or its profile is not assigned.");
        }

        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        real_time += Time.deltaTime;
        saturationRealTime += Time.deltaTime;
        if (saturationRealTime > countTime)
        {
            SetSaturation(-80);
        }
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
                            Debug.Log("perfect");
                            Destroy(hit2D.collider.gameObject);
                            score += 100;
                            score_text = score.ToString();
                            SetSaturation(0);
                            saturationRealTime = 0;

                        }
                        else if (distance < goodArea)
                        {
                            Debug.Log("good");
                            Destroy(hit2D.collider.gameObject);
                            score += 75;
                            score_text = score.ToString();
                            SetSaturation(0);
                            saturationRealTime = 0;

                        }
                        else if (distance < badArea)
                        {
                            Debug.Log("bad");
                            Destroy(hit2D.collider.gameObject);
                            score += 50;
                            score_text = score.ToString();
                            SetSaturation(0);
                            saturationRealTime = 0;
                        }
                        //Destroy(hit2D.collider.gameObject);
                    }
                }
            }
        }

        Debug.Log(count);
        if (Input.GetKeyDown(KeyCode.R))
        {
            videoPlayer.Play();
            count = 100;
            count_text = count.ToString();
            real_time = 0;
        }
        //�e�X�g�͂ł��Ă��Ȃ����ǂ��̉��͓����Ǝv���B
        if (score > 2000)//score�����𒴂�����̏�������
        {//�e�L�X�g�ŃN���A�Ƃ��o������悳�����B
            finish_count += Time.deltaTime;
            audioSource.volume -= Time.deltaTime;
            if (finish_count > finish_time)
                //SceneManager.LoadScene("clear(ste1)");//New Scene ��Scene�̖��O�ɏ���������
                FadeManager.Instance.LoadScene("clear(ste2)", 1f);

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

    public void SetSaturation(float saturationValue)
    {
        if (colorGrading != null)
        {
            colorGrading.saturation.value = saturationValue;
            Debug.Log("satu : " + colorGrading.saturation.value);
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
        Gizmos.DrawCube(transform.position, new Vector2(0.5f, 1));
        //Gizmos.DrawCube

    }
}