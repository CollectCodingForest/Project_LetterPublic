using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterSO",  menuName = "ScriptableObject/LobbyScene/ChracterSO", order = 0)]
public class LobbySceneSO : ScriptableObject
{
    [Header("캐릭터 정보")]
    public string characterName;
    [Tooltip("A~F 까지의 백의자리 고유 번호, A는 100, F는 600")]
    public int characterNum;

    [Space (10f)]
    public Sprite lobbyImage;
    public Sprite dialogueImage;

}

