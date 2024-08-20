[System.Serializable]
public class SaveData
{
    public string date; // 날짜
    public string time; // 시간
    public int gold; // 골드
    // 필요한 다른 게임 상태 정보를 여기에 추가하세요.

    public int totalEarnGold;  // 게임 플레이 중 벌은 돈
    public int totalCustomer;   // 맞이한 전체 신청자
    public int totalAllow;
    public int totalRefuse;
    public int totalSuccess;
    public int totalFailed;
    public int totalFineCount;
    public int totalFine;

    public float playTime;      //유저가 이 세이브데이타를 플레이한 시간
}
