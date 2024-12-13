using UnityEngine;

public class move_cube : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        // Rigidbody コンポーネントがアタッチされているか確認
         rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            // Rigidbody が存在しない場合、新しく追加
            rb = gameObject.AddComponent<Rigidbody>();
            Debug.Log("Rigidbody が追加されました。");
        }
        else
        {
            Debug.Log("Rigidbody は既に存在しています。");
        }

        // 必要に応じて Rigidbody の設定を変更
        rb.useGravity = false; // 重力を有効にする
        rb.mass = 1.0f;       // 質量を設定
    }
    void Update()
    {
        this.rb.linearVelocity = new Vector3(10, 0, 0);
    }
}
