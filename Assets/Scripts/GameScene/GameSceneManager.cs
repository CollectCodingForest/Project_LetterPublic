using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    private static GameSceneManager s_instance;
    public static GameSceneManager Instance
    {
        get { return s_instance; }
    }

    // 제어만 하자?
    public EventSubject eventSubject;
    //[SerializeField] private int _howManyComeToday;
    public DayBlueprint blueprint;
    //public List<DayBlueprint> blueprintStorage; // 여기에 블루프린트를 저장한다.
    public RandomPool pool;
    public Vector3 targetPosition;
    public float dropSpeed = 1100f;

    [Header("Current")]
    public PlayData playData;
    public CustomerData currentCustomerData { get; private set; }
    public List<string> wrongPart = new List<string>();
    public List<GameObject> currentDocuments = new List<GameObject>();
    private int currentDocumentsCount = 0;
    public int CurrentDocumentsCount
    {
        get
        {
            return currentDocumentsCount;
        }
        set
        {
            currentDocumentsCount = value;
            if (currentDocumentsCount == 0)
            {
                BehaviourEvaluation();
                StartCoroutine(EndCustomer());
            }
        }
    }

    public bool isStamp;   // 플레이어가 도장을 찍었는지를 bool로 반환. 도장을 찍었으면 true 아니면 false. 
    public int currentNotAllowed = 0;   // 값이 양수면 유저가 신청를 거부했다고 판단했다는것. 거부도장찍기에서 ++함.
    public bool IsGrant   // 허가시켰는가. 허가도장은 0. -> true상태. 거부도장은 1++. -> false상태
    {
        get
        {
            if (currentNotAllowed > 0) return false;
            else if (currentNotAllowed == 0) return true;
            else return false;
        }
    }

    [Header("Down NeedCaching")]
    [Header("Document")]
    public IdentityCard IdentityCard;
    public BasicInvoice basicInvoice;
    public Parcel Parcel;
    public CustomsClearance customsClearance;
    public ParcelApplication parcelApplication;
    public ParcelPermit parcelPermit;

    [Header("SubDocument")]
    public GameObject subBasicInvoice;
    private SubObjectEvent subBasicInvoiceObjectEvent;
    public GameObject subIdentityCard;
    public GameObject subParcel;
    public GameObject subCustomsClearance;
    public GameObject subParcelApplication;
    public GameObject subParcelPermit;

    [Header("NeedCaching")]
    public InterrogationController interrogationController; // 인스펙터 캐싱
    public Transform unUseDocumentPosition;
    public Transform documentAppearPosition;
    public DropZone dropZone;
    public CutInvoice cutInvoice;
    public StampAnimation stampA;
    public StampAnimation stampB;
    public Transform mainDesk;
    public OneColumnCSVParser oneColumnCSVParser;

    [Header("Button")]
    public Button lobbyBtn;
    public Button nextBtn;


    protected void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }

        eventSubject = new EventSubject();
        pool = new RandomPool();
        
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        oneColumnCSVParser = GetComponent<OneColumnCSVParser>();
        oneColumnCSVParser.LoadCSVs();
        subBasicInvoiceObjectEvent = subBasicInvoice.GetComponent<SubObjectEvent>();
        targetPosition = documentAppearPosition.position + (Vector3.down * 100f);
        lobbyBtn.onClick.AddListener(() => BtnMoveLobby());
        lobbyBtn.gameObject.SetActive(false);
        playData = GameManager.Instance.playData;

        Initialize();
    }


    void Initialize()
    {
        playData.ResetToday(); // 플레이 전 기록 초기화

        blueprint = GameManager.Instance.blueprintStorage[GameManager.Instance.date];
        blueprint.Initialize();
    }

    public void BtncomeNewCustomer()
    {
        NextCustomer();
    }

    void NextCustomer()
    {
        EndCustomer();
        if (currentCustomerData != null) return;

        if (blueprint.datas.Count > blueprint.index)
        {
            // 베이직인보이스 붙이기
            if (cutInvoice.synkInvoice.IsCut)
            {
                cutInvoice.ResetInvoice();
            }
            if (!subBasicInvoiceObjectEvent.subDeskObject.activeSelf)
            {
                subBasicInvoiceObjectEvent.subDeskObject.SetActive(true);
            }
            stampA.DestroyStamp();
            stampB.DestroyStamp();

            currentCustomerData = blueprint.GetNextData();
            StartCoroutine(PleasePaper());
            playData.Customer += 1;
        }
        else
        {
            Debug.Log($"오늘 신청자가 더 없어요. 끝");
            EndDay();
        }
    }

    //public void DebugBtnToggleIdentityCard()
    //{
    //    if (IdentityCard.gameObject.activeSelf)
    //    {
    //        //IdentityCard.UpdateUI();
    //        //basicInvoice.UpdateUI();
    //        //// 택배도 활성화하기
    //    }
    //    else
    //    {
    //        IdentityCard.gameObject.SetActive(true);
    //        basicInvoice.gameObject.SetActive(true);
    //    }
    //}

    IEnumerator PleasePaper()
    {
        if (IdentityCard.gameObject.activeSelf)
        {
            eventSubject.EventUpdateUI();
        }
        else
        {
            IdentityCard.gameObject.SetActive(true);
            basicInvoice.gameObject.SetActive(true);
        }

        DoYouHaveDocument(currentCustomerData.hasDocuments);

        // 서류를 서브데스크에 내는 애니메이션
        foreach (GameObject item in currentDocuments)
        {
            // TODO::애니메이션 동안 item 마우스선택 불가능하게 하기
            item.transform.position = documentAppearPosition.position;
            item.transform.DOMove(targetPosition, 0.4f, false);
        }
        yield return new WaitForSeconds(0.6f);  // 서류 다 놓을때까지 기다리기

        yield return null;
    }

    void DoYouHaveDocument(BindingDocumentsFlags hasDocuments) //currentCustomer.hasDocuments
    {
        // 메서드기능: 비트플래그에 따라서 나와야할 서류를 판단해 그오브젝트를 가리키게 임시저장하는 리스트에 추가한다. 
        //TODO:: 서류추가
        if (hasDocuments.HasFlag(BindingDocumentsFlags.IdentityCard))
        {
            currentDocuments.Add(subIdentityCard.gameObject);
            currentDocumentsCount++;
        }
        if (hasDocuments.HasFlag(BindingDocumentsFlags.BasicInvoice))
        {
            currentDocuments.Add(subBasicInvoice.gameObject);
            currentDocumentsCount++;
        }
        if (hasDocuments.HasFlag(BindingDocumentsFlags.Parcel))
        {
            currentDocuments.Add(subParcel.gameObject);
            subParcel.GetComponent<SubObjectEvent>().subDeskObject.SetActive(true);
            currentDocumentsCount++;
        }
        if (hasDocuments.HasFlag(BindingDocumentsFlags.CustomsClearance))
        {
            currentDocuments.Add(subCustomsClearance.gameObject);
            currentDocumentsCount++;
        }
        if (hasDocuments.HasFlag(BindingDocumentsFlags.ParcelApplication))
        {
            currentDocuments.Add(subParcelApplication.gameObject);
            currentDocumentsCount++;
        }
        if (hasDocuments.HasFlag(BindingDocumentsFlags.ParcelPermit))
        {
            currentDocuments.Add(subParcelPermit.gameObject);
            currentDocumentsCount++;
        }
    }

    void BehaviourEvaluation()
    {
        if (IsGrant)
        {
            playData.Allow += 1;
        }
        else
        {
            playData.Refuse += 1;
        }

        // 신청자1명 처리결과평가 //TODO:: 한 신청자 사이클 끝에서 실행하기
        if (IsGrant == currentCustomerData.isLegal)       // if isLegal false
        {
            // TODO:: 성공 대가 실행            
            playData.Success += 1;
        }
        else
        {
            playData.Failed += 1;
            eventSubject.EventCounter();
            // TODO: 뭐가 틀렸는지 알려주기.
            eventSubject.EventFailAlarm();
        }
    }

    public void Judged()
    {
        if (isStamp) return;
        else isStamp = true;
    }

    IEnumerator EndCustomer()
    {
        yield return new WaitForSeconds(0.3f);
        // 한 신청자가 끝날 때 실행될 메서드. 정리 및 초기화 목적
        // 신청자가 가지고 있던서류의 처리가 모두 끝나면 신청자는 물러나고, 결과평가를 한다. 
        currentCustomerData = null;
        currentDocuments.Clear();
        currentDocumentsCount = 0;
        isStamp = false;
        currentNotAllowed = 0;
        dropZone.collectZone.SetActive(false);
        eventSubject.EventEndCustomer();
        basicInvoice.RemoveSticker();

        Debug.Log("Customer is gone");
    }

    void EndDay()
    {
        // 하루 종료. GameScene을 종료시키는 메서드
        //playData.ResetToday(); // 결과 화면 출력 후 리셋
        wrongPart.Clear();
        blueprint.ThisDayEnd();

        lobbyBtn.gameObject.SetActive(true);
        nextBtn.gameObject.SetActive(false);
    }

    public string targetSceneName = "LobbyScene";

    public void LoadTargetScene()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        SceneManager.LoadScene(targetSceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == targetSceneName)
        {
            // 실행될 코드
            GameManager.Instance.datetime = DateTimeInLobby.PM;

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void BtnMoveLobby()
    {
        LoadTargetScene();
    }
      
}