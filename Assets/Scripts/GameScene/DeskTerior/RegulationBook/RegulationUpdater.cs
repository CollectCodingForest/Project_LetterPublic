using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegulationUpdater : MonoBehaviour
{
    public GameManager gameManager;
    public RegulationBookController regulationBookController;

    private int currentDay = 0;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        int day = gameManager.date;
        if (day != currentDay)
        {
            currentDay = day;
            UpdateRegulations(currentDay);
        }
    }

    private void UpdateRegulations(int day)
    {
        switch (day)
        {
            case 1:
                //Day1AddRegulation();
                //Day1UpdateRegulation();
                break;
            case 2:
                //Day2UpdateRegulation();
                break;
            default:
                break;
        }
    }

    private void Day1AddRegulation()
    {
        var newRegulationTexts = new List<RegulationText>
        {
            new RegulationText("2일차에 추가된 규정입니다.", TextObjectType.Regulation)
        };

        regulationBookController.AddRegulation("Day1 규정 추가", newRegulationTexts);
    }

    private void Day1UpdateRegulation()
    {
        var newRegulationTexts = new List<RegulationText>
        {
            new RegulationText("2일차에 수정된 규정입니다.", TextObjectType.Regulation),
            new RegulationText("2일차에 수정된 규정입니다!", TextObjectType.Regulation)
        };

        regulationBookController.UpdateRegulation(7, "Day1 규정 수정", newRegulationTexts);
    }

    private void Day2UpdateRegulation()
    {
        var newRegulationTexts = new List<RegulationText>
        {
            new RegulationText("3일차에 수정된 규정입니다.", TextObjectType.Regulation),
            new RegulationText("3일차에 수정된 규정입니다!", TextObjectType.Regulation),
            new RegulationText("3일차에 수정된 규정입니다~", TextObjectType.Regulation)
        };

        regulationBookController.UpdateRegulation(7, "Day2 규정 수정", newRegulationTexts);
    }




}
