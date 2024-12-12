using Unity.VisualScripting;
using UnityEngine;

// Noteを生成する
// Prefab:Instantiate

public class NoetGenerator : MonoBehaviour
{
    public Vector3 initPositionNote;
    [SerializeField] Note notePrefab;
   

    

    public void SpawnNote()
    {
        //Instantiate(生成したいもの, 場所, 角度);
        // Quaternion.identityは角度の決まり文句、角度の初期値
        Instantiate(notePrefab, initPositionNote, Quaternion.identity);

    }

    private void Start()
    {
        initPositionNote = new Vector3(0, 0, 0);
        SpawnNote();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnNote();
        }
    }

}
