using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class NextScriptBtn : MonoBehaviour, IShowNextScript
{
    public string[] selectionA;
    public string[] selectionB;

    LobbyScriptController lobbyManager => LobbyScriptController.Instance;
    public int count => lobbyManager.count;

    public void BtnClick()
    {
        //만약 로비매니저의 startSelectionNum과 count가 똑같을 때, 
        //선택지를 N개 보여준다. (SelectionNum과 동일한 수의 개수)
        //선택지에는 n의 정보를 담는다.

        lobbyManager.count++;

        if (lobbyManager.contentSave.Length > count)
        {
            Debug.Log("클릭");
            ShowNextScript();

            //만약 선택지 번호의 가장 최신 번호가 count와 동일하다면 실행
            if (lobbyManager.selectionNum == null) return;

            if (lobbyManager.selectionNum.Length <= lobbyManager.startSelectionNum && 
                lobbyManager.selectionNum[lobbyManager.startSelectionNum] == count)
            {
                Debug.Log("선택지 함수 진입");
                lobbyManager.SetSelectionContent(count);
                //lobbyManager.lobbyAnimation.ShowSelectionUI();
            }
            else
            {
                lobbyManager.lobbyAnimation.ShowSquareBtn();
            }
        }
        else
        {
            lobbyManager.lobbyAnimation.CloseCharacterDialogueScreen();
            lobbyManager.ClearStringArray();
        }

    }

    public void ShowNextScript()
    {
        lobbyManager.lobbyAnimation.SetContentAlphaZero();
        lobbyManager.SetDialogueContent(count);
        lobbyManager.lobbyAnimation.PlayContentAnimation();
    }

    
}
