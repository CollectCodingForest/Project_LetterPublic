using TMPro;
using UnityEngine;
// 클릭가능한 객체
public class SampleObject : MonoBehaviour
{
    public TextMeshProUGUI tmp;    // tmp객체
    public bool isRight;
    public void SetTmpText(string str)
    {
        tmp.text = str;
    }
}