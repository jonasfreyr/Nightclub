using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CrateScript : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public GameObject beerBottle;
    public BarStockMinigame parent;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        var newBottle = Instantiate(beerBottle, eventData.position, Quaternion.identity, transform.parent);

        newBottle.GetComponent<Beerbottle>().barStockMinigame = parent;
        
        eventData.pointerDrag = newBottle;

    }
    
    public void OnDrag(PointerEventData eventData)
    {
  
    }
}
