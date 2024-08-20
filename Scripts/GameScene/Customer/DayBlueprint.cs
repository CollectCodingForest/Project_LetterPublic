using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayBlueprint", menuName = "ScriptableObject/DayBlueprint")]
[Serializable]
public class DayBlueprint : ScriptableObject
{
    [SerializeField] public List<CustomerData> datas;

    // 체크에 따라서 랜덤을 만든다. 일자진행을 위해서.
    public bool identityCard;
    public bool basicInvoice;
    public bool customsClearance;
    public bool parcelApplication;
    public bool parcelPermit;

    public int index { get; set; }

    public void Initialize()
    {
        if (datas == null)
        {
            datas = new List<CustomerData>();
        }

        for (int i = 0; i < datas.Count; ++i)
        {
            if (datas[i] == null)
            {
                CustomerData data = ScriptableObject.CreateInstance<CustomerData>();
                data.AllRandom(identityCard, basicInvoice, customsClearance, parcelApplication, parcelPermit);
                datas[i] = data;                
            }
            else if (datas[i] != null && datas[i].IsRandomButFollowDocs)
            {
                datas[i].AllRandom(identityCard, basicInvoice, customsClearance, parcelApplication, parcelPermit);
            }

            datas[i].InitHasDocuments();
            if (!datas[i].isLegal)
            {
                datas[i].GetRandomFlags();
                datas[i].OverrideIncorrectDataToIIllegalDocs();
            }
        }
        index = 0;
    }

    public CustomerData GetNextData()
    {
        if (index < datas.Count)
        {
            return datas[index++];
        }
        else return null;
    }

    public void ThisDayEnd()
    {
        for (int i = 0; i < datas.Count; ++i)
        {
            datas[i].hasDocuments = BindingDocumentsFlags.Default;
            datas[i].ResetBool();
        }
        //datas.Clear();
    }
}