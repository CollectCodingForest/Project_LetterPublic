using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RegulationBookController : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    [Header("TextPrefab")]
    public GameObject titleTextPrefab;
    public GameObject contentTextPrefab;

    [Header("PagePanel")]
    public Transform leftPageContent;
    public Transform rightPageContent;

    [Header("Button")]
    public Button prevBtn;
    public Button nextBtn;

    [Header("Image")]
    public Image pageImage; // 페이지 이미지
    public Sprite coverImage; // 표지 이미지
    public Sprite innerPageImage; // 내부 페이지 이미지

    private SubObjectEvent subRegulationBook;
    public static bool isDraggingHandled = false;
    private RegulationManager regulationManager;
    private List<Regulation> regulations; // 페이지 리스트
    private int currentPage;

    void Start()
    {
        subRegulationBook = GetComponent<MainObjectEvent>().subObj;
        regulationManager = new RegulationManager();
        regulations = regulationManager.GetRegulations();

        currentPage = 0;

        prevBtn.onClick.AddListener(PrevPage);
        nextBtn.onClick.AddListener(NextPage);

        UpdatePage();
    }

    public void NextPage()
    {
        if ((currentPage + 1) * 2 < regulations.Count)
        {
            currentPage++;
            UpdatePage();
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
    }

    void UpdatePage()
    {
        // 페이지 이미지 설정
        if (currentPage == 0)
        {
            pageImage.sprite = coverImage;
        }
        else
        {
            pageImage.sprite = innerPageImage;
        }

        // 왼쪽 페이지 업데이트
        foreach (Transform child in leftPageContent)
        {
            Destroy(child.gameObject);
        }
        if (currentPage * 2 < regulations.Count)
        {
            DisplayRegulation(leftPageContent, regulations[currentPage * 2]);
        }

        // 오른쪽 페이지 업데이트
        foreach (Transform child in rightPageContent)
        {
            Destroy(child.gameObject);
        }
        if (currentPage * 2 + 1 < regulations.Count)
        {
            DisplayRegulation(rightPageContent, regulations[currentPage * 2 + 1]);
        }

        // 버튼 활성화/비활성화
        prevBtn.gameObject.SetActive(currentPage > 0);
        nextBtn.gameObject.SetActive((currentPage + 1) * 2 < regulations.Count);
    }

    void DisplayRegulation(Transform pageContent, Regulation regulation)
    {
        // 타이틀 생성
        var titleObj = Instantiate(titleTextPrefab, pageContent);
        var titleText = titleObj.GetComponent<TMP_Text>();
        titleText.text = regulation.Title;

        // 최대 8개의 contentTextPrefab만 생성되도록 제한
        int maxContents = Mathf.Min(8, regulation.Texts.Count);
        for (int i = 0; i < maxContents; i++)
        {
            var content = regulation.Texts[i];
            var contentObj = Instantiate(contentTextPrefab, pageContent);
            var contentText = contentObj.GetComponent<TMP_Text>();
            contentText.text = content.Text;

            // ClickableDocument 컴포넌트 추가 및 objectType 설정
            var clickable = contentObj.AddComponent<ClickableDocument>();
            clickable.objectType = content.ObjectType;
        }
    }

    public void AddRegulation(string title, List<RegulationText> texts) // 새 규정 추가
    {
        var newRegulation = new Regulation(title, texts);
        regulationManager.AddRegulation(newRegulation);
        regulations = regulationManager.GetRegulations(); // 리스트 업데이트
    }

    public void InsertRegulation(int index, string title, List<RegulationText> texts) // 해당 인덱스 자리에 규정 추가
    {
        var newRegulation = new Regulation(title, texts);
        regulationManager.InsertRegulation(index, newRegulation);
        regulations = regulationManager.GetRegulations(); // 리스트 업데이트
    }

    public void UpdateRegulation(int index, string title, List<RegulationText> texts) // 해당 인덱스 규정 수정
    {
        if (index >= 0 && index < regulations.Count)
        {
            regulations[index] = new Regulation(title, texts);
        }
    }

    public void RemoveRegulation(int index) // 해당 인덱스 규정 삭제
    {
        regulationManager.RemoveRegulation(index);
        regulations = regulationManager.GetRegulations(); // 리스트 업데이트
    }

    public void ReturnRegulationPosition()
    {
        subRegulationBook.transform.localPosition = new Vector3(185f, -530f, 0f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraggingHandled = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(FindMousePlace.instance.inSubDesk)
        {
            ReturnRegulationPosition();
        }
        isDraggingHandled = false;
    }
}