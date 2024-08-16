using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public List<SaveData> saveSlots = new List<SaveData>();
    public GameObject saveSlotPrefab; // 저장 슬롯 프리팹
    public Transform contentTransform; // Scroll View의 Content Transform

    public void AddSave(SaveData newSave)
    {
        saveSlots.Add(newSave);
        SaveSystem.SaveGame(newSave, "save_" + saveSlots.Count);
        DisplaySaveSlots(); // 새로운 저장 슬롯을 반영하도록 UI를 업데이트합니다.
    }

    public void LoadSave(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < saveSlots.Count)
        {
            SaveData saveData = saveSlots[slotIndex];
            // saveData를 사용하여 게임 상태를 로드합니다.
        }
        else
        {
            Debug.LogError("Invalid save slot index");
        }
    }

    public void DeleteSave(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < saveSlots.Count)
        {
            saveSlots.RemoveAt(slotIndex);
            DisplaySaveSlots(); // 삭제된 저장 슬롯을 반영하도록 UI를 업데이트합니다.
        }
        else
        {
            Debug.LogError("Invalid save slot index");
        }
    }

    public void DisplaySaveSlots()
    {
        // 기존 슬롯 삭제
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        // 새로운 슬롯 추가
        for (int i = 0; i < saveSlots.Count; i++)
        {
            var saveData = saveSlots[i];
            GameObject slot = Instantiate(saveSlotPrefab, contentTransform);
            slot.transform.Find("DateText").GetComponent<Text>().text = saveData.date;
            slot.transform.Find("TimeText").GetComponent<Text>().text = saveData.time;
            slot.transform.Find("GoldText").GetComponent<Text>().text = saveData.gold.ToString();

            int index = i; // 변수 캡처 문제를 피하기 위해 별도의 변수 사용
            slot.GetComponent<Button>().onClick.AddListener(() => LoadSave(index));
            slot.transform.Find("DeleteButton").GetComponent<Button>().onClick.AddListener(() => DeleteSave(index));
        }
    }

    // 통계 정보를 계산하는 메서드
    public void DisplayStatistics()
    {
        int totalGold = saveSlots.Sum(slot => slot.gold);
        int saveCount = saveSlots.Count;
        float averageGold = saveCount > 0 ? totalGold / (float)saveCount : 0;

        Debug.Log($"Total Saves: {saveCount}");
        Debug.Log($"Total Gold: {totalGold}");
        Debug.Log($"Average Gold per Save: {averageGold:F2}");
    }

    private void Start()
    {
        DisplaySaveSlots(); // 게임 시작 시 저장 슬롯 UI를 표시합니다.
    }
}
