using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableUIElement : MonoBehaviour, IDragHandler
{
    public bool allowDragging = true;
    
    public void OnDrag (PointerEventData eventData)
    {
        if (allowDragging)
        {
            transform.position += (Vector3)eventData.delta;   
        }
    }
}
