using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnScript : MonoBehaviour
{
    //public Button btn0;
    public Button btn1;
    //public Button btn2;

    private void Awake()
    {
        InitBtn();
    }


    void InitBtn()
    {
        btn1.onClick.AddListener(GameSceneManager.Instance.BtncomeNewCustomer);        
    }
}
