using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DocumentInterface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SubObjectEvent : TestDragScript, IReturnable
{
    public GameObject mainDeskobjectImage;

    private Shadow mainObjectShadow;

    private new void Awake()
    {
        base.Awake();

        Initialize();
    }

    public void Initialize()
    {
        subDeskObject = transform.GetChild(0).gameObject;

        if (GameObject.Find("Main_" + splitname[1]))
        {
            base.mainDeskObjectImage = GameObject.Find("Main_" + splitname[1]).gameObject;
            mainDeskobjectImage = base.mainDeskObjectImage.transform.GetChild(0).gameObject;

            mainObjectShadow = mainDeskobjectImage.GetComponent<Shadow>();
        }
    }
    public void SplitName()
    {
        NameSplitToParcel();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        FindMousePlace.instance.MoveObjectToMousePosition(base.mainDeskObjectImage);

        mainDeskobjectImage.SetActive(!FindMousePlace.instance.inSubDesk);
        subDeskObject.SetActive(FindMousePlace.instance.inSubDesk);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        base.mainDeskObjectImage.GetComponent<RectTransform>().SetAsLastSibling();
        mainObjectShadow.enabled = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        mainObjectShadow.enabled = false;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        //만약 서브책상에 드랍된다면 정해진 자리로 자동 정리
        if (!RegulationBookController.isDraggingHandled)
        {
            ReturnObj(eventData);
        }
    }

    
    // 이거는 FSM으로 리팩토링 해보기?
    public void ReturnObj(PointerEventData eventData) 
    {
        if (GameSceneManager.Instance.isStamp && UtilUI.IsPointerOverSpecificUI(GameSceneManager.Instance.dropZone.returnZone, eventData))
        {
            GameSceneManager.Instance.dropZone.DropToCustomer(this.gameObject);
        }
        else if (!GameSceneManager.Instance.isStamp && UtilUI.IsPointerOverSpecificUI(GameSceneManager.Instance.dropZone.returnZone, eventData))
        {
            GameSceneManager.Instance.dropZone.DropToDesk(this.gameObject);
        }
        //else if (GameSceneManager.Instance.isStamp && UtilUI.IsPointerOverSpecificUI(GameSceneManager.Instance.dropZone.collectZone, eventData))
        //{
        //    GameSceneManager.Instance.dropZone.DropToTop(this.gameObject);
        //}
    }
}
