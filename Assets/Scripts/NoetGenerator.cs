using Unity.VisualScripting;
using UnityEngine;

// Note�𐶐�����
// Prefab:Instantiate

public class NoetGenerator : MonoBehaviour
{
    public Vector3 initPositionNote;
    [SerializeField] Note notePrefab;
   

    

    public void SpawnNote()
    {
        //Instantiate(��������������, �ꏊ, �p�x);
        // Quaternion.identity�͊p�x�̌��܂蕶��A�p�x�̏����l
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
