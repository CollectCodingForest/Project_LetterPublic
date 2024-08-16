using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    public SaveManager saveManager;
    public GameObject saveSlotPrefab;
    public Transform saveSlotContainer;

    void Start()
    {
        UpdateSaveSlotsUI();
    }

    void UpdateSaveSlotsUI()
    {
        // 기존 저장 슬롯 UI 요소를 제거합니다.
        foreach (Transform child in saveSlotContainer)
        {
            Destroy(child.gameObject);
        }

        // 각 저장 슬롯에 대한 새 UI 요소를 만듭니다.
        foreach (SaveData saveData in saveManager.saveSlots)
        {
            GameObject saveSlot = Instantiate(saveSlotPrefab, saveSlotContainer);
            saveSlot.GetComponent<SaveSlot>().Setup(saveData);
        }
    }
}

public class SaveSlot : MonoBehaviour
{
    public Text dateText;
    public Text timeText;
    public Text goldText;

    public void Setup(SaveData saveData)
    {
        dateText.text = saveData.date;
        timeText.text = saveData.time;
        goldText.text = saveData.gold.ToString() + " G";
    }
}
