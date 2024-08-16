using TMPro;
using UnityEngine;
using UnityEngine.UI;
//시간대를 가리키는 enum, am은 게임 플레이 전, pm은 게임 플레이 이후 입니다.
public enum DateTimeInLobby
{
    AM = 0,
    PM = 1000
}

public class LobbyScriptController : MonoBehaviour
{
    private static LobbyScriptController _instance;
    public static LobbyScriptController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("AnimationControl").AddComponent<LobbyScriptController>();

            }
            return _instance;
        }
    }
    private CSVReader cSVReader;
    public LobbyAnimation lobbyAnimation;

    [SerializeField]
    public int date;
    public DateTimeInLobby datetime;

    [Header("대화화면 Text")]
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI characterContent;
    public int count;

    [Header("받아온 대사 확인용")]
    public string[] contentSave;
    public string[] selectionSave;

    [Header("선택지 번호 정리")]
    public int[] selectionNum;
    public int startSelectionNum;

    [Header("선택지 생성")]
    public GameObject selectionA;
    public GameObject selectionB;
    public TextMeshProUGUI selectionATxt;
    public TextMeshProUGUI selectionBTxt;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance == this)
            {
                Destroy(gameObject);
            }
        }

        lobbyAnimation = GetComponent<LobbyAnimation>();
        cSVReader = GetComponent<CSVReader>();
    }

    private void Start()
    {
        //임시로 날짜를 1일차, AM으로 지정했습니다. 나중에 통일해서 관리
        date = GameManager.Instance.date;
        datetime = GameManager.Instance.datetime;

        if(datetime == DateTimeInLobby.AM)
        {
            btnGoHome.gameObject.SetActive(false);
            btnWork.gameObject.SetActive(true);
        }
        else if(datetime == DateTimeInLobby.PM) 
        {
            btnGoHome.gameObject.SetActive(true);
            btnWork.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 다이얼로그를 읽어와서 보관합니다.
    /// </summary>
    public void GetDialogueResource(int CharacterNum)
    {
        contentSave = cSVReader.FindDialogueScriptData(CharacterNum + date + (int)datetime + 1);
        selectionSave = cSVReader.FindSelectionScriptData(CharacterNum + date + (int)datetime+ 1);
    }

    /// <summary>
    /// 화면의 글씨를 전달한 번호에 맞춰서 변경합니다.
    /// </summary>
    /// <param name="num"></param>
    public void SetDialogueContent(int num)
    {
        string[] splitString = contentSave[num].Split('\t');
        characterName.text = splitString[0];
        characterContent.text = splitString[1].Replace("\"", "");
    }

    /// <summary>
    /// 진행하는 대화 섹션에서 존재하는 선택지 번호를 찾습니다.
    /// </summary>
    public int[] FindSelectionNum()
    {
        if(selectionSave != null)
        {
            string newNum = null;
            int[] num = new int[selectionSave.Length];

            for(int i = 0; i < selectionSave.Length; i++)
            {
                newNum = selectionSave[i].Split("\t")[0];
                num[i] = int.Parse(newNum);
            }
            return num;
        }
        return null;
    }

    /// <summary>
    /// 화면의 글씨를 전달할 선택지 번호에 맞춰서 변경합니다.
    /// </summary>
    /// <param name="num"></param>
    public void SetSelectionContent(int num)
    {
        if (selectionNum[startSelectionNum] == num)
        {
            string[] split = selectionSave[startSelectionNum].Split("\t");
            selectionATxt.text = split[1];
            selectionA.GetComponent<SelectionBtn>().nextNum = int.Parse(split[2]);
            startSelectionNum++;
            if (selectionNum.Length <= startSelectionNum && selectionNum[startSelectionNum] == num)
            {
                split = selectionSave[startSelectionNum].Split("\t");
                selectionBTxt.text = split[1];
                selectionB.GetComponent<SelectionBtn>().nextNum = int.Parse(split[2]);
                startSelectionNum++;
            }
        }
    }

    /// <summary>
    /// 세이브해둔 정보를 초기화합니다.
    /// </summary>
    public void ClearStringArray()
    {
        contentSave = null;
        selectionSave = null;
        selectionNum = null;
        startSelectionNum = 0;
        count = 0;
    }

    /// <summary>
    /// 로비에 맞는 캐릭터 일러스트를 가져와서 배치합니다.
    /// </summary>
    private void GetResource()
    {
        //TODO : DateInfo CSV 확인해서 맵에 있는 MPC확인, 날짜와 시간에 맞게 맵에 배치
    }

    public Button btnWork;
    public Button btnGoHome;

    public void BtnGotoHome()
    {
        GameManager.Instance.NextDay();
        btnGoHome.gameObject.SetActive(false);
        btnWork.gameObject.SetActive(true);

        date = GameManager.Instance.date;
        datetime = GameManager.Instance.datetime;
        // 내일 로비씬 로드...?
        // 그냥 잠시 암전? 후 로비 그대로 쓰기
    }
}
