using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SubRegulationBook : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    public void ReturnRegulationPosition()
    {
        transform.localPosition = new Vector3(185f, -530f, 0f);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        RegulationBookController.isDraggingHandled = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (FindMousePlace.instance.inSubDesk)
        {
            ReturnRegulationPosition();
        }
        RegulationBookController.isDraggingHandled = false;
    }
}
