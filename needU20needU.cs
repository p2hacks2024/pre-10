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
        double spawn_time;//�m�[�c�̃^�C�~���O�����킹�邽�߂̎���
        double spawn_time_calculate(double spawn_x, double hit_x, double note_speed)//�����͈ꂩ�珇��prefab�̐������邘���W�A����̂����W�A�m�[�g�̃X�s�[�h�ł���B
        {
            double spawn_time;
            spawn_time = (spawn_x - hit_x) / note_speed;
            if (spawn_time > 0)
            {
                return spawn_time;
            }
            else if (spawn_time < 0)
            {
                return spawn_time * -1;
            }
            else
            {
                return 0;
            }
            //return�͐��̐��ŕԂ�
        }
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
                }
            }
            Debug.Log(count_37);
            for (int i = 0; i < count_37; i++)
            {
                spawn_prefab[i] = Random.Range(0, 5);//�����I�u�W�F�N�g�̌���6�����獡��0�`5�ɂ����B
                Debug.Log(spawn_prefab[i]);
            }
            spawn_time = spawn_time_calculate(-10, 0, 0);
        }

        private void Update()
        {
            count_time += Time.deltaTime;
            for (int i = 0; i < count_37; i++)
            {
                if(i==0 && data_37_realtime[0]==0)//0�b�ڂɃm�[�c������ꍇ��0�b�ŏo���B
                {
                    Instantiate(MessageObj[spawn_prefab[i]], new Vector3(-10, 0, 0), Quaternion.identity);
                }
                if (count_time > data_37_realtime[i] - spawn_time&& count_time < data_37_realtime[i]-spawn_time + Time.deltaTime)
                { 
                    Instantiate(MessageObj[spawn_prefab[i]], new Vector3(-10, 0, 0), Quaternion.identity);
                    //Vector3(x,y,z)��������ς���Ɛ�������邘���W���ς��B
                    Debug.Log("data_37_realtime " + data_37_realtime);
                }

            }
        }
    }
}