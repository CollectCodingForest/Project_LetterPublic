using System;
using System.Xml.Linq;
using DocumentInterface;
using UnityEngine;

[Serializable]
public class ItemData 
{
    public string Name;    // TODO::품목. enum으로 바꿀수 있을지도...    
    public float Weight;
    public bool IsLegal;

    public ItemData() { }
    public ItemData(string name, float weight, bool isLegal) 
    {
        Name = name;
        Weight = weight;
        IsLegal = isLegal;
    }
    public ItemData(ItemData poolData)
    {
        Name = poolData.Name;
        Weight = poolData.Weight;
        IsLegal = poolData.IsLegal;
    }


}



[Serializable]
public class Item : MonoBehaviour
{
    // 만들어질 아이템 게임오브젝트
    [SerializeField] public ItemData data;

    //public Item(ItemData info)
    //{
    //    this.info = info;
    //}

    public void CreateItem()
    {
        // parcel같이 움직일 수 있는 게임오브젝트 만들기.
        // 해당 parcel의 item끼리 묶기
    }

    //public void OverrideRandomInfo()
    //{
    //    data = UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.pool.itemNamePool);
    //}
}
