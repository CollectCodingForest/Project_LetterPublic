using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [Header("OptionWindow")]
    public GameObject OptionWindow;
    public TMP_Text TitleText;

    [Header("DisplayBtn")]
    public Button FullScreenButton;
    public Button WindowedButton;

    [Header("SceneBtn")]
    public Button BackButton;
    public Button MainButton;

    //private bool isOptionWindowActive = false;
    private Color selectedColor = new Color(129f / 255f, 107f / 255f, 82f / 255f); // 816B52 색상
    private Color defaultColor = Color.white; // 기본 색상 FFFFFF

    private void Start()
    {
        OptionWindow.SetActive(false); // 기본 비활성화

        int screenMode = PlayerPrefs.GetInt("ScreenMode", 0); // 기본값 창모드
        if (screenMode == 1)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
        else
        {
            Screen.SetResolution(1920, 1080, false);
        }

        LoadSceneInfo();
        ApplyInitialButtonColors(screenMode == 1);
    }

    /* GameManager에 작성하기 - 현재는 OptionCanvas가 기본 비활성화 상태여서 사용하기 힘듦
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionWindow();
        }
    }

    private void ToggleOptionWindow() // Esc키로 옵션창 활성화/비활성화 하기
    {
        isOptionWindowActive = !isOptionWindowActive;
        OptionWindow.SetActive(isOptionWindowActive);
        //Time.timeScale = isOptionWindowActive ? 0f : 1f; // 활성화 시 게임 일시정지
    }
    */

    void LoadSceneInfo()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "MainScene" || sceneName == "CreditScene")
        {
            TitleText.text = "설정";
            BackButton.transform.localPosition = new Vector3(0, -340, 0);
            MainButton.gameObject.SetActive(false);
        }
        else
        {
            TitleText.text = "일시정지";
            BackButton.transform.localPosition = new Vector3(230, -340, 0);
            MainButton.gameObject.SetActive(true);
        }
    }

    public void OnClickFullScreenMode()
    {
        AudioManager.Instance.PlayClickSound();
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        PlayerPrefs.SetInt("ScreenMode", 1); // 전체화면 모드 저장

        UpdateButtonColors(true); // 버튼 색상 업데이트
    }

    public void OnClickWindowMode()
    {
        AudioManager.Instance.PlayClickSound();
        int width = 1920;
        int height = 1080;

        // 모니터 해상도를 확인하여 적절한 해상도로 설정
        if (width > Screen.currentResolution.width || height > Screen.currentResolution.height)
        {
            width = Screen.currentResolution.width;
            height = Screen.currentResolution.height;
        }

        Screen.SetResolution(width, height, false);
        PlayerPrefs.SetInt("ScreenMode", 0); // 창모드 저장

        UpdateButtonColors(false); // 버튼 색상 업데이트
    }

    private void ApplyInitialButtonColors(bool isFullScreenMode)
    {
        SetButtonColor(FullScreenButton, isFullScreenMode ? selectedColor : defaultColor);
        SetButtonColor(WindowedButton, isFullScreenMode ? defaultColor : selectedColor);
    }

    private void UpdateButtonColors(bool isFullScreenMode)
    {
        SetButtonColor(FullScreenButton, isFullScreenMode ? selectedColor : defaultColor);
        SetButtonColor(WindowedButton, isFullScreenMode ? defaultColor : selectedColor);

        // 버튼을 일시적으로 비활성화하고 다시 활성화하여 UI 갱신
        FullScreenButton.interactable = false;
        WindowedButton.interactable = false;
        FullScreenButton.interactable = true;
        WindowedButton.interactable = true;
    }

    private void SetButtonColor(Button button, Color color)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = color;
        colorBlock.selectedColor = color;
        colorBlock.highlightedColor = color;
        colorBlock.pressedColor = color;
        colorBlock.disabledColor = color;
        button.colors = colorBlock;
    }

    public void OptionWindowBtn() // 옵션창 활성화
    {
        AudioManager.Instance.PlayClickSound();
        OptionWindow.SetActive(true);
        AudioManager.Instance.InitializeSliders(); // 슬라이더 초기화

        Time.timeScale = 0f;
    }

    public void CancelOptionWindow() // 취소
    {
        AudioManager.Instance.PlayClickSound();
        OptionWindow.SetActive(false);

        Time.timeScale = 1f;
    }
}
