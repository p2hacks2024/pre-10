using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class JudgementEffect : MonoBehaviour
{
    //effect‚Ì•¶š‚Ì•ÏX:

    [SerializeField] Text text;

    public void setText(string message)
    {
        text.text = message;
    }
}
