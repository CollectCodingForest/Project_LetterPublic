using System.Collections.Generic;
using DocumentInterface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ParcelApplicationData : ILegal
{
    public string customsNumber;       // 통관 번호
    public string trackingNumber;       // 운송장 번호
    public float Weight;               // 중량
    public PersonInfo Recipient;       // 수취인
    public string ApplicationDate;     // 신청일자
    public List<string> ParcelContents; // 택배 내용물
    public List<int> Quantity;          // 택배 수량
    public ReceiptReason Purpose;       // 택배 목적, 배송 이유
    public string PersonalUseReason;    // 기타 배송 이유, ReceiptReason이 PersonalUse일 때 사용
    public bool OfficialSeal;         // 관리국 인장

    public ParcelApplicationData() { }
    public ParcelApplicationData(string customsNumber, string trackingNumber, float weight, PersonInfo recipient, string applicationDate, List<string> parcelContents, List<int> quantity, ReceiptReason purpose, string personalUseReason, bool officialSeal)
    {
        this.customsNumber = customsNumber;
        this.trackingNumber = trackingNumber;
        Weight = weight;
        Recipient = recipient;
        ApplicationDate = applicationDate;
        ParcelContents = parcelContents;
        Quantity = quantity;
        Purpose = purpose;
        PersonalUseReason = personalUseReason;
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
                    customsNumber = RandomMethod.CustomsNumber();
                    GameSceneManager.Instance.wrongPart.Add($"ParcelApplication. CustomsNumber 다름");

                    break;
                }
            case 1:
                {
                    trackingNumber = RandomMethod.TrackingNumber();
                    GameSceneManager.Instance.wrongPart.Add($"ParcelApplication. TrackingNumber 다름");

                    break;
                }
            case 2:
                {
                    Recipient = RandomMethod.PersonInfo();
                    GameSceneManager.Instance.wrongPart.Add($"ParcelApplication. Recipient 다름");

                    break;
                }
            case 3:
                {
                    ApplicationDate = RandomMethod.IssueDate();
                    GameSceneManager.Instance.wrongPart.Add($"ParcelApplication. ApplicationDate 다름");

                    break;
                }
            case 4:
                {
                    ParcelContents = RandomMethod.ParcelContents();
                    GameSceneManager.Instance.wrongPart.Add($"ParcelApplication. ParcelContents 다름");

                    break;
                }
            case 5:
                {
                    Quantity = RandomMethod.ParcelQuantity();
                    GameSceneManager.Instance.wrongPart.Add($"ParcelApplication. Quantity 다름");

                    break;
                }
            case 6:
                {
                    Purpose = RandomMethod.ReceiptReason();
                    GameSceneManager.Instance.wrongPart.Add($"ParcelApplication. Purpose 다름");

                    break;
                }
            case 7:
                {
                    OfficialSeal = RandomMethod.Bool();
                    GameSceneManager.Instance.wrongPart.Add($"ParcelApplication. OfficialSeal 다름");

                    break;
                }            
        }
    }
}

public class ParcelApplication : Document // 택배 신청서
{
    ParcelApplicationData data;

    public string CustomsNumber { get { return data.customsNumber; } set { data.customsNumber = value; } }        // 통관 번호
    public string TrackingNumber { get { return data.trackingNumber; } set { data.trackingNumber = value; } }        // 운송장 번호
    public float Weight { get { return data.Weight; } set { data.Weight = value; } }                // 중량
    public PersonInfo Recipient { get { return data.Recipient; } set { data.Recipient = value; } }       // 수취인
    public string ApplicationDate { get { return data.ApplicationDate; } set { data.ApplicationDate = value; } }       // 신청일자
    public List<string> ParcelContents { get { return data.ParcelContents; } set { data.ParcelContents = value; } } // 택배 내용물
    public List<int> Quantity { get { return data.Quantity; } set { data.Quantity = value; } }         // 택배 수량
    public ReceiptReason Purpose { get { return data.Purpose; } set { data.Purpose = value; } }       // 택배 목적, 배송 이유
    public string PersonalUseReason { get { return data.PersonalUseReason; } set { data.PersonalUseReason = value; } }     // 기타 배송 이유, ReceiptReason이 PersonalUse일 때 사용
    public bool OfficialSeal { get { return data.OfficialSeal; } set { data.OfficialSeal = value; } }         // 관리국 인장

    [Header("Text Elements")]
    public TMP_Text[] textElements;

    [Header("Image")]
    public Image sealImage;

    [Header("Toggles")]
    public Toggle giftTogs;
    public Toggle documentsTogs;
    public Toggle foodTogs;
    public Toggle urgentGoodsTogs;
    public Toggle personalUseTogs;

    private void Awake()
    {
        if (textElements == null)
        {
            textElements = new TMP_Text[8];
        }

        if (sealImage == null)
        {
            Debug.LogWarning("sealImage가 설정되지 않았습니다.");
        }
    }

    protected override void UpdateUI()
    {
        base.customerData = GameSceneManager.Instance.currentCustomerData;
        data = base.customerData.parcelApplicationData;
        if (data == null) return;

        if (textElements == null || textElements.Length < 8)
        {
            Debug.Log("BasicInvoice Text Elements 부족");
            return;
        }

        textElements[0].text = CustomsNumber;
        textElements[1].text = TrackingNumber;
        textElements[2].text = Weight + "kg";
        textElements[3].text = Recipient.Name;
        textElements[4].text = ApplicationDate;
        textElements[5].text = string.Join(", ", ParcelContents);
        textElements[6].text = string.Join(", ", Quantity);
        textElements[7].text = (Purpose == ReceiptReason.PersonalUse ? PersonalUseReason : "");


        // ReceiptReason에 따라 체크박스 설정
        if (giftTogs != null && documentsTogs != null && foodTogs != null && 
            urgentGoodsTogs != null && personalUseTogs != null)
        {
            giftTogs.isOn = (Purpose == ReceiptReason.Gift);
            documentsTogs.isOn = (Purpose == ReceiptReason.Documents);
            foodTogs.isOn = (Purpose == ReceiptReason.Food);
            urgentGoodsTogs.isOn = (Purpose == ReceiptReason.UrgentGoods);
            personalUseTogs.isOn = (Purpose == ReceiptReason.PersonalUse);

            // 비활성화 처리
            giftTogs.interactable = false;
            documentsTogs.interactable = false;
            foodTogs.interactable = false;
            urgentGoodsTogs.interactable = false;
            personalUseTogs.interactable = false;
        }

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