using UnityEngine;
using UnityEngine.EventSystems;

public class Trashcan : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool IsDragging { get; private set; }
    public DraggableUIElement forwardEventsToElement;

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (forwardEventsToElement != null)
        {
            forwardEventsToElement.OnDrag(eventData);   
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsDragging = false;
    }
}