using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위해 필요

public class GameController : MonoBehaviour
{
    public SaveManager saveManager;
    //public StatisticsUI statisticsUI;

    // 게임 중에 저장하고 메인 메뉴로 이동하는 함수
    public void SaveAndReturnToMainMenu()
    {
        SaveData saveData = new SaveData();
        saveData.date = System.DateTime.Now.ToString("MM/dd");
        saveData.time = System.DateTime.Now.ToString("HH:mm");
        saveData.gold = Random.Range(0, 1000); // 임의의 골드 값

        saveManager.AddSave(saveData);
        SaveSystem.SaveGame(saveData, "save_" + saveManager.saveSlots.Count);

        // 통계 UI 업데이트
        //statisticsUI.UpdateStatisticsUI();

        // 메인 메뉴로 이동
        SceneManager.LoadScene("MainMenu");
    }
}
