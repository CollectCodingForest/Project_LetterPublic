using DocumentInterface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParcelPermitData : ILegal
{
    public string Name;
    public string DateOfBirth;
    public string TrackingNumber; // 운송장 번호
    public string IssueDate;     // 발급일자
    public string ExpiryDate;    // 만료일자
    public bool OfficialSeal;  // 관리국 인장

    public ParcelPermitData() { }
    public ParcelPermitData(string name, string dateOfBirth, string trackinNumber, string issueDate, string expiryDate, bool officialSeal)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
        TrackingNumber = trackinNumber;
        IssueDate = issueDate;
        ExpiryDate = expiryDate;
        OfficialSeal = officialSeal;
    }

    public void OverrideRandomInfo()
    {
        System.Random random = new System.Random();
        int num = random.Next(0, 6);

        switch (num)
        {
            case 0:
                {
                    Name = RandomMethod.Name();
                    GameSceneManager.Instance.wrongPart.Add($"택배허가서. 이름 다름");

                    break;
                }
            case 1:
                {
                    DateOfBirth = RandomMethod.RRN();
                    GameSceneManager.Instance.wrongPart.Add($"택배허가서. 주민번호 다름");
                    break;
                }
            case 2:
                {
                    TrackingNumber = RandomMethod.TrackingNumber();
                    GameSceneManager.Instance.wrongPart.Add($"택배허가서. TrackingNumber 다름");
                    break;
                }
            case 3:
                {
                    IssueDate = RandomMethod.IssueDate();
                    GameSceneManager.Instance.wrongPart.Add($"택배허가서. IssueDate 다름");

                    break;
                }
            case 4:
                {
                    ExpiryDate = RandomMethod.ExpiryDate();
                    GameSceneManager.Instance.wrongPart.Add($"택배허가서. ExpiryDate 다름");

                    break;
                }
            case 5:
                {
                    OfficialSeal = RandomMethod.Bool();
                    GameSceneManager.Instance.wrongPart.Add($"택배허가서. OfficialSeal 다름");

                    break;
                }
        }
    }
}
public class ParcelPermit : Document // 택배물 허가서
{
    ParcelPermitData data;

    public string Name { get { return data.Name; } set { data.Name = value; } }          // 이름
    public string DateOfBirth { get { return data.DateOfBirth; } set { data.DateOfBirth = value; } }    // 생년월일
    public string TrackinNumber { get { return data.TrackingNumber; } set { data.TrackingNumber = value; } }   // 운송장 번호
    public string IssueDate { get { return data.IssueDate; } set { data.IssueDate = value; } }       // 발급일자
    public string ExpiryDate { get { return data.ExpiryDate; } set { data.ExpiryDate = value; } }      // 만료일자
    public bool OfficialSeal { get { return data.OfficialSeal; } set { data.OfficialSeal = value; } }    // 관리국 인장

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
        data = base.customerData.parcelPermitData;
        if (data == null) return;

        if (textElements == null || textElements.Length < 5)
        {
            Debug.Log("Text Elements 부족");
            return;
        }

        textElements[0].text = Name;
        textElements[1].text = DateOfBirth;
        textElements[2].text = TrackinNumber;
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