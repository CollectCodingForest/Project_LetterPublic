using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DropZone : MonoBehaviour
{
    public Vector3 returnPos;
    public Vector3 returnParcelPos;
    public GameObject returnZone;
    public GameObject collectZone;

    public float dropSpeed = 1100f;

    float distance;
    float duration;
    public void DropToCustomer(GameObject droppedObj)
    {
        droppedObj.GetComponentInChildren<Image>().raycastTarget = false;
        droppedObj.transform.DOMove(returnPos, 0.3f).SetDelay(0.1f).OnComplete(Complete);
            //() => droppedObj.transform.position = GameSceneManager.Instance.unUseDocumentPosition.position);
        GameSceneManager.Instance.CurrentDocumentsCount--;

        if (GameSceneManager.Instance.IsGrant && (droppedObj == GameSceneManager.Instance.Parcel.gameObject || droppedObj == GameSceneManager.Instance.subParcel))
        {
            GameSceneManager.Instance.currentNotAllowed++;
            Debug.Log("허가했는데 왜 택배를 돌려줘?");
        }
        void Complete()
        {
            droppedObj.transform.position = GameSceneManager.Instance.unUseDocumentPosition.position;
            droppedObj.GetComponentInChildren<Image>().raycastTarget = true;
        }
    }
    public void DropToDesk(GameObject droppedObj)
    {
        distance = droppedObj.transform.position.y - GameSceneManager.Instance.targetPosition.y;
        duration = distance / dropSpeed;
        droppedObj.transform.DOMove(new Vector3(droppedObj.transform.position.x, GameSceneManager.Instance.targetPosition.y, 0f), duration).SetEase(Ease.Linear);

    }
    //public void DropToTop(GameObject droppedObj)
    //{
    //    if (droppedObj.TryGetComponent<ICollectable>(out ICollectable collectable))
    //    {
    //        collectable.Collect();

    //        //distance = droppedObj.transform.position.y - GameSceneManager.Instance.targetPosition.y;
    //        //duration = distance / dropSpeed;
    //        //droppedObj.transform.DOMove(new Vector3(droppedObj.transform.position.x, GameSceneManager.Instance.unUseDocumentPosition.position.y, 0f), duration).SetEase(Ease.Linear);
    //        //GameSceneManager.Instance.CurrentDocumentsCount--;
    //    }
    //    else if(droppedObj.GetComponent<SubObjectEvent>().mainDeskobjectImage.TryGetComponent<ICollectable>(out ICollectable collectable2))
    //    {
    //        // 메인오브젝트를 올릴경우
    //        distance = droppedObj.transform.position.y - GameSceneManager.Instance.targetPosition.y;
    //        duration = distance / dropSpeed;
    //        droppedObj.GetComponent<SubObjectEvent>().mainDeskobjectImage.transform.DOMove(new Vector3(droppedObj.transform.position.x, GameSceneManager.Instance.unUseDocumentPosition.position.y, 0f), duration).SetEase(Ease.Linear);
    //        Tween tween = droppedObj.transform.DOMove(new Vector3(droppedObj.transform.position.x, GameSceneManager.Instance.unUseDocumentPosition.position.y, 0f), duration).SetEase(Ease.Linear);
    //        tween.WaitForCompletion();
    //        droppedObj.transform.GetChild(0).gameObject.SetActive(true);
    //        GameSceneManager.Instance.CurrentDocumentsCount--;            
    //    }
    //    else
    //    {
    //        Debug.Assert(false, $"{droppedObj.name} 오브젝트는 ICollectable이 아닙니다.");
    //    }
    //}



}

