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

                    //ノーツナンバー確認用
                    //Debug.Log(midiEvent.ToString());
                }
            }
            //Debug.Log(count_37);
            for (int i = 0; i < count_37; i++)
            {
                spawn_prefab[i] = Random.Range(0, 5);//個数がオブジェクトの個数が3つだから今回0～2にした。
                Debug.Log(spawn_prefab[i]);
            }
            /* 確認用
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
                if (count_time > data_37_realtime[i] - 3.4 && count_time < data_37_realtime[i] - 3.4 + Time.deltaTime)
                /*タイミング調整のために-3.5をつけている。Time.deltaTimeを足すことにより
                二つ以上のオブジェクトの生成を防ぐ。
                */
                {
                    Instantiate(MessageObj[spawn_prefab[i]], new Vector3(-10, 0, 0), Quaternion.identity);
                    //Vector3(x,y,z)第一引数を変えると生成されるｘ座標が変わる。
                    Debug.Log("data_37_realtime " + data_37_realtime);
                }

            }
        }
    }
}