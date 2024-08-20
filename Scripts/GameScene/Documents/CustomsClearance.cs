using DocumentInterface;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CustomsClearanceData : ILegal
{
    public string name;
    public string DateOfBirth;
    public string CustomsNumber;
    public string IssueDate;
    public string ExpiryDate;
    public bool OfficialSeal;

    public CustomsClearanceData() { }
    public CustomsClearanceData(string name, string dateOfBirth, string customsNumber, string issueDate, string expiryDate, bool officialSeal)
    {
        this.name = name;
        DateOfBirth = dateOfBirth;
        CustomsNumber = customsNumber;
        IssueDate = issueDate;
        ExpiryDate = expiryDate;
        OfficialSeal = officialSeal;
    }

    public void OverrideRandomInfo()
    {
        System.Random random = new System.Random();
        int num = random.Next(0, 6);
        string reason;

        switch (num)
        {
            case 0:
                {
                    name = RandomMethod.Name();
                    reason = "Name";
                    break;
                }
            case 1:
                {
                    DateOfBirth = RandomMethod.RRN();
                    reason = "DateOfBirth";

                    break;
                }
            case 2:
                {
                    CustomsNumber = RandomMethod.CustomsNumber();
                    reason = "CustomsNumber";

                    break;
                }
            case 3:
                {
                    IssueDate = RandomMethod.IssueDate();
                    reason = "IssueDate";

                    break;
                }
            case 4:
                {
                    ExpiryDate = RandomMethod.ExpiryDate();
                    reason = "ExpiryDate";

                    break;
                }
            case 5:
                {
                    OfficialSeal = RandomMethod.Bool();
                    reason = "OfficialSeal";

                    break;
                }
            default:
                reason = "Unknown";
                break;
        }
        GameSceneManager.Instance.wrongPart.Add($"{this.GetType().Name}. {reason} 다름");
    }
}

public class CustomsClearance : Document // 통관 허가증
{
    CustomsClearanceData data;
    public string Name { get { return data.name; } set { data.name = value; } }          // 이름
    public string DateOfBirth { get { return data.DateOfBirth; } set { data.DateOfBirth = value; } }    // 생년월일
    public string CustomsNumber { get { return data.CustomsNumber; } set { data.CustomsNumber = value; } }  // 통관 번호
    public string IssueDate { get { return data.IssueDate; } set { data.IssueDate = value; } }     // 발급일자
    public string ExpiryDate { get { return data.ExpiryDate; } set { data.ExpiryDate = value; } }     // 만료일자
    public bool OfficialSeal { get { return data.OfficialSeal; } set { data.OfficialSeal = value; } }   // 관리국 인장(true: OfficialSeal 이미지, false: fakeSeal 이미지)

    [Header("Text Elements")]
    public TMP_Text[] textElements;

    [Header("Image")]
    public Image sealImage;

    private void Awake()
    {
        if (textElements == null)
        {
            textElements = new TMP_Text[5];
        }

        if (sealImage == null)
        {
            Debug.LogWarning("sealImage가 설정되지 않았습니다.");
        }
    }

    
    protected override void UpdateUI()
    {
        base.customerData = GameSceneManager.Instance.currentCustomerData;
        data = base.customerData.customsClearanceData;
        if (data == null) return;

        if (textElements == null || textElements.Length < 5)
        {
            Debug.Log("Text Elements 부족");
            return;
        }

        textElements[0].text = Name;
        textElements[1].text = DateOfBirth;
        textElements[2].text = CustomsNumber;
        textElements[3].text = IssueDate;
        textElements[4].text = ExpiryDate;

        //LoadPhoto();
    }

    private void LoadPhoto() // Resources 폴더에서 Name과 같은 이름의 파일 가져오기
    {
        string imageName = OfficialSeal ? "OfficialSeal" : "fakeSeal";
        string imagePath = "Seal/" + imageName;
        Sprite loadSprite = Resources.Load<Sprite>(imagePath);

        if (loadSprite != null)
        {
            sealImage.sprite = loadSprite;
        }
        else
        {
            Debug.LogWarning("이미지를 로드할 수 없습니다: " + imagePath);
        }
    }

}