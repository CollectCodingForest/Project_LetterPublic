using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainObjectInvoice : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    //사용 안할수도 있음

    private RectTransform _rectTransform;
    private Shadow[] _shadow;

    public GameObject subDeskobject;
    public GameObject subDeskobjectImage;

    public GameObject[] mainDeskobject;

    private Vector2 defaultPosition;

    protected string[] splitname;

    Vector2 mousePosition => FindMousePlace.instance.mousePosition;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        splitname = this.gameObject.name.Split('_');

        mainDeskobject[0] = transform.GetChild(0).gameObject;
        mainDeskobject[1] = transform.GetChild(1).gameObject;

        _shadow[0] = mainDeskobject[0].GetComponent<Shadow>();
        _shadow[1] = mainDeskobject[1].GetComponent<Shadow>();
        shadowOnOff(false);

        subDeskobject = GameObject.Find("Sub_" + splitname[1]).gameObject;
        subDeskobjectImage = subDeskobject.transform.GetChild(0).gameObject;
    }

    public void Start()
    {
        subDeskobjectImage.SetActive(false); //나중에 변경. 테스트 위해서 작성
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.gameObject.transform.position =
            new Vector2(mousePosition.x - defaultPosition.x, mousePosition.y - defaultPosition.y);

        FindMousePlace.instance.MoveObjectToMousePosition(subDeskobject);

        mainDeskobject[0].SetActive(!FindMousePlace.instance.inSubDesk);
        mainDeskobject[1].SetActive(!FindMousePlace.instance.inSubDesk);
        subDeskobjectImage.SetActive(FindMousePlace.instance.inSubDesk);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _rectTransform.SetAsLastSibling();

        defaultPosition.x = mousePosition.x - this.gameObject.transform.position.x;
        defaultPosition.y = mousePosition.y - this.gameObject.transform.position.y;

        shadowOnOff(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        shadowOnOff(false);
    }

    void shadowOnOff(bool value)
    {
        _shadow[0].enabled = value;
        _shadow[1].enabled = value;
    }
}
