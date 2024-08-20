using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneCSV : MonoBehaviour
{
    // 리팩토링 해야할 코드입니다.

    public TextAsset csvFile;
    public Queue<CSVData> csvDataQueue = new Queue<CSVData>();
    public static Dictionary<int, Queue<CSVData>> GSdicData = new Dictionary<int, Queue<CSVData>>();

    string[] csvLine;
    string[] splitData;


    private IEnumerator Start()
    {
        csvLine = csvFile.text.Split('\n');

        for (int i = 1; i < csvLine.Length; ++i)
        {
            if (string.IsNullOrWhiteSpace(csvLine[i]))
                continue;

            splitData = csvLine[i].Split(',');

            CSVData data = new CSVData
            {
                dialogueNum = int.Parse(splitData[0]),
                id = int.Parse(splitData[1]),
                speakers = splitData[2],
                content = splitData[3]
            };

            ////>>대사창에 쓰여있는 쉼표로 인해 대사가 끝까지 들어오지 않아 추가
            //if (splitData.Length > 4)
            //{
            //    for (int j = 4; j < splitData.Length; ++j)
            //    {
            //        data.content = data.content + "," + splitData[j];
            //    }
            //}

            if (!GSdicData.ContainsKey(data.dialogueNum))
            {
                GSdicData[data.dialogueNum] = new Queue<CSVData>();
            }

            GSdicData[data.dialogueNum].Enqueue(data);
        }

        yield return null;
    }
}
