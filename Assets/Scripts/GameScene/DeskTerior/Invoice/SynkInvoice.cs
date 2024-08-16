using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//송장 상부와 게임 오브젝트 on/off 싱크를 맞추는 함수입니다.

public class SynkInvoice : MonoBehaviour
{
    public GameObject InvoiceTop;

    private Shadow TopShadow;
    private Shadow shadow;

    private Image image;

    [Header ("송장 분리 여부")]
    public bool IsCut;

    private void Awake()
    {
        shadow = GetComponent<Shadow>();
        TopShadow = InvoiceTop.GetComponent<Shadow>();
        image = GetComponent<Image>();
    }


    // Update is called once per frame
    void Update()
    {
        if (IsCut == false)
        {
            shadow.enabled = TopShadow.enabled;
            image.enabled = InvoiceTop.activeSelf;
        }
        
    }
}
