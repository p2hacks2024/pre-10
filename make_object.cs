using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;


public class make_objectr : MonoBehaviour
{
    public GameObject Cube; // Inspector 用
    public float realtime;
    public float spawnInterval = 1.0f;
    public float elapsedTime = 0;
    void Start()
    {
        realtime = Time.time;
        // prefab が割り当てられていない場合、名前で検索して割り当て
        if (Cube == null)
        {
            Cube = Resources.Load<GameObject>("PrefabName"); // "PrefabName" を実際のプレハブ名に置き換え
            if (Cube == null)
            {
                Debug.LogError("Prefab が見つかりません！ 'PrefabName' を確認してください。");
            }
        }
    }
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= spawnInterval)
        {
            elapsedTime = 0;
            Instantiate(Cube, Vector3.zero, Quaternion.identity);
        }
            Debug.Log("経過時間（秒）" + Time.deltaTime);
        Debug.Log("elapsedTime" + elapsedTime);
        
        

    }
}
