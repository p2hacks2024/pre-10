using UnityEngine;
using UnityEngine.UI;

public class JudgementEffect : MonoBehaviour
{
    //effectの文字の変更
    [SerializeField] Text text;

    public void setText(string message)
    {
        text.text = message;
    }

}
