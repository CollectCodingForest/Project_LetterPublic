using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public SaveData currentSaveData;
    [SerializeField]public PlayData playData = new PlayData();
    public List<DayBlueprint> blueprintStorage = new List<DayBlueprint>(); // 여기에 블루프린트를 저장한다.
    

    [SerializeField]
    public int date;
    public DateTimeInLobby datetime = DateTimeInLobby.AM;

    public List<Regulation> savedRegulations = new List<Regulation>(); // 규정집 내용 리스트

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Initialize();
    }


    void Initialize()
    {
        date = 0;
        datetime = DateTimeInLobby.AM;        
    }

    public void NextDay()
    {
        date++;
        datetime = DateTimeInLobby.AM;
    }
}
