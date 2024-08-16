using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OneColumnCSVParser : MonoBehaviour
{
    // CSV 파일 경로
    //public string filePath = "Assets/Resources/CSV/randomPool/Name.csv";
    //public List<string> filePathList;

    public List<TextAsset> files = new List<TextAsset>();

    public void LoadCSVs()
    {
        for (int i = 0; i < files.Count; i++)
        {
            LoadCSV(files[i]);
        }
    }

    void LoadCSV(TextAsset file)
    {
        // 파일이 존재하는지 확인
        if (file)
        {
            // CSV 파일을 읽어와서 각 라인을 배열로 분리
            //string[] csvLines = File.ReadAllLines(file);
            string[] csvLines = file.text.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);//'\n');


            if (csvLines.Length > 1)
            {
                // 첫 번째 라인은 헤더
                string header = csvLines[0];

                List<string> data = new List<string>();
                // 각 라인의 데이터를 읽어서 딕셔너리에 저장
                for (int i = 1; i < csvLines.Length; i++)
                {
                    data.Add(csvLines[i]);
                }
                    GameSceneManager.Instance.pool.randomPool.Add(header, data);
            }
            else
            {
                Debug.LogWarning("CSV 파일에 데이터가 없습니다: " + file);
            }
        }
        else
        {
            Debug.LogError("CSV 파일을 찾을 수 없습니다: " + file);
        }
    }
}
