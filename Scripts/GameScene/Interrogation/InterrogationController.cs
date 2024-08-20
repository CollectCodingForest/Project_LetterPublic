using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Microsoft.Win32.SafeHandles;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InterrogationController : MonoBehaviour
{
    // Model과 View를 컨트롤하는 클래스


    [HideInInspector]public InterrogationModel model;

    private List<ChatBubble> bubbleList = new List<ChatBubble>();
    
    public float leftright = 200f;
    public float downHeight = -150f;
    public float topHeight = 180f;

    [SerializeField]private int chatCount = 0;

    public float second;
    private IEnumerator myCoroutine;
    private bool isCoroutineStarted;
    private bool isStarted;

    [SerializeField] private InterrogationRecord interrogationRecord;

    [SerializeField]
    private Button interrogationBtn;
    private InterrogationCheck check;

    private void Start()
    {
        check = new InterrogationCheck();
        model = GetComponent<InterrogationModel>();
        myCoroutine = UpChatBubbleSecond(second);
        InitInterrogationBtn();
    }

    private void Update()
    {
        if (isStarted)
        {
            if (isCoroutineStarted)
            {
                StopCoroutine(myCoroutine);
                isCoroutineStarted = false;
            }

            myCoroutine = UpChatBubbleSecond(second);
            StartCoroutine(myCoroutine);
            isStarted = false;
            ClearModel();
        }
    }



    // 초기화
    public void InitialChatBubble(int key) 
    {
        if(chatCount != 0) { return; }
        // 파싱된 데이터에서 원하는 dialogueNum을 가져온다. TODO :: 팀원들한테 물어봐서 CSV의 dialogueNum 변수명 바꾸기.
        if (GameSceneCSV.GSdicData.ContainsKey(key))
        {
            // 데이터를 캐싱받아옴.
            Queue<CSVData> datas = GameSceneCSV.GSdicData[key];
            // 받아온 데이터를 model에 파싱해 세팅함.
            foreach(CSVData data in datas)
            {
                //                              오브젝트풀에서 가져옴                
                ChatBubble chatBubble = ObjectPoolManager.instance.Pool.Get();
                                                                                          
                chatBubble.IsNpc = (ConvertStringToBool(data.speakers));    // NPC 체크
                chatBubble.SetChatBubble(data.content); 
                model.chatBubbles.Enqueue(chatBubble);
                chatBubble.gameObject.SetActive(false);
                chatCount++;
            }
        }

        else
        {
            Debug.Assert(false, "찾는 key가 없습니다.");
        }
    }

    // 대사가 끝나면 model을 비움.
    public void ClearModel()
    {
        model.chatBubbles.Clear();
    }


    // 신청자가 오면 초기화를 위해 실행될 메서드 // TODO:: 팀원들한테 물어봐서 어울리는 메서드 이름 바꾸기.
    public void WelcomeComrade(int? customersKey = null)  
    {
        if(customersKey != null)
        {
            InitialChatBubble(customersKey.Value);
        }
        else
        {
            // 특수대사key가 null이면 틀린부분값을 받아와서 그에 맞는 대사를 넣어준다
            
        }
    }

    // 불일치 할 경우에 실행되는 메서드
    public void CheckObject(GameObject first, GameObject second)
    {
        // 이름일 경우
        if((first.name == "SenderNameTxt" && second.name == "NameTxt") || (first.name == "NameTxt" && second.name == "SenderNameTxt"))
        {
            int num = Random.Range(3000, 3003);
            InitialChatBubble(num);
            ActiveInterrogationBtn();
        }
        else if((first.name == "SenderAddressTxt" && second.name == "AddressTxt") || (first.name == "AddressTxt" && second.name == "SenderAddressTxt"))
        {
            int num = Random.Range(3010, 3013);
            InitialChatBubble(num);
            ActiveInterrogationBtn();
        }
        else
        {
            Debug.Assert(false, "이 경고가 발생하면 안됨.");
        }
    }
    public void CheckObjectType(TextObjectType type, bool regul)
    {
        int num = check.CheckObjectType(type, regul);
        InitialChatBubble(num);
        ActiveInterrogationBtn();
    }   

    public void ActiveInterrogationBtn()
    {
        if (interrogationBtn.interactable) return;
        else
        {
            interrogationBtn.interactable = true;
        }
    }
    public void InitInterrogationBtn()
    {
        if (interrogationBtn != null)
        {
            interrogationBtn.onClick.AddListener(BtnMethod);
        }
        interrogationBtn.interactable = false;
    }
    void BtnMethod()
    {
        StartCoroutine(AutoExe());
        interrogationBtn.interactable = false;
        // 이게 실행되면 몇초마다 자동으로 GetNextChatBubble 실행되야 한다.
    }

    IEnumerator AutoExe()
    {
        while (chatCount>0)
        {
            GetNextChatBubble();
            yield return new WaitForSeconds(1);
        }
    }

    // 디버그용 고객이 왔다고 치는 버튼. 인자도 넣어주었다.
    public void DebugBtnWellCome()
    {
        if(isCoroutineStarted)
        {
            StopCoroutine(myCoroutine);
            isCoroutineStarted = false;
        }
        WelcomeComrade(800);
    }
    public void Welcome(int dialogueNum)        
    {
        if (isCoroutineStarted)
        {
            StopCoroutine(myCoroutine);
            isCoroutineStarted = false;
        }
        WelcomeComrade(dialogueNum);
    }

    public void DebugBtnGetChatBubble()
    {
        if (chatCount > 0)
        {
            bubbleList.Add(GetChatBubble());
        }
        UpChatBubble();
    }
    public void GetNextChatBubble()
    {        
        if (chatCount > 0)
        {
            bubbleList.Add(GetChatBubble());
        }
        UpChatBubble();
    }
    public void DebugBtnChangePerson()
    {
        interrogationRecord.ClearRecord();
        ClearModel();
        bubbleList.Clear();
    }


    public bool ConvertStringToBool(string str)
    {
        if(str == "1")
        {
            return true;
        }
        else if(str == "0")
        {
            return false;
        }
        else
        {
            return false;
        }
    }




    // ChatBubble을 띄우기
    public ChatBubble GetChatBubble()
    {
        if (chatCount != 0)
        {
            ChatBubble chatBubble = model.chatBubbles.Dequeue();

            if (chatBubble.IsNpc)
            {
                // 오른쪽에 붙인다.
                chatBubble.transform.localPosition = new Vector2(leftright, downHeight);
                chatBubble.tmp.alignment = TMPro.TextAlignmentOptions.Right;
            }
            else
            {
                // 왼쪽에 붙인다.
                chatBubble.transform.localPosition = new Vector2(-leftright, downHeight);
                chatBubble.tmp.alignment = TMPro.TextAlignmentOptions.Left;
            }

            chatBubble.gameObject.SetActive(true);
            chatCount--;
            if (chatCount == 0)
            {
                isStarted = true;
            }

            interrogationRecord.AddRecord(chatBubble.tmp.text);
            return chatBubble;
        }
        else
        {
            Debug.Assert(false, $"chatCount: {chatCount}");
            
            return null;
        }
    }


    
    // 시간이 지나면 ChatBubble을 위로 올리기 (사라지게 하기)
    public void UpChatBubble()  // 뷰로 옮기기
    {
        float upHeight = bubbleList[bubbleList.Count - 1].BubbleImage.rectTransform.sizeDelta.y + 10f;
        for (int i = 0; i < bubbleList.Count; i++)
        {
            Vector3 currentPosition = bubbleList[i].transform.localPosition;
            Vector3 targetPosition = currentPosition + (Vector3.up * upHeight);

            if (bubbleList[i].transform.localPosition.y < topHeight)
            {
                //bubbleList[i].transform.localPosition += Vector3.up * upHeight;
                bubbleList[i].transform.DOLocalMove(targetPosition, 0.5f);
            }            
        }
        if (bubbleList[0].transform.localPosition.y > topHeight)
        {
            ReleaseChatBubble(bubbleList[0]);
        }
    }

    private void ReleaseChatBubble(ChatBubble chatBubble)
    {
        ObjectPoolManager.instance.Pool.Release(chatBubble);
        bubbleList.Remove(chatBubble);
    }


    public IEnumerator UpChatBubbleSecond(float second)
    {
        isCoroutineStarted = true;
        while (bubbleList.Count != 0 && chatCount == 0)
        {
            yield return new WaitForSeconds(second);
            UpChatBubble();
        }
        isCoroutineStarted = false;
    }

}