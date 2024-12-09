using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;


public class make_objectr : MonoBehaviour
{
    public GameObject Cube; // Inspector �p
    public float realtime;
    public float spawnInterval = 1.0f;
    public float elapsedTime = 0;
    void Start()
    {
        realtime = Time.time;
        // prefab �����蓖�Ă��Ă��Ȃ��ꍇ�A���O�Ō������Ċ��蓖��
        if (Cube == null)
        {
            Cube = Resources.Load<GameObject>("PrefabName"); // "PrefabName" �����ۂ̃v���n�u���ɒu������
            if (Cube == null)
            {
                Debug.LogError("Prefab ��������܂���I 'PrefabName' ���m�F���Ă��������B");
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
            Debug.Log("�o�ߎ��ԁi�b�j" + Time.deltaTime);
        Debug.Log("elapsedTime" + elapsedTime);
        
        

    }
}
