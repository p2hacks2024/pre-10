using UnityEngine;
using UnityEngine.SceneManagement;

public class clear3 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("sele(3 - 4)");//New Scene ��Scene�̖��O�ɏ���������
        }
    }
}
