using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompareManager : MonoBehaviour
{
    [Header("GameManager")]
    public GameManager gameManager;

    [Header("Canvas_Interrogation")]
    public Button InterrogationButton; // 심문 버튼

    [Header("DebugBtnCanvas")]
    public Button compareButton; // 비교 버튼
    public TMP_Text compareButtonText; // 비교 버튼 텍스트

    [Header("CompareObject")]
    public GameObject darkBackground; // 비교 시 어두운 배경
    public GameObject firstHighlightPanel; // 첫 번째 클릭된 텍스트를 밝게 표시하는 패널
    public TMP_Text firstHighlightText; // 첫 번째 하이라이트 패널의 텍스트
    public GameObject secondHighlightPanel; // 두 번째 클릭된 텍스트를 밝게 표시하는 패널
    public TMP_Text secondHighlightText; // 두 번째 하이라이트 패널의 텍스트
    public LineRenderer lineRendererStart; // 라인 렌더러
    public LineRenderer lineRendererEnd;
    public GameObject resultTextBox; // 결과 텍스트 박스
    public TMP_Text resultText; // 결과 텍스트 박스의 텍스트

    [Header("FindMousePlace")]
    public GameObject findMousePlaceObject; // FindMousePlace 오브젝트

    [Header("MainDesk_Object")]
    public TMP_Text todayDateText; // 오늘 날짜 텍스트

    private string firstText;    // 첫 번째 선택한 텍스트
    private string secondText;   // 두 번째 선택한 텍스트
    private GameObject clickedDocument; //클릭된 서류 오브젝트
    private GameObject firstDocument; // 첫 번째 선택한 서류 오브젝트
    private TMP_Text firstTextObject; // 첫 번째 선택한 텍스트 오브젝트
    private TMP_Text secondTextObject; // 두 번째 선택한 텍스트 오브젝트
    private bool isSelecting;    // 비교 기능 활성화 상태
    private TextObjectType firstTextType; // 첫 번째 선택한 텍스트의 타입
    private TextObjectType secondTextType; // 두 번째 선택한 텍스트의 타입
    private bool isRegulationBook; // 규정집을 포함하고 있는지
    private string resultMessage;
    private DateTime todayDate; // 오늘 날짜

    void Start()
    {
        gameManager = GameManager.Instance;
        compareButton.onClick.AddListener(StartComparison);

        Initialize();
    }

    void Initialize()
    {
        // 초기 값
        compareButtonText.text = "비교";
        darkBackground.SetActive(false);
        firstHighlightPanel.SetActive(false);
        secondHighlightPanel.SetActive(false);
        lineRendererStart.gameObject.SetActive(false);
        lineRendererEnd.gameObject.SetActive(false);
        resultTextBox.SetActive(false);
        InterrogationButton.gameObject.SetActive(false);

        // 오늘 날짜 계산
        DateTime baseDate = new DateTime(2010, 9, 1); // 기준일
        todayDate = baseDate.AddDays(gameManager.date); // 게임의 현재 날짜
        todayDateText.text = $"{todayDate:yy.MM.dd.}"; // 날짜를 포맷에 맞춰 표시
    }

    void StartComparison()
    {
        if (compareButtonText.text == "닫기")
        {
            darkBackground.SetActive(false);
            firstHighlightPanel.SetActive(false);
            secondHighlightPanel.SetActive(false);
            lineRendererStart.gameObject.SetActive(false);
            lineRendererEnd.gameObject.SetActive(false);
            resultTextBox.SetActive(false);
            compareButtonText.text = "비교";

            // FindMousePlace 컴포넌트 활성화
            if (findMousePlaceObject != null)
            {
                findMousePlaceObject.SetActive(true);
            }

            return;
        }

        if (!isSelecting)
        {
            isSelecting = true;
            firstText = "";
            secondText = "";
            firstDocument = null;
            firstTextObject = null;
            secondTextObject = null;

            darkBackground.SetActive(true);
            firstHighlightPanel.SetActive(false);
            secondHighlightPanel.SetActive(false);
            lineRendererStart.gameObject.SetActive(false);
            lineRendererEnd.gameObject.SetActive(false);
            resultTextBox.SetActive(false);

            compareButtonText.text = "취소";
            Debug.Log($"비교 시작 - isSelecting: {isSelecting}, firstText: {firstText}, secondText: {secondText}");

            // FindMousePlace 컴포넌트 비활성화
            if (findMousePlaceObject != null)
            {
                findMousePlaceObject.SetActive(false);
            }
        }
        else
        {
            isSelecting = false;
            darkBackground.SetActive(false);
            firstHighlightPanel.SetActive(false);
            secondHighlightPanel.SetActive(false);
            lineRendererStart.gameObject.SetActive(false);
            lineRendererEnd.gameObject.SetActive(false);
            resultTextBox.SetActive(false);

            compareButtonText.text = "비교";
            Debug.Log($"비교 취소 - isSelecting: {isSelecting}");

            // FindMousePlace 컴포넌트 활성화
            if (findMousePlaceObject != null)
            {
                findMousePlaceObject.SetActive(true);
            }
        }
    }

    public void OnDocumentClicked(TMP_Text text, TextObjectType objectType)
    {
        if (!isSelecting) return;

        // 텍스트의 부모의 부모 가져오기(서류 오브젝트)
        Transform clickedDocumentParent = text.transform.parent.parent;

        if (clickedDocumentParent != null)
        {
            clickedDocument = clickedDocumentParent.gameObject;

            if (firstDocument == null)
            {
                firstText = text.text;
                firstDocument = clickedDocument;
                firstTextObject = text;
                firstTextType = objectType;
                HighlightText(text, true, true);
                Debug.Log($"첫 번째 문서 선택 - isSelecting: {isSelecting}, firstText: {firstText}, firstTextType: {firstTextType}, firstDocument: {firstDocument.name}");
                Debug.Log($"firstTextObject : {firstTextObject}, {firstTextObject.name}");
            }
            else if (clickedDocument != firstDocument)
            {
                secondText = text.text;
                secondTextObject = text;
                secondTextType = objectType;
                HighlightText(text, true, false);
                CompareDocuments();
                isSelecting = false;

                Debug.Log($"두 번째 문서 선택 - isSelecting: {isSelecting}, secondText: {secondText}, secondTextType: {secondTextType}, secondDocument: {clickedDocument.name}");
                Debug.Log($"secondTextObject : {secondTextObject}, {secondTextObject.name}");
            }
            else
            {
                Debug.LogWarning("같은 문서를 다시 선택할 수 없습니다.");
            }
        }
    }

    void CompareDocuments()
    {
        //만료일자 비교

        if ((firstTextType == TextObjectType.Today || secondTextType == TextObjectType.Today) &&
           (firstTextType == TextObjectType.ExpiryDate || secondTextType == TextObjectType.ExpiryDate ||
            firstTextType == TextObjectType.CC_ExpiryDate || secondTextType == TextObjectType.CC_ExpiryDate ||
            firstTextType == TextObjectType.PP_ExpiryDate || secondTextType == TextObjectType.PP_ExpiryDate))
        {
            DateTime expiryDate;
            if (firstTextType == TextObjectType.Today)
            {
                expiryDate = ParseExpiryDate(secondText); // 두 번째 서류 텍스트 파싱하여 저장
            }
            else
            {
                expiryDate = ParseExpiryDate(firstText); // 첫 번째 서류 텍스트 파싱하여 저장
            }

            if (todayDate > expiryDate) // 만료일이 지난 경우
            {
                HandleComparisonResult(1, true);
                return;
            }
            else // 지나지 않은 경우
            {
                HandleComparisonResult(0, false);
                return;
            }
        }

        // 발급일자 비교
        if ((firstTextType == TextObjectType.Today || secondTextType == TextObjectType.Today) &&
            (firstTextType == TextObjectType.IssueDate || secondTextType == TextObjectType.IssueDate ||
            firstTextType == TextObjectType.CC_IssueDate || secondTextType == TextObjectType.CC_IssueDate ||
            firstTextType == TextObjectType.PP_IssueDate || secondTextType == TextObjectType.PP_IssueDate))
        {
            if (firstDocument.name == "RegulationCanvas" || clickedDocument.name == "RegulationCanvas")
            {
                HandleComparisonResult(2, false);
                return;
            }

            DateTime issueDate;

            if (firstTextType == TextObjectType.Today)
            {
                issueDate = ParseIssueDate(secondText); // 두 번째 서류 텍스트 파싱하여 저장
            }
            else
            {
                issueDate = ParseIssueDate(firstText); // 첫 번째 서류 텍스트 파싱하여 저장
            }

            if (todayDate < issueDate) // 발급일이 현재 날짜보다 최신인 경우
            {
                HandleComparisonResult(1, true);
                return;
            }
            else // 최신이 아닌 경우
            {
                HandleComparisonResult(0, false);
                return;
            }
        }

        // 타입 비교
        if (firstTextType == secondTextType)
        {
            if (firstText == secondText)
            {
                HandleComparisonResult(0, false);
                return;
            }
            else
            {
                HandleComparisonResult(1, true);
                return;
            }
        }
        else
        {
            HandleComparisonResult(2, false);
            return;
        }
    }

    void HandleComparisonResult(int result, bool isDataMismatch) // 결과 처리 메서드
    {


        switch (result)
        {
            case 0:
                resultMessage = "데이터 일치";
                break;
            case 1:
                resultMessage = "데이터 불일치";
                break;
            case 2:
                resultMessage = "연관 없음";
                break;
        }

        Debug.Log(resultMessage);
        StartCoroutine(DrawLineBetweenHighlights(resultMessage));

        if (isDataMismatch)
        {
            if (firstDocument.name == "RegulationCanvas" || clickedDocument.name == "RegulationCanvas")
            {
                isRegulationBook = true;
            }
            else
            {
                isRegulationBook = false;
            }

            GameSceneManager.Instance.interrogationController.CheckObjectType(firstTextType, isRegulationBook);
            InterrogationButton.gameObject.SetActive(true);
        }
        else
        {
            InterrogationButton.gameObject.SetActive(false);
        }

        compareButtonText.text = "닫기";
    }

    DateTime ParseExpiryDate(string expiryDateString) // 날짜 문자열을 DateTime으로 변환
    {
        DateTime expiryDate;
        if (DateTime.TryParseExact(expiryDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out expiryDate))
        {
            return expiryDate;
        }
        else
        {
            Debug.LogError("날짜 변환 실패: " + expiryDateString);
            return DateTime.MinValue;
        }
    }

    DateTime ParseIssueDate(string issueDateString) // 발급일자 문자열을 DateTime으로 변환
    {
        DateTime issueDate;
        if (DateTime.TryParseExact(issueDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out issueDate))
        {
            return issueDate;
        }
        else
        {
            Debug.LogError("발급일자 변환 실패: " + issueDateString);
            return DateTime.MinValue;
        }
    }

    private void HighlightText(TMP_Text text, bool highlight, bool isFirst)
    {
        if (highlight)
        {
            // 클릭된 텍스트의 위치와 크기를 계산하여 하이라이트 패널을 이동 및 크기 조절
            RectTransform textRectTransform = text.GetComponent<RectTransform>();
            RectTransform highlightRectTransform;
            TMP_Text highlightTMP_Text;

            if (isFirst)
            {
                highlightRectTransform = firstHighlightPanel.GetComponent<RectTransform>();
                highlightTMP_Text = firstHighlightText;
            }
            else
            {
                highlightRectTransform = secondHighlightPanel.GetComponent<RectTransform>();
                highlightTMP_Text = secondHighlightText;
            }

            // 위치와 크기 설정
            highlightRectTransform.position = textRectTransform.position;
            highlightRectTransform.sizeDelta = textRectTransform.sizeDelta;

            // 피벗 값 설정
            highlightRectTransform.pivot = textRectTransform.pivot;

            // 텍스트 내용 복사
            highlightTMP_Text.text = text.text;
            highlightTMP_Text.fontSize = text.fontSize;
            highlightTMP_Text.color = text.color;
            highlightTMP_Text.font = text.font;
            highlightTMP_Text.alignment = text.alignment;
            highlightTMP_Text.rectTransform.pivot = textRectTransform.pivot;
            highlightTMP_Text.rectTransform.position = textRectTransform.position;
            highlightTMP_Text.rectTransform.sizeDelta = textRectTransform.sizeDelta;

            if (isFirst)
                firstHighlightPanel.SetActive(true); // 첫 번째 하이라이트 패널 활성화
            else
                secondHighlightPanel.SetActive(true); // 두 번째 하이라이트 패널 활성화
        }
        else
        {
            if (isFirst)
                firstHighlightPanel.SetActive(false); // 첫 번째 하이라이트 패널 비활성화
            else
                secondHighlightPanel.SetActive(false); // 두 번째 하이라이트 패널 비활성화
        }
    }

    private IEnumerator DrawLineBetweenHighlights(string resultTextString)
    {
        Vector3 startPos1 = firstHighlightPanel.transform.position;
        Vector3 startPos2 = secondHighlightPanel.transform.position;

        // 두 하이라이트 패널의 중앙 지점 계산
        Vector3 midPoint = (startPos1 + startPos2) / 2;

        // 두 개의 LineRenderer 설정
        lineRendererStart.positionCount = 2;
        lineRendererEnd.positionCount = 2;

        // 선의 굵기 설정
        lineRendererStart.startWidth = 10f;
        lineRendererStart.endWidth = 10f;
        lineRendererEnd.startWidth = 10f;
        lineRendererEnd.endWidth = 10f;

        // LineRenderer 활성화
        lineRendererStart.gameObject.SetActive(true);
        lineRendererEnd.gameObject.SetActive(true);

        float duration = 0.5f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            // 첫 번째 선: 하이라이트 패널 1에서 텍스트 박스까지
            lineRendererStart.SetPosition(0, startPos1);
            lineRendererStart.SetPosition(1, Vector3.Lerp(startPos1, midPoint, t));

            // 두 번째 선: 하이라이트 패널 2에서 텍스트 박스까지
            lineRendererEnd.SetPosition(0, startPos2);
            lineRendererEnd.SetPosition(1, Vector3.Lerp(startPos2, midPoint, t));

            yield return null;
        }

        // 선의 최종 위치 설정
        lineRendererStart.SetPosition(1, midPoint);
        lineRendererEnd.SetPosition(1, midPoint);

        // 결과 텍스트 박스 위치 설정
        resultTextBox.transform.position = midPoint;
        resultText.text = resultTextString;
        resultTextBox.SetActive(true);
    }
}
