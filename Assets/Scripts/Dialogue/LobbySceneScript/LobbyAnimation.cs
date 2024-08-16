using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyAnimation : MonoBehaviour
{

    [Header("대화화면 시 상하단 UI배경")]
    public Image blackBoxUITop;
    public Image blackBoxUIBottom;
    public Image dialogueMPCImage;

    [Header("text 애니메이션용")]
    public GameObject AllTextUI;
    private Button textUIBtn;
    public CanvasGroup nameCanvasGroup;
    public CanvasGroup contentCanvasGroup;
    public Image squareBtn;

    [Header("선택지 제작")]
    public CanvasGroup selection;

    LobbyScriptController lobbyManager => LobbyScriptController.Instance;

    private void Awake()
    {
        textUIBtn = AllTextUI.GetComponent<Button>();
    }

    private void Start()
    {
        SetTextGroupActiveFalse();
        SetSeiectionActive(false);
        selection.alpha = 0;
    }

    /// <summary>
    /// 대화화면을 처음 제작합니다.
    /// </summary>
    public void ShowCharacterDialogueScreen()
    {
        SetContentAlphaZero();

        blackBoxUITop.rectTransform.DOSizeDelta(new Vector2(0, 110), 1f).SetEase(Ease.OutExpo);
        blackBoxUIBottom.rectTransform.DOSizeDelta(new Vector2(0, 300), 1f).SetEase(Ease.OutExpo);

        //TODO : 캐릭터 일러스트 생성

        SetTextGroupActiveTrue();
        OnOffBtnClick(true);
        nameCanvasGroup.DOFade(1, 1f).SetEase(Ease.OutExpo).SetDelay(0.5f);
        contentCanvasGroup.DOFade(1, 1f).SetEase(Ease.OutExpo).SetDelay(0.5f);
    }

    /// <summary>
    /// 대화화면에서 종료합니다. 
    /// </summary>
    public void CloseCharacterDialogueScreen()
    {
        OnOffBtnClick(false);
        nameCanvasGroup.DOFade(0, 0.5f).SetEase(Ease.OutExpo).OnComplete(SetTextGroupActiveFalse);
        contentCanvasGroup.DOFade(0, 0.5f).SetEase(Ease.OutExpo);

        blackBoxUITop.rectTransform.DOSizeDelta(new Vector2(0, 60), 1f).SetEase(Ease.OutExpo).SetDelay(0.5f);
        blackBoxUIBottom.rectTransform.DOSizeDelta(new Vector2(0, 60), 1f).SetEase(Ease.OutExpo).SetDelay(0.5f);

        //TODO : 캐릭터 일러스트 close
    }

    /// <summary>
    /// 글씨를 보이지 않게 만듭니다.
    /// </summary>
    public void SetContentAlphaZero()
    {
        CloseSquareBtn();
        contentCanvasGroup.alpha = 0f;
    }

    /// <summary>
    /// 플레이어가 화면을 클릭할 경우 다음 글씨를 보여주는 애니메이션입니다.
    /// </summary>
    public void PlayContentAnimation()
    {
        contentCanvasGroup.DOFade(1, 1f).SetDelay(0.1f);
    }

    /// <summary>
    /// 선택지를 생성합니다.
    /// </summary>
    public void ShowSelectionUI()
    {
        SetSeiectionActive(true);
        selection.alpha = 0;
        selection.DOFade(1, 0.5f).SetEase(Ease.OutExpo);
    }

    /// <summary>
    /// 선택지를 보이지 않게 닫습니다.
    /// </summary>
    public void CloseSelectionUI()
    {
        SetSeiectionActive(false);
        selection.DOFade(0, 0.5f).SetEase(Ease.OutExpo);
    }

    public void ShowSquareBtn()
    {
        squareBtn.DOFade(1, 0.5f).SetEase(Ease.OutExpo).SetDelay(0.3f);
    }

    public void CloseSquareBtn()
    {
        squareBtn.DOFade(0, 0.1f).SetEase(Ease.OutExpo);
    }

    private void OnOffBtnClick(bool onOff) { textUIBtn.interactable = onOff; }

    private void SetSeiectionActive(bool onOff) { selection.interactable = onOff; }

    private void SetTextGroupActiveFalse() { AllTextUI.SetActive(false); }
    private void SetTextGroupActiveTrue() { AllTextUI.SetActive(true); }
}
