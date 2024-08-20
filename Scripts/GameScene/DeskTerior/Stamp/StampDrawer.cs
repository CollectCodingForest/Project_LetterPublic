using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StampDrawer : MonoBehaviour
{
    public float moveY;
    // public Ease ease; << 커브 종류 리스트로 확인 가능
    public bool isOpen = false;

    [SerializeField] private AnimationCurve animationCurve; // 커브 직접 생성

    public void OnOffDesk()
    {
        moveY = isOpen == false ? -330 : -707;
        transform.DOLocalMoveY(moveY, 1).SetEase(animationCurve);
        isOpen = !isOpen;
    }
}
