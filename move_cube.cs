using UnityEngine;

public class move_cube : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        // Rigidbody �R���|�[�l���g���A�^�b�`����Ă��邩�m�F
         rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            // Rigidbody �����݂��Ȃ��ꍇ�A�V�����ǉ�
            rb = gameObject.AddComponent<Rigidbody>();
            Debug.Log("Rigidbody ���ǉ�����܂����B");
        }
        else
        {
            Debug.Log("Rigidbody �͊��ɑ��݂��Ă��܂��B");
        }

        // �K�v�ɉ����� Rigidbody �̐ݒ��ύX
        rb.useGravity = false; // �d�͂�L���ɂ���
        rb.mass = 1.0f;       // ���ʂ�ݒ�
    }
    void Update()
    {
        this.rb.linearVelocity = new Vector3(10, 0, 0);
    }
}
