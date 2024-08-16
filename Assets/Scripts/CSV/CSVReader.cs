using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.U2D;

public class CSVData
{
    public int dialogueNum;
    public int id;
    public string speakers;
    public string content;

    public CSVData()
    { }
    public CSVData(int _dialogueNum, int _id, string _speakers, string _content)
    {
        dialogueNum = _dialogueNum;
        id = _id;
        speakers = _speakers;
        content = _content;
    }
}

public class DialogueCSVData
{
    public int dialogueNum;
    public int id;
    public string speakers;
    public string content;

    public DialogueCSVData()
    { }
    public DialogueCSVData(int _dialogueNum, int _id, string _speakers, string _content)
    {
        dialogueNum = _dialogueNum;
        id = _id;
        speakers = _speakers;
        content = _content;
    }
}
public class SelectionCSVData
{
    public int dialogueNum;
    public int startId;
    public string content;
    public string nextId;

    public SelectionCSVData()
    { }
    public SelectionCSVData(int _dialogueNum, int _startId, string _content, string _nextId)
    {
        dialogueNum = _dialogueNum;
        startId = _startId;
        content = _content;
        nextId = _nextId;
    }
}

public class CSVReader : MonoBehaviour
{
    // 리팩토링 해야할 코드입니다.

    public TextAsset dialogueCsvFile;
    public TextAsset SelectionCsvFile;
    public TextAsset DayMPCCsvFile;

    //타 코드에서 문제가 생겨서 이전버젼 유지해놨습니다.
    public Queue<CSVData> csvDataQueue = new Queue<CSVData>();
    public static Dictionary<int, Queue<CSVData>> dicData = new Dictionary<int, Queue<CSVData>>();

    //public Queue<DialogueCSVData> DcsvDataQueue = new Queue<DialogueCSVData>();
    public static Dictionary<int, Queue<DialogueCSVData>> DdicData = new Dictionary<int, Queue<DialogueCSVData>>();

    //public Queue<SelectionCSVData> ScsvDataQueue = new Queue<SelectionCSVData>();
    public static Dictionary<int, Queue<SelectionCSVData>> SdicData = new Dictionary<int, Queue<SelectionCSVData>>();

    string[] csvLine;
    string[] splitData;


    private void Start()
    {
        GetDialogueCSV();
        GetSelectionCSV();
    }
    private void OnDisable()
    {
        DdicData.Clear();
        SdicData.Clear();
    }

    //다이얼로그 csv를 읽어옵니다.
    private void GetDialogueCSV()
    {
        csvLine = dialogueCsvFile.text.Split('\n');

        for (int i = 1; i < csvLine.Length; ++i)
        {
            if (string.IsNullOrWhiteSpace(csvLine[i]))
                continue;

            splitData = csvLine[i].Split(',');

            DialogueCSVData data = new DialogueCSVData
            {
                dialogueNum = int.Parse(splitData[0]),
                id = int.Parse(splitData[1]),
                speakers = splitData[2],
                content = splitData[3]
            };

            //>>대사창에 쓰여있는 쉼표로 인해 대사가 끝까지 들어오지 않아 추가
            if (splitData.Length > 4)
            {
                for (int j = 4; j < splitData.Length; ++j)
                {
                    data.content = data.content + "," + splitData[j];
                }
            }

            if (!DdicData.ContainsKey(data.dialogueNum))
            {
                DdicData[data.dialogueNum] = new Queue<DialogueCSVData>();
            }

            DdicData[data.dialogueNum].Enqueue(data);
        }
    }
    private void GetSelectionCSV()
    {
        csvLine = SelectionCsvFile.text.Split('\n');

        for (int i = 1; i < csvLine.Length; ++i)
        {
            if (string.IsNullOrWhiteSpace(csvLine[i]))
                continue;

            splitData = csvLine[i].Split(',');

            SelectionCSVData data = new SelectionCSVData
            {
                dialogueNum = int.Parse(splitData[0]),
                startId = int.Parse(splitData[1]),
                content = splitData[2],
                nextId = splitData[3]
            };

            if (!SdicData.ContainsKey(data.dialogueNum))
            {
                SdicData[data.dialogueNum] = new Queue<SelectionCSVData>();
            }

            SdicData[data.dialogueNum].Enqueue(data);
        }
    }

    public string[] FindDialogueScriptData(int keyNum)
    {
        string[] strings = new string[DdicData[keyNum].Count];
        foreach (var data in DdicData[keyNum])
        {
            strings[data.id] = data.speakers + "\t" + data.content;
        }
        return strings;
    }

    public string[] FindSelectionScriptData(int keyNum)
    {
        if (!SdicData.ContainsKey(keyNum)) 
            return null;

        string[] strings = new string[SdicData[keyNum].Count];
        int count = 0;
        if (SdicData.ContainsKey(keyNum))
        {
            foreach (var data in SdicData[keyNum])
            {
                strings[count] = data.startId + "\t" + data.content + "\t" + data.nextId;
                count++;
            }
            return strings;
        }
        return null;
    }
}
