using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeightScale : MonoBehaviour
{
    public TextMeshProUGUI weightScale;

    

    // 택배 오브젝트를 서브데스크에 올려놓아져있는 상태면 이 메서드가 실행
    // 택배오브젝트를 누르고있지 않음, 서브데스크 범위 안에 있어야함, 드래그를 시작하면 실행을 멈춤.
    // 숫자에 옵저버패턴으로 연결.
    // 처음 서류제출할 때 실행.
    // 업데이트로 서브오브젝트가 범위 내에 있는걸 확인하면 1회 실행
    public void Scale()
    {
        float weight = 0;
        // 택배 아이템의 무게를 표시
        foreach ( var item in GameSceneManager.Instance.currentCustomerData.parcelData.items)
        {
            weight += item.Weight;
        }

        weightScale.text = weight.ToString();
    }


}
