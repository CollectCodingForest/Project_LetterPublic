/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Customer
{
    // data를 기반으로 만들어진 객체
    // 
    public CustomerData data;
    public BindingDocumentsFlags hasDocuments;
    public BindingDocumentsFlags randomPickFlags;
    //public bool isLegal;
    
    public void Initialize()
    {
        //isLegal = (UnityEngine.Random.value > 0.5f);

        if(data==null)
        {
            data = GameSceneManager.Instance.currentCustomerData;
            //CreateRandomData();
            CheckNullCustomerData();
            if (!data.isLegal)
            {
                ApplyIncorrectData();
            }
        }
    }

    private void ApplyIncorrectData()
    {
        // 메서드기능: 오류정보를 생성해서 덮어씌운다

        // 갖고있는 서류에서 랜덤한 갯수의 플래그를 뽑는다.
        randomPickFlags = GetRandomFlags(hasDocuments);
        // 그 플래그에 해당하는 클래스에서 랜덤메서드를 실행한다.
        ProcessFlags(randomPickFlags);
    }

    void CheckNullCustomerData()
    {
        // 메서드기능: CustomerData에 정보가 있으면 그 서류를 가지고 있다고 판단하고 비트플래그를 올린다.
        if (data.identityCardData != null)
        {
            hasDocuments = hasDocuments | BindingDocumentsFlags.IdentityCard;
        }
        if (data.basicInvoiceData != null)
        {
            hasDocuments = hasDocuments | BindingDocumentsFlags.BasicInvoice;
        }
        //if (data.parcelApplication != null)
        //{
        //    hasDocuments = hasDocuments | BindingDocumentsFlags.ParcelApplication;
        //}
        if (data.parcelData != null)
        {
            hasDocuments = hasDocuments | BindingDocumentsFlags.Parcel;
        }
    }

    public BindingDocumentsFlags GetRandomFlags(BindingDocumentsFlags hasDocuments)
    {
        // 메서드기능: hasDocuments에서 랜덤한 수의 플래그를 뽑는다.
        System.Random random = new System.Random();
        var values = Enum.GetValues(typeof(BindingDocumentsFlags))
                            .Cast<BindingDocumentsFlags>()
                            .Where(flag => flag != BindingDocumentsFlags.Default && hasDocuments.HasFlag(flag)).ToArray();

        int randomCount = random.Next(1, values.Length + 1);    // 1~ enum길이에서 랜덤한 숫자를 얻는다.
        BindingDocumentsFlags result = BindingDocumentsFlags.Default;

        
        foreach (var value in values.OrderBy(_ => random.Next()).Take(randomCount))
        {
            result |= value;
        }
        return result;
    }

    public void ProcessFlags(BindingDocumentsFlags flags)
    {
        if(flags.HasFlag(BindingDocumentsFlags.IdentityCard))
        {
            // 불일치정보를 만들어 덮어씌우는 메서드
            this.data.identityCardData.OverrideRandomInfo();
        }        
        if (flags.HasFlag(BindingDocumentsFlags.BasicInvoice))
        {
            this.data.basicInvoiceData.OverrideRandomInfo();
        }
        if (flags.HasFlag(BindingDocumentsFlags.Parcel))
        {
            // 불일치정보를 만들어 덮어씌우는 메서드
            //this.data.parcels.OverrideRandomInfo();
        }
    }

    //// 기초 랜덤정보 생성. 
    //public void CreateRandomData()
    //{
    //    bool randomBool = UnityEngine.Random.value > 0.5f;
    //    string name = UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.randomPool.namePool, GameSceneManager.Instance.randomPool.usedNamePool);
    //    string name2 = UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.randomPool.namePool, GameSceneManager.Instance.randomPool.usedNamePool);
    //    string address = UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.randomPool.addressPool);
    //    string address2 = UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.randomPool.addressPool);
    //    string rrn = UtilGeneric.GenerateRandomNumber(6) + "-" + UtilGeneric.GenerateRandomNumber(6);
    //    string trackingNumber = UtilGeneric.GenerateRandomNumber(12);
    //    PersonInfo tempPersonInfo = new PersonInfo(name2, address2, "contact", randomBool);

    //    data = ScriptableObject.CreateInstance<CustomerData>(); //new CustomerData();
    //    data.personInfoData = data.InitPersonInfo(name, address, "contact", randomBool);
    //    data.identityCardData = data.InitIdentityCard(rrn, UtilGeneric.GetRandomDate(new DateTime(1999, 1, 1), new DateTime(2010, 12, 31)));
        
    //    data.NumberOfItems = UnityEngine.Random.Range(1, 3);

    //    data.parcel = data.InitParcelDatas();
    //    data.basicInvoiceData = data.InitBasicInvoice(trackingNumber, tempPersonInfo, 24f, 8000, randomBool);
    //}



    // customer가 가질 서류 체크 메서드
    // 인스펙터에서 토글로 customer의 서류생성여부를 선택할 수 있다.


}

*/