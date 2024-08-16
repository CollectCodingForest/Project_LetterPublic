using System.Collections;
using System.Collections.Generic;
using DocumentInterface;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainObjectEvent : TestDragScript
{
    private Shadow _shadow;

    [Header("MainObjectEvent")]
    public GameObject subDeskobjectImage;
    public SubObjectEvent subObj;


    private new void Awake()
    {
        base.Awake();

        Initialize();
    }
    
    public void Initialize()
    {
        mainDeskObjectImage = transform.GetChild(0).gameObject;

        _shadow = mainDeskObjectImage.GetComponent<Shadow>();
        _shadow.enabled = false;

        if (GameObject.Find("Sub_" + splitname[1]))
        {
            base.subDeskObject = GameObject.Find("Sub_" + splitname[1]).gameObject;
            subDeskobjectImage = base.subDeskObject.transform.GetChild(0).gameObject;
            subObj = base.subDeskObject.GetComponent<SubObjectEvent>();
        }        
    }

    public void SplitName()
    {
        NameSplitToParcel();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        FindMousePlace.instance.MoveObjectToMousePosition(base.subDeskObject);

        mainDeskObjectImage.SetActive(!FindMousePlace.instance.inSubDesk);
        subDeskobjectImage.SetActive(FindMousePlace.instance.inSubDesk);        
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        _shadow.enabled = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        _shadow.enabled = false;

        subObj.OnEndDrag(eventData);        
    }
}
