using UnityEngine;
using UnityEngine.EventSystems;

public class Toilet : MonoBehaviour, IDragHandler
{
    public ToiletPlunger plunger;
    
    // Forward drag event
    public void OnDrag(PointerEventData eventData)
    {
        plunger.OnDrag(eventData);
    }
}