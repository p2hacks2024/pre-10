using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class JudgementEffect : MonoBehaviour
{
    //effect�̕����̕ύX:

    [SerializeField] Text text;

    public void setText(string message)
    {
        text.text = message;
    }
}
