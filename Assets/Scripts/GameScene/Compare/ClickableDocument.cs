using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableDocument : MonoBehaviour, IPointerClickHandler
{
    public TextObjectType objectType;

    [HideInInspector]
    public CompareManager documentComparison;

    private void Awake()
    {
        // 부모 객체에서 CompareManager 컴포넌트를 찾아 설정
        documentComparison = GetComponentInParent<CompareManager>();
        if (documentComparison == null)
        {
            Debug.LogWarning("CompareManager 컴포넌트를 찾을 수 없습니다: " + gameObject.name);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TMP_Text textComponent = GetComponent<TMP_Text>();
        if (textComponent != null)
        {
            Debug.Log("텍스트 클릭됨: " + textComponent.text);
            documentComparison.OnDocumentClicked(textComponent, objectType);
        }
        else
        {
            Debug.LogWarning("TMP_Text 컴포넌트를 찾을 수 없습니다: " + gameObject.name);
        }
    }
}
