using UnityEngine;
using UnityEngine.UI;

public class LobbySceneMPC : MonoBehaviour
{
    public int characterNum;

    LobbyScriptController lobbyManager => LobbyScriptController.Instance;

    public void OnBtn()
    {
        lobbyManager.GetDialogueResource(characterNum);
        lobbyManager.SetDialogueContent(0);
        lobbyManager.lobbyAnimation.ShowCharacterDialogueScreen();
        lobbyManager.selectionNum = lobbyManager.FindSelectionNum();
    }
}
