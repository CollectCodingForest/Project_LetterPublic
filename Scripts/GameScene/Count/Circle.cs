using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour
{
    [SerializeField] private Text circleText;
    private bool isFilled = false;

    public bool IsFilled => isFilled; // 현재 원이 채워졌는지 여부를 확인하는 속성

    private void Start()
    {
        circleText.text = "○";
    }

    public void IncrementCount()
    {
        if (!isFilled)
        {
            FillCircle();
        }
    }

    private void FillCircle()
    {
        circleText.text = "●";
        circleText.color = Color.red;
        isFilled = true;
    }        
}
