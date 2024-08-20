using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public void LoadScene(string sceneName) // 씬 이름 문자열로 받아서 해당 씬 로드
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnMainSceneBtn() //특정 씬 로드
    {
        AudioManager.Instance.PlayClickSound();
        LoadScene("MainScene");
    }

    public void OnSaveLoadSceneBtn()
    {
        AudioManager.Instance.PlayClickSound();
        LoadScene("SaveLoadScene");
    }

    public void OnLobbySceneBtn()
    {
        AudioManager.Instance.PlayClickSound();
        LoadScene("LobbyScene");
    }

    public void OnGameSceneBtn()
    {
        AudioManager.Instance.PlayClickSound();
        LoadScene("GameScene");
    }

    public void OnCreditSceneBtn()
    {
        AudioManager.Instance.PlayClickSound();
        LoadScene("CreditScene");
    }
}
