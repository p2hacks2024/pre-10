/*MidiEventはイベント発生時間（Tick）、チャンネルのノートのon,off(status),
              音の高さ、ノートナンバー（data1）、ヴェロシティ（data2)として持つ。
 リアルタイムへの変換公式
a:realtime
temp:曲のテンポ
四分音符が480ticksである
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
        int[] data_37 = new int[500];//ノートナンバー37番の音のノートオンの時のticksを格納
        float[] data_37_realtime = new float[500];//tickをリアルタイムに直したものを格納
        int temp = 120;//曲のテンポ
        float count_time = 0;//ノーツを生成する時間を管理するための時間
        int count_37 = 0;//ノートナンバー37の信号の数を記録
        int score = 0;
        int[] spawn_prefab = new int[500];//どのオブジェクトを出すかの指定配列。
        [SerializeField] GameObject[] MessageObj; //prefabを複数指定。
        double spawn_time;//ノーツのタイミングを合わせるための時間
        double spawn_time_calculate(double spawn_x, double hit_x, double note_speed)//引数は一から順にprefabの生成するｘ座標、判定のｘ座標、ノートのスピードである。
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
            //returnは正の数で返す
        }
        private void Start()
        {
            var midiEventSet = _asset.template.events;

            foreach (MidiEvent midiEvent in midiEventSet)
            {
                if (midiEvent.status == 144)//ノートオン１４４（10進数）ノートオフ１２８（10進数）
                {
                    if (midiEvent.data1 == 37)//曲によって変える必要がある
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
                spawn_prefab[i] = Random.Range(0, 5);//個数がオブジェクトの個数が6つだから今回0～5にした。
                Debug.Log(spawn_prefab[i]);
            }
            spawn_time = spawn_time_calculate(-10, 0, 0);
        }

        private void Update()
        {
            count_time += Time.deltaTime;
            for (int i = 0; i < count_37; i++)
            {
                if(i==0 && data_37_realtime[0]==0)//0秒目にノーツがある場合は0秒で出す。
                {
                    Instantiate(MessageObj[spawn_prefab[i]], new Vector3(-10, 0, 0), Quaternion.identity);
                }
                if (count_time > data_37_realtime[i] - spawn_time&& count_time < data_37_realtime[i]-spawn_time + Time.deltaTime)
                { 
                    Instantiate(MessageObj[spawn_prefab[i]], new Vector3(-10, 0, 0), Quaternion.identity);
                    //Vector3(x,y,z)第一引数を変えると生成されるｘ座標が変わる。
                    Debug.Log("data_37_realtime " + data_37_realtime);
                }

            }
        }
    }
}