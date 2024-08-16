using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerData", menuName = "ScriptableObject/CustomerData")]
[Serializable]
public class CustomerData : ScriptableObject
{
    public bool IsRandomButFollowDocs;
    public bool isLegal;    // 이 신청자 정보가 적법할지 체크.
    
    public PersonInfo senderInfoData;

    // 서류에 필요한 정보
    public bool identityCard;
    public IdentityCardData identityCardData;
    public bool basicInvoice;
    public BasicInvoiceData basicInvoiceData;
    public bool customsClearance;
    public CustomsClearanceData customsClearanceData;
    public bool parcelApplication;
    public ParcelApplicationData parcelApplicationData;
    public bool parcelPermit;
    public ParcelPermitData parcelPermitData;

    // 택배상자 생성에 필요한 정보    
    public ParcelData parcelData;
    public int NumberOfItems;
    [SerializeField] public List<ItemData> itemDatas;

    [field : SerializeField] public BindingDocumentsFlags hasDocuments { get; set; } = BindingDocumentsFlags.Default;
    public BindingDocumentsFlags illegalDocs;

    

    
    public void AllRandom(bool _id, bool _basicInvoice, bool _customsClearance, bool _parcelApplication, bool _parcelPermit)
    {
        bool randomBool = UnityEngine.Random.value > 0.5f;
        string name = UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.pool.randomPool[PoolKey.Name.ToString()], GameSceneManager.Instance.pool.usedNamePool);
        string name2 = UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.pool.randomPool[PoolKey.Name.ToString()], GameSceneManager.Instance.pool.usedNamePool);
        string address = UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.pool.randomPool[PoolKey.Address.ToString()]);
        string address2 = UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.pool.randomPool[PoolKey.Address.ToString()]);
        string rrn = UtilGeneric.GenerateRandomNumber(6) + "-" + UtilGeneric.GenerateRandomNumber(6);
        string trackingNumber = UtilGeneric.GenerateRandomNumber(12);
        PersonInfo receiptPersonInfo = new PersonInfo(name2, address2, "contact", randomBool);

        List<string> parcelContents = new List<string>() { UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.pool.itemNamePool), UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.pool.itemNamePool) };
        List<int> parcelQuantity = new List<int>() { UnityEngine.Random.Range(1, 2), UnityEngine.Random.Range(1, 2) };
        ParcelSize parcelSize = new ParcelSize(UnityEngine.Random.Range(100, 700), UnityEngine.Random.Range(100, 700));
        string customsNumber = UtilGeneric.GenerateRandomNumber(6);
        string issueDate = UtilGeneric.GetRandomDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31));
        string expiryDate = issueDate;
        ReceiptReason receiptReason = (ReceiptReason)UnityEngine.Random.Range(0, Enum.GetValues(typeof(ReceiptReason)).Length);
        string PersonalUseReason = "알빠노";

        isLegal = randomBool;
        senderInfoData = new PersonInfo(name, address, "contact", randomBool); //InitPersonInfo(name, address, "contact", randomBool);

        parcelData = new ParcelData(parcelSize, InitItemDataList());
        float weight = WeightSum();
        
        float WeightSum()
        {
            float weightSum = 0;
            foreach (var item in parcelData.items)
            {
                weightSum += item.Weight;
            }
            return weightSum;
        }
        List<ItemData> InitItemDataList()
        {
            itemDatas = new List<ItemData>();
            foreach( var item in parcelContents)
            {
                if(GameSceneManager.Instance.pool.itemPool.TryGetValue(item, out ItemData itemData))
                {
                    itemDatas.Add(itemData);
                }
            }
            return itemDatas;
        }

        if (_id)
        {
            identityCard = true;

            identityCardData = new IdentityCardData(name, rrn, address, UtilGeneric.GetRandomDate(new DateTime(1999, 1, 1), new DateTime(2010, 12, 31))); 
        }
        if (_basicInvoice)
        {
            basicInvoice = true;

            basicInvoiceData = new BasicInvoiceData(trackingNumber, senderInfoData, receiptPersonInfo, parcelContents, parcelQuantity, weight, 6000, true);
        }        
        if(_customsClearance)
        {
            customsClearance = true;

            customsClearanceData = new CustomsClearanceData(name, rrn, customsNumber, issueDate, expiryDate, true);
        }
        if (_parcelApplication)
        {
            parcelApplication = true;

            parcelApplicationData = new ParcelApplicationData(customsNumber, trackingNumber, weight, receiptPersonInfo, issueDate, parcelContents, parcelQuantity, receiptReason, PersonalUseReason, true);
        }
        if(_parcelPermit)
        {
            parcelPermit = true;

            parcelPermitData = new ParcelPermitData(name, rrn, trackingNumber, issueDate, expiryDate, true);
        }
    }

    public void ResetBool()
    {
        identityCard = false;
        basicInvoice = false;
        customsClearance = false;
        parcelApplication = false;
        parcelPermit = false;

    }

    public void InitHasDocuments()
    {
        // 메서드기능: CustomerData에 정보가 있으면 그 서류를 가지고 있다고 판단하고 비트플래그를 올린다.
        if (identityCard)
        {
            hasDocuments = hasDocuments | BindingDocumentsFlags.IdentityCard;
        }
        if (basicInvoice)
        {
            hasDocuments = hasDocuments | BindingDocumentsFlags.BasicInvoice;
        }        
        if (parcelData != null)
        {
            hasDocuments = hasDocuments | BindingDocumentsFlags.Parcel;
        }
        if (customsClearance)
        {
            hasDocuments = hasDocuments | BindingDocumentsFlags.CustomsClearance;
        }
        if (parcelApplication)
        {
            hasDocuments = hasDocuments | BindingDocumentsFlags.ParcelApplication;
        }
        if (parcelPermit)
        {
            hasDocuments = hasDocuments | BindingDocumentsFlags.ParcelPermit;
        }
    }

    public void GetRandomFlags()
    {
        // 메서드기능: hasDocuments에서 랜덤한 수의 플래그를 뽑는다.
        System.Random random = new System.Random();
        var values = Enum.GetValues(typeof(BindingDocumentsFlags))
                            .Cast<BindingDocumentsFlags>()
                            .Where(flag => flag != BindingDocumentsFlags.Default && hasDocuments.HasFlag(flag)).ToArray();

        int randomCount = random.Next(1, 2);     // 난이도상 적은 수가 틀려야 틀렸는지 찾기 힘들다.        //random.Next(1, values.Length + 1);    // 1~ enum길이에서 랜덤한 숫자를 얻는다.
        BindingDocumentsFlags result = BindingDocumentsFlags.Default;

        foreach (var value in values.OrderBy(_ => random.Next()).Take(randomCount))
        {
            result |= value;
        }

        illegalDocs = result;
    }

    public void OverrideIncorrectDataToIIllegalDocs()
    {
        if (illegalDocs.HasFlag(BindingDocumentsFlags.IdentityCard))
        {
            identityCardData.OverrideRandomInfo();            // 불일치정보를 만들어 덮어씌우는 메서드
        }
        if (illegalDocs.HasFlag(BindingDocumentsFlags.BasicInvoice))
        {
            basicInvoiceData.OverrideRandomInfo();
        }
        if (illegalDocs.HasFlag(BindingDocumentsFlags.Parcel))
        {
            // 불일치정보를 만들어 덮어씌우는 메서드
            //this.data.parcels.OverrideRandomInfo();
        }
        if(illegalDocs.HasFlag(BindingDocumentsFlags.CustomsClearance))
        {
            customsClearanceData.OverrideRandomInfo();
        }
        if (illegalDocs.HasFlag(BindingDocumentsFlags.ParcelApplication))
        {
            parcelApplicationData.OverrideRandomInfo();
        }
        if (illegalDocs.HasFlag(BindingDocumentsFlags.ParcelPermit))
        {
            parcelPermitData.OverrideRandomInfo();
        }
    }
}
