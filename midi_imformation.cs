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
    public class midi_imfomation : MonoBehaviour
    {
        [SerializeField] private MidiAnimationAsset _asset;
        int[] data_43 = new int[500];//ノートナンバー４３番の音のノートオンの時のticksを格納
        float[] data_43_realtime = new float[500];//tickをリアルタイムに直したものを格納
        int[] data_38 = new int[500];//ノートナンバー３８番の音のノートオンの時のticksを格納
        float[] data_38_realtime = new float[500];//tickをリアルタイムに直したものを格納
        int[] data_36 = new int[500];//ノートナンバー３６番の音のノートオンの時のticksを格納
        float[] data_36_realtime = new float[500];//tickをリアルタイムに直したものを格納
        int temp=100;//曲のテンポ
        float count_time = 0;//ノーツを生成する時間を管理するための時間
        int count_43 = 0;//ノートナンバー４３の信号の数を記録
        int count_38 = 0;//ノートナンバー３８の信号の数を記録
        int count_36 = 0;//ノートナンバー３６の信号の数を記録
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
                    if (midiEvent.data1 == 43)//曲によって変える必要がある
                    {
                        data_43[count_43] = (int)midiEvent.time;
                        data_43_realtime[count_43] = (float)midiEvent.time/(temp*8); 
                        count_43++;           
                    }
                    if (midiEvent.data1 == 38)//曲によって変える必要がある
                    {
                        data_38[count_38] = (int)midiEvent.time;
                        data_38_realtime[count_38] = (float)midiEvent.time / (temp * 8);
                        count_38++;
                    }
                    if (midiEvent.data1 == 36)//曲によって変える必要がある
                    {
                        data_36[count_36] = (int)midiEvent.time;
                        data_36_realtime[count_36] = (float)midiEvent.time / (temp * 8);
                        count_36++;
                    }
                    //ノーツナンバー確認用
                   // Debug.Log(midiEvent.ToString());
                }
            }
            Debug.Log(count_38);
            for (int i = 0; i < count_38; i++)
            {
                spawn_prefab[i] = Random.Range(1,3);//個数がオブジェクトの個数が3つだから今回1～３にした。
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
            for (int i = 0; i < count_38; i++)
            {
                if (count_time > data_38_realtime[i] -3.5&& count_time < data_38_realtime[i]-3.5+Time.deltaTime)
                    /*タイミング調整のために-3.5をつけている。Time.deltaTimeを足すことにより
                    二つ以上のオブジェクトの生成を防ぐ。
                    */
                    //好きなものを使うと良い
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
            if (Input.GetKeyDown(KeyCode.Space))//scoreが一定を超えたらの条件分岐
            {

            }
             */
            
        }
    }
}