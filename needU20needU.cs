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
    public class needU20needU : MonoBehaviour
    {
        [SerializeField] private MidiAnimationAsset _asset;
        int[] data_37 = new int[500];//�m�[�g�i���o�[37�Ԃ̉��̃m�[�g�I���̎���ticks���i�[
        float[] data_37_realtime = new float[500];//tick�����A���^�C���ɒ��������̂��i�[
        int temp = 120;//�Ȃ̃e���|
        float count_time = 0;//�m�[�c�𐶐����鎞�Ԃ��Ǘ����邽�߂̎���
        int count_37 = 0;//�m�[�g�i���o�[37�̐M���̐����L�^
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
                    if (midiEvent.data1 == 37)//�Ȃɂ���ĕς���K�v������
                    {
                        data_37[count_37] = (int)midiEvent.time;
                        data_37_realtime[count_37] = (float)midiEvent.time / (temp * 8);
                        count_37++;
                    }

                    //�m�[�c�i���o�[�m�F�p
                    Debug.Log(midiEvent.ToString());
                }
            }
            Debug.Log(count_37);
            //for (int i = 0; i < count_37; i++)
            //{
            //    spawn_prefab[i] = Random.Range(0,2);//�����I�u�W�F�N�g�̌���3�����獡��0�`2�ɂ����B
            //    Debug.Log(spawn_prefab[i]);
            //}
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
            for (int i = 0; i < count_37; i++)
            {
                if (count_time > data_37_realtime[i] && count_time < data_37_realtime[i] + Time.deltaTime)
                /*�^�C�~���O�����̂��߂�-3.5�����Ă���BTime.deltaTime�𑫂����Ƃɂ��
                ��ȏ�̃I�u�W�F�N�g�̐�����h���B
                */
                {
                    Instantiate(MessageObj[spawn_prefab[i]], new Vector3(0, 0, 0), Quaternion.identity);
                    //Vector3(x,y,z)��������ς���Ɛ�������邘���W���ς��B
                    Debug.Log("data_37_realtime " + data_37_realtime);
                }

            }
        }
    }
}