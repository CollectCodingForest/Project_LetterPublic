using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class TestDragScript : MonoBehaviour, IBeginDragHandler,
    IDragHandler, IDropHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform _rectTransform;

    [SerializeField]private Vector2 defaultPosition;

    [Header("Dynamic allocation")]
    public GameObject mainDeskObjectImage;
    public GameObject subDeskObject;

    protected string[] splitname;

    [SerializeField] private Vector2 mousePosition2;
    
    protected Vector2 mousePosition
    {
        get
        {
            if (Application.isFocused)
            {       
                return FindMousePlace.instance.mousePosition;
            }
            else return default;
        }       
    }
    

    public void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        splitname = this.gameObject.name.Split('_');
    }

    private void Update()
    {
        mousePosition2 = mousePosition;

    }

    protected void NameSplitToParcel()
    {
        splitname = this.gameObject.name.Split('_');
    }

    public virtual void OnBeginDrag(PointerEventData eventData) //드래그 시작할때 한번만 실행
    {

    }

    public virtual void OnDrag(PointerEventData eventData) //마우스를 누른 상태에서 이동할때마다 실행
    {
        //this.gameObject.transform.position =
        //                new Vector2(mousePosition.x - defaultPosition.x, mousePosition.y - defaultPosition.y);
        transform.position = mousePosition- defaultPosition;
    }

    public virtual void OnDrop(PointerEventData eventData) //마우스를 누른 상태에서 본인 위에서 드랍하면 실행
    {
        //Debug.Log("test : OnDrop");
    }

    public virtual void OnEndDrag(PointerEventData eventData) //드래그 끝날 때 한번만 실행
    {
    }

    public virtual void OnPointerDown(PointerEventData eventData) //클릭 시
    {
        _rectTransform.SetAsLastSibling();
        //Vector3 viewportMousePos = Camera.main.ScreenToViewportPoint(mousePosition);

        //// 마우스가 화면 경계를 벗어났는지 확인
        //if (viewportMousePos.x < 0 || viewportMousePos.x > 1 ||
        //    viewportMousePos.y < 0 || viewportMousePos.y > 1)
        //{
        //    return;  // 화면 경계를 벗어난 경우 null 반환
        //}
        //else
        //{
            defaultPosition.x = mousePosition.x - transform.position.x;
            defaultPosition.y = mousePosition.y - transform.position.y;
        //}
    }

    public virtual void OnPointerUp(PointerEventData eventData) //클릭 종료 시
    {
        defaultPosition = Vector2.zero;
    }
}
