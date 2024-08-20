using System;
using System.Collections.Generic;
using DG.Tweening;
using DocumentInterface;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[Serializable]
public class ParcelData : ILegal
{
    public ParcelSize size;
    public List<ItemData> items;

    public ParcelData(ParcelSize size, List<ItemData> items)
    {
        this.size = size;
        this.items = items;
    }

    public void OverrideRandomInfo()
    {
        //data를 현재 값이 아닌 랜덤한 값으로 바꾸는 로직
        float originHeight = size.Height;
        float originWidth = size.Width;

        float newHeight = UtilGeneric.GetRandomValueExcludingOriginal(originHeight, 200, 700);
        float newWidth = UtilGeneric.GetRandomValueExcludingOriginal(originHeight, 200, 700);
        size.Height = newHeight;
        size.Width = newWidth;
    }
}
public class Parcel : Document, ILegal, ICollectable, IReturnable ,IEndDragHandler
{
    ParcelData data;
    public ParcelSize size { get { return data.size; } set { data.size = value; } }
    public List<ItemData> itemDatas { get { return data.items; } set { data.items = value; } }
    public List<Item> items = new List<Item>();
    public GameObject imageObj;
    public Image image; // 택배상자 이미지
    public bool isLegal;    // 상자 자체에 있을 수있는 불법정보 : 무게, 사이즈등등
    public bool IsLegal {  get { return isLegal; } }

    public float randomRangeMin = 200f;
    public float randomRangeMax = 700f;

    private float dropSpeed;

    [Header("Debug")]
    public TextMeshProUGUI tmp;

    protected override void Start()
    {
        base.Start();
        dropSpeed = GameSceneManager.Instance.dropSpeed;
    }

    protected override void UpdateUI()
    {
        data = GameSceneManager.Instance.currentCustomerData.parcelData;
        isLegal = !GameSceneManager.Instance.currentCustomerData.illegalDocs.HasFlag(BindingDocumentsFlags.Parcel);

        if (!isLegal)
        {
            OverrideRandomInfo();
        }

        SetImage(size);
        SetItems(itemDatas);

        tmp.text = $"IsLegal: {IsLegal}";
    }


    // 이미지 세팅
    void SetImage(ParcelSize box)
    {
        image = imageObj.GetComponentInChildren<Image>();
                        
        image.rectTransform.sizeDelta = new Vector2 (box.Width, box.Height);
    }

    // 아이템 세팅
    void SetItems(List<ItemData> _items)
    {
        this.itemDatas = _items;
        for(int i = 0; i < _items.Count; i++)
        {
            //Item obj = Instantiate(GameManager.Instance.parcelMaker.itemPrefab, gameObject.transform);
            GameObject obj = new GameObject("Item");
            obj.transform.parent = this.transform;
            Item item = obj.AddComponent<Item>();
            item.data = _items[i];
            items.Add(item);
        }
    }

    public void OverrideRandomInfo()
    {
        //data를 현재 값이 아닌 랜덤한 값으로 바꾸는 로직
        float originHeight = data.size.Height;
        float originWidth = data.size.Width;
                
        float newHeight = UtilGeneric.GetRandomValueExcludingOriginal(originHeight, randomRangeMin, randomRangeMax);
        float newWidth = UtilGeneric.GetRandomValueExcludingOriginal(originHeight, randomRangeMin, randomRangeMax);
        data.size.Height = newHeight;
        data.size.Width = newWidth;
    }

    public void Collect()
    {
        // 통과인경우 이게 실행
        float distance = gameObject.transform.position.y - GameSceneManager.Instance.targetPosition.y;
        float duration = distance / dropSpeed;
        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, GameSceneManager.Instance.unUseDocumentPosition.position.y, 0f), duration).SetEase(Ease.Linear);
        GameSceneManager.Instance.CurrentDocumentsCount--;
    }

    public void ReturnObj(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameSceneManager.Instance.isStamp && UtilUI.IsPointerOverSpecificUI(GameSceneManager.Instance.dropZone.returnZone, eventData))
        {
            //GameSceneManager.Instance.dropZone.DropToCustomer(this.gameObject);
        }
        else if (!GameSceneManager.Instance.isStamp && UtilUI.IsPointerOverSpecificUI(GameSceneManager.Instance.dropZone.returnZone, eventData))
        {
            GameSceneManager.Instance.dropZone.DropToDesk(this.gameObject);
        }
        else if (GameSceneManager.Instance.isStamp && UtilUI.IsPointerOverSpecificUI(GameSceneManager.Instance.dropZone.collectZone, eventData))
        {
            //GameSceneManager.Instance.dropZone.DropToTop(this.gameObject);
            Collect();
        }
    }

    
}