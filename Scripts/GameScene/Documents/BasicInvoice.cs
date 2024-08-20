using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DocumentInterface;
using UnityEngine.EventSystems;

[Serializable]
public class BasicInvoiceData : ILegal
{
    public string TrackingNumber;     // 운송장 번호
    public PersonInfo Sender;        // 보내는 사람
    public PersonInfo Recipient;        // 받는 사람
    public List<string> ParcelContents; // 택배 내용물
    public List<int> Quantity;          // 택배 수량
    public float Weight;                 // 중량
    public int Fare;                       // 택배 요금
    public bool HasSenderSignature;     // 발송인 서명

    public BasicInvoiceData() { }
    public BasicInvoiceData(string trackingNumber, PersonInfo sender, PersonInfo recipient, List<string> parcelContents, List<int> quantity, float weight, int fare, bool hasSenderSignature)
    {
        TrackingNumber = trackingNumber;
        Sender = sender;
        Recipient = recipient;
        ParcelContents = parcelContents;
        Quantity = quantity;
        Weight = weight;
        Fare = fare;
        HasSenderSignature = hasSenderSignature;
    }

    public void OverrideRandomInfo()
    {
        System.Random random = new System.Random();
        int num;
        if (GameManager.Instance.date <= 1)
        {
            num = random.Next(0, 1);
        }
        else
        {
            num = random.Next(0, 4);
        }
        string reason;

        switch (num)
        {
            case 0:
                {
                    Sender = RandomMethod.PersonInfo();
                    reason = "Sender";
                    

                    break;
                }
            case 1:
                {
                    TrackingNumber = RandomMethod.TrackingNumber();
                    reason = "TrackingNumber";

                    break;
                }
            case 2:
                {
                    Recipient = RandomMethod.PersonInfo();
                    reason = "Recipient";

                    break;
                }
            case 3:
                {
                    ParcelContents = RandomMethod.ParcelContents();
                    reason = "ParcelContents";

                    break;
                }
                case 4:
                {
                    Quantity = RandomMethod.ParcelQuantity();
                    reason = "Quantity";
                    break;
                }
            case 5:
                {
                    Weight = RandomMethod.Weight();
                    reason = "Weight";
                    break;
                }
            default:
                reason = "Unknown";
                break;
        }
        GameSceneManager.Instance.wrongPart.Add($"{this.GetType().Name}. {reason} 다름");
    }
}

public class BasicInvoice : Document, IStickerable, IDragHandler, IPointerUpHandler
{
    BasicInvoiceData data;
    public SubObjectEvent subObj;
    public string TrackingNumber { get { return data.TrackingNumber; } set { data.TrackingNumber = value; } }       // 운송장 번호
    public PersonInfo Sender { get { return data.Sender; } set { data.Sender = value; } }          // 보내는 사람
    public PersonInfo Recipient { get { return data.Recipient; } set { data.Recipient = value; } }       // 받는 사람
    public List<string> ParcelContents { get { return data.ParcelContents; } set { data.ParcelContents = value; } }// 택배 내용물
    
    public List<int> Quantity { get { return data.Quantity; } set { data.Quantity = value; } }          // 택배 수량
    public float Weight { get { return data.Weight; } set { data.Weight = value; } }              // 중량
    public int Fare { get { return data.Fare; } set { data.Fare = value; } }                  // 택배 요금
    public bool HasSenderSignature { get { return data.HasSenderSignature; } set { data.HasSenderSignature = value; } }     // 발송인 서명

    private bool isSticker;
    public bool IsSticker {  get { return isSticker; } set {  isSticker = value; } }

    [Header("Text Elements")]
    public TMP_Text[] textElements;

    [Header("Image")]
    public Image signatureImage;

    private void Awake()
    {
        if (textElements == null)
        {
            textElements = new TMP_Text[11];
        }
    }
   
    protected override void UpdateUI()
    {
        base.customerData = GameSceneManager.Instance.currentCustomerData;
        data = base.customerData.basicInvoiceData;
        if (data == null) return;

        if (textElements == null || textElements.Length < 11)
        {
            Debug.Log("BasicInvoice Text Elements 부족");
            return;
        }

        textElements[0].text = TrackingNumber;
        textElements[1].text = Sender.Name;
        textElements[2].text = Sender.Address;
        textElements[3].text = Sender.Contact;
        textElements[4].text = Recipient.Name;
        textElements[5].text = Recipient.Address;
        textElements[6].text = Recipient.Contact;
        textElements[7].text = string.Join("\n", ParcelContents);
        textElements[8].text = string.Join("\n", Quantity);
        textElements[9].text = Weight.ToString("0") + "kg";
        textElements[10].text = Fare + "원";

        //LoadPhoto();
    }

    private void LoadPhoto()
    {
        string imagePath = HasSenderSignature ? "Signature/" + Sender.Name : "Seal/fakeSeal";
        Sprite loadSprite = Resources.Load<Sprite>(imagePath);

        if (loadSprite != null)
        {
            signatureImage.sprite = loadSprite;
        }
        else
        {
            Debug.LogWarning("이미지를 로드할 수 없습니다: " + imagePath);
        }
    }

    public void SetActiveStamps(bool isDesk)
    {
        if (GameSceneManager.Instance.stampA.stamps.Count != 0)
        {
            foreach (var item in GameSceneManager.Instance.stampA.stamps)
            {
                item.gameObject.SetActive(isDesk);
            }
        }
        if (GameSceneManager.Instance.stampB.stamps.Count != 0)
        {
            foreach (var item in GameSceneManager.Instance.stampB.stamps)
            {
                item.gameObject.SetActive(isDesk);
            }
        }
    }

    public void AttachSticker()
    {
        this.transform.SetParent(GameSceneManager.Instance.Parcel.transform);
        gameObject.GetComponent<MainObjectEvent>().enabled = false;
    }

    public void RemoveSticker()
    {
        this.transform.SetParent(GameSceneManager.Instance.mainDesk);
        gameObject.GetComponent<MainObjectEvent>().enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetActiveStamps(!FindMousePlace.instance.inSubDesk);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsSticker)
        {
            if (UtilUI.IsPointerOverSpecificUI(GameSceneManager.Instance.Parcel.imageObj, eventData))
            {
                AttachSticker();
            }
        }
    }    
    
    public void RaycastDisabled()
    {
        foreach( var item in textElements)
        {
            item.raycastTarget = false;
        }
    }
    public void RaycastEnabled()
    {
        foreach (var item in textElements)
        {
            item.raycastTarget = true;
        }
    }
}