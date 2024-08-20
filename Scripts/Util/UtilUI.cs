using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UtilUI : MonoBehaviour
{
    // 마우스포인터가 있는 곳에 특정 UI오브젝트가 있는지 bool값을 반환하는 메서드.
    // 주의사항: uiObject의 RectTransform의 Width, Height를 따름,  
    public static bool IsPointerOverSpecificUI(GameObject uiObject, PointerEventData eventData)
    {
        if (uiObject != null)
        {
            GraphicRaycaster raycaster = UtilGeneric.FindTInHierarchy<GraphicRaycaster>(uiObject);//uiObject.GetComponentInParent<GraphicRaycaster>();
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(eventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject == uiObject)
                {
                    return true;
                }
            }
            return false;
        }
        return false;
    }
}
