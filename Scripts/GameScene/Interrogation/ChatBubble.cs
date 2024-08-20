using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class ChatBubble : MonoBehaviour 
{
    // 말풍선 객체, view, Prefab

    public Image BubbleImage;   // 말풍선. 에디터에서 설정
    public TextMeshProUGUI tmp;    // tmp객체
    public StringBuilder sb = new StringBuilder();    // 대사가 들어갈 객체
    public bool IsNpc;   // 플레이어는 false, NPC는 true

    public float padding = 5f; // 텍스트와 말풍선 가장자리 사이의 패딩

    //public IObjectPool<GameObject> Pool { get; set; }
    
    public void SetChatBubble(string str)
    {
        UtilSB.ClearText(tmp, sb);
        SetDefaultBubbleSize();
        UtilSB.SetText(tmp, sb, str);
        AdjustBubblePivot();
        AdjustBubbleSize();
    }
    public void ClearChatBubble()
    {
        UtilSB.ClearText(tmp, sb);
    }

    private void SetDefaultBubbleSize()
    {
        BubbleImage.rectTransform.sizeDelta = new Vector2(395f, 10f);//BubbleImage.rectTransform.sizeDelta.y);
    }
    // 말풍선 사이즈를 텍스트에 맞게 조정
    private void AdjustBubbleSize()
    {
        // 텍스트의 바운드(경계)를 계산
        tmp.ForceMeshUpdate();  // 텍스트의 메쉬를 업데이트하여 정확한 크기를 계산
        Vector2 textSize = tmp.GetRenderedValues(false);    //렌더링된 텍스트의 크기를 가져옴.

        // 말풍선의 크기를 텍스트 크기와 패딩을 반영하여 조정
        BubbleImage.rectTransform.sizeDelta = new Vector2(textSize.x + padding * 2f, textSize.y + padding * 2f);
    }

    // 말풍선의 피봇을 조정
    public void AdjustBubblePivot()
    {
        if (IsNpc)
        {
            BubbleImage.rectTransform.pivot = new Vector2(1f, 0.5f);
        }
        else
        {
            BubbleImage.rectTransform.pivot = new Vector2(0f, 0.5f);
        }
    }
}
