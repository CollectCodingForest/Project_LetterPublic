using System;
using System.Collections;
using System.Collections.Generic;
using DocumentInterface;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MainObjectEvent))]
[DisallowMultipleComponent]

public class Sticker : MonoBehaviour
{
    // 스티커. TestDragScript 상속하는 컴포넌트가 있는 오브젝트에 Add컴포넌트하면 스티커기능을 사용할 수 있게 한다.

    private MainObjectEvent drag;
    private GameObject targetObj;  

    public float targetScale;
    private Vector3 _targetScale;        
    public GameObject btnGameObject;
    private Image btnImage;
    private Button btn;

   

    private void OnEnable()
    {
        //doc = GetComponent<Document>();
        drag = GetComponent<MainObjectEvent>();
        targetObj = drag.mainDeskObjectImage;

        btnImage = btnGameObject.GetComponent<Image>();
        btn = btnGameObject.GetComponent<Button>();
        btn.onClick.AddListener(BtnSticker);

        //btnImage = GetComponent<Image>();
        //btn = GetComponent<Button>();
        GameSceneManager.Instance.eventSubject.OnEndCustomer += ResetScale;
        GameSceneManager.Instance.cutInvoice.OnCutEnd += ActiveImage;
        _targetScale = new Vector3(targetScale, targetScale, 0f);
    }

    private void OnDisable()
    {
        GameSceneManager.Instance.eventSubject.OnEndCustomer -= ResetScale;
        GameSceneManager.Instance.cutInvoice.OnCutEnd -= ActiveImage;
    }
      
    void ActiveImage()
    {
        btn.enabled = true;
        btnImage.enabled = true;
    }

    public void BtnSticker()    // 에디터에서 인스펙터 버튼 설정.
    {
        targetObj.transform.localScale = _targetScale;
        btnImage.enabled = false;
        btn.enabled = false;
        targetObj.transform.parent.GetComponent<IStickerable>().IsSticker = true;
        // TODO::애니메이션 재생
    }

    void ResetScale() // 옵저버로 보기
    {
        targetObj.transform.parent.position = GameSceneManager.Instance.unUseDocumentPosition.position;
        targetObj.transform.localScale = Vector3.one;
        //btnImage.enabled = false;
        //btn.enabled = false;
        targetObj.transform.parent.GetComponent<IStickerable>().IsSticker = false;
    }

    public GameObject GetCreatedGameObject()
    {
        return btnGameObject;
    }
    public void SetCreatedGameObject(GameObject obj)
    {
        btnGameObject = obj;        
    }

}
