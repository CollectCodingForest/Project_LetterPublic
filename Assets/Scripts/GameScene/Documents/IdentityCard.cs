using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IdentityCard : Document
{
    IdentityCardData data;
    public string Name { get { return data.name; } set { data.name = value; } }      // 이름
    public string RRN { get { return data.RRN; } set { data.RRN = value; } }       // 주민등록번호
    public Sprite Photo { get; set; }     // 사진
    public string Address { get { return data.address; } set { data.address = value; } }   // 주소
    public string IssueDate { get { return data.issueDate; } set { data.issueDate = value; } } // 발급일자

    [Header("Text Elements")]
    public TMP_Text[] textElements;

    [Header("Sprite")]
    public Image photoImage;

    private void Awake()
    {        
        if (textElements == null)
        {
            textElements = new TMP_Text[4];
        }

        if (photoImage == null)
        {
            Debug.LogWarning("photoImage가 설정되지 않았습니다.");
        }
    }

    protected override void Start()
    {  
        base.Start();
        
    }


    protected override void UpdateUI()
    {
        customerData = GameSceneManager.Instance.currentCustomerData;
        data = customerData.identityCardData;
        if (data == null) return;

        if (textElements == null || textElements.Length < 4)
        {
            Debug.Log("Text Elements 부족");
            return;
        }
                
        textElements[0].text = Name;
        textElements[1].text = RRN;
        textElements[2].text = Address;
        textElements[3].text = IssueDate;

        //LoadPhoto();
    }

    private void LoadPhoto() // Resources 폴더에서 Name과 같은 이름의 파일 가져오기
    {
        if (!string.IsNullOrEmpty(Name))
        {
            //Photo = Resources.Load<Sprite>(Name);
            string imagePath = "PersonPhoto/" + Name;
            Photo = Resources.Load<Sprite>(imagePath);

            if (Photo != null)
            {
                photoImage.sprite = Photo;
            }
            else
            {
                Debug.LogWarning("이미지를 로드할 수 없습니다: " + Name);
            }
        }
        else
        {
            Debug.LogWarning("이름이 설정되지 않았습니다.");
        }
    }
}