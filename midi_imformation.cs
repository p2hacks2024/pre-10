/*MidiEvent�̓C�x���g�������ԁiTick�j�A�`�����l���̃m�[�g��on,off(status),
              ���̍����A�m�[�g�i���o�[�idata1�j�A���F���V�e�B�idata2)�Ƃ��Ď��B
 ���A���^�C���ւ̕ϊ�����
a:realtime
temp:�Ȃ̃e���|
�l��������480ticks�ł���
a=tick/(8*temp)
 */

using JetBrains.Annotations;
using Klak.Timeline.Midi;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
namespace Logger
{
    public class midi_imfomation : MonoBehaviour
    {
        [SerializeField] private MidiAnimationAsset _asset;
        int[] data_43 = new int[500];//�m�[�g�i���o�[�S�R�Ԃ̉��̃m�[�g�I���̎���ticks���i�[
        float[] data_43_realtime = new float[500];//tick�����A���^�C���ɒ��������̂��i�[
        int[] data_38 = new int[500];//�m�[�g�i���o�[�R�W�Ԃ̉��̃m�[�g�I���̎���ticks���i�[
        float[] data_38_realtime = new float[500];//tick�����A���^�C���ɒ��������̂��i�[
        int[] data_36 = new int[500];//�m�[�g�i���o�[�R�U�Ԃ̉��̃m�[�g�I���̎���ticks���i�[
        float[] data_36_realtime = new float[500];//tick�����A���^�C���ɒ��������̂��i�[
        int temp=100;//�Ȃ̃e���|
        float count_time = 0;//�m�[�c�𐶐����鎞�Ԃ��Ǘ����邽�߂̎���
        int count_43 = 0;//�m�[�g�i���o�[�S�R�̐M���̐����L�^
        int count_38 = 0;//�m�[�g�i���o�[�R�W�̐M���̐����L�^
        int count_36 = 0;//�m�[�g�i���o�[�R�U�̐M���̐����L�^
        int score = 0;
        int[] spawn_prefab = new int[500];//�ǂ̃I�u�W�F�N�g���o�����̎w��z��B
        [SerializeField] GameObject[] MessageObj; //prefab�𕡐��w��B
        private void Start()
        {
            var midiEventSet = _asset.template.events;
            
            foreach (MidiEvent midiEvent in midiEventSet)
            {
                if (midiEvent.status == 144)//�m�[�g�I���P�S�S�i10�i���j�m�[�g�I�t�P�Q�W�i10�i���j
                {
                    if (midiEvent.data1 == 43)//�Ȃɂ���ĕς���K�v������
                    {
                        data_43[count_43] = (int)midiEvent.time;
                        data_43_realtime[count_43] = (float)midiEvent.time/(temp*8); 
                        count_43++;           
                    }
                    if (midiEvent.data1 == 38)//�Ȃɂ���ĕς���K�v������
                    {
                        data_38[count_38] = (int)midiEvent.time;
                        data_38_realtime[count_38] = (float)midiEvent.time / (temp * 8);
                        count_38++;
                    }
                    if (midiEvent.data1 == 36)//�Ȃɂ���ĕς���K�v������
                    {
                        data_36[count_36] = (int)midiEvent.time;
                        data_36_realtime[count_36] = (float)midiEvent.time / (temp * 8);
                        count_36++;
                    }
                    //�m�[�c�i���o�[�m�F�p
                   // Debug.Log(midiEvent.ToString());
                }
            }
            Debug.Log(count_38);
            for (int i = 0; i < count_38; i++)
            {
                spawn_prefab[i] = Random.Range(1,3);//�����I�u�W�F�N�g�̌���3�����獡��1�`�R�ɂ����B
                Debug.Log(spawn_prefab[i]);
            }
           /* �m�F�p
            for (int i = 0; i < count_43; i++)
            {
                Debug.Log("data_43 " + data_43[i]);
                Debug.Log("data_43_realtime " + data_43_realtime[i]);
            }
            for (int i = 0; i < count_38; i++)
                Debug.Log("data_38 " + data_38[i]);
            for (int i = 0; i < count_36; i++)
                Debug.Log("data_36 " + data_36[i]);
           */
        }

        private void Update()
        {
            count_time += Time.deltaTime;
            for (int i = 0; i < count_38; i++)
            {
                if (count_time > data_38_realtime[i] -3.5&& count_time < data_38_realtime[i]-3.5+Time.deltaTime)
                    /*�^�C�~���O�����̂��߂�-3.5�����Ă���BTime.deltaTime�𑫂����Ƃɂ��
                    ��ȏ�̃I�u�W�F�N�g�̐�����h���B
                    */
                    //�D���Ȃ��̂��g���Ɨǂ�
                {
                    Instantiate(MessageObj[spawn_prefab[i]], new Vector3(-30,0,0), Quaternion.identity);
                    Debug.Log("data_38_realtime " + data_38_realtime);
                }
                //if (count_time > data_36_realtime[i] -3.5  && count_time < data_36_realtime[i]-3.5+Time.deltaTime)
                //{
                //    Instantiate(Cube, new Vector3(0, 0, 0), Quaternion.identity);
                //    Debug.Log("data_36_realtime " + data_36_realtime);
                //}
                //if (count_time > data_43_realtime[i] - 3.5 && count_time < data_43_realtime[i]-3.5+Time.deltaTime)
                //{
                //    Instantiate(Cube, new Vector3(0, 0, 0), Quaternion.identity);
                //    Debug.Log("data_43_realtime " + data_43_realtime);
                //}
            }
            /*
            if (Input.GetKeyDown(KeyCode.Space))//score�����𒴂�����̏�������
            {

            }
             */
            
        }
    }
}