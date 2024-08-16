using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class StampAnimation : MonoBehaviour
{
    //생성한 송장을 이곳으로 전달해야함. 임시적으로 수기로 연결하겠습니다.
    //나중에 게임매니저든 뭐든 통해서 전달해주세요! 도장 생성할 Transform만 접근 가능하면 됩니다~
    [field:SerializeField] public GameObject stampPlace {get; private set;}

    [SerializeField] private GameObject StampPermission;
    [SerializeField] private GameObject StampDisallowed;

    [SerializeField] private bool StampResult; // true면 허가 / false면 불가  / 인스펙터에서 초기화하고 있음
    private Vector2 PermissionPosition = new Vector2(426, -366);
    private Vector2 DisallowedPosition = new Vector2(14, -366);

    public List<GameObject> stamps = new List<GameObject>();
    public void Stamping()
    {
        transform.DOLocalMoveY(-34, 0.5f).SetEase(Ease.InOutExpo).OnComplete(StampingOver);
    }

    void StampingOver()
    {
        MakeStamp();
        transform.DOLocalMoveY(79, 1f).SetEase(Ease.InQuart);
    }

    void MakeStamp()
    {
        GameObject Stamp;

        if (StampResult) Stamp = Instantiate(StampPermission, PermissionPosition, Quaternion.identity);
        else
        {
            Stamp = Instantiate(StampDisallowed, DisallowedPosition, Quaternion.identity);
            GameSceneManager.Instance.currentNotAllowed++;  // TODO:: 도장을 누르기만 하면 찍고 생성하고있는데, 나중에 수정했을 때 같이 조건식 수정하기.
        }
        stamps.Add(Stamp);
        GameSceneManager.Instance.Judged();
        GameSceneManager.Instance.dropZone.collectZone.SetActive(true);
        Stamp.transform.SetParent(stampPlace.transform);
    }

    public void DestroyStamp()
    {
        foreach (GameObject Stamp in stamps)
        {
            Destroy(Stamp);
        }
        stamps.Clear();
    }
}
