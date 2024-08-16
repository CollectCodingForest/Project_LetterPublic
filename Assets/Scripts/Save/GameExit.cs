using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    public GameObject ExitWindow;

    public void GameExitBtn() // 게임 종료
    {
        AudioManager.Instance.PlayClickSound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ExitGameWindow() // 게임 종료 버튼 클릭
    {
        AudioManager.Instance.PlayClickSound();
        ExitWindow.SetActive(true);
    }

    public void CancelExitGame() // 취소
    {
        AudioManager.Instance.PlayClickSound();
        ExitWindow.SetActive(false);
    }
}
