using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// using UnityEngine.UIElements;
using UnityEngine.UI;


public class DraggableWire : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector3 startPoint;
    Vector3 startPos;
    public Image wireEnd;
    public Image snapTo;
    public GameObject BaseObject;

    void Start() {
        // Vector2 size = wireEnd.rectTransform.sizeDelta;
        // Debug.Log(size);
        // Vector2 pixelPivot = wireEnd.sprite.pivot;
        // // Vector2 percentPivot = new Vector2(pixelPivot.x / size.x, pixelPivot.y / size.y);
        // wireEnd.rectTransform.pivot = percentPivot;
        startPoint = transform.parent.position;
        startPos = transform.position;
        Debug.Log(startPos);
    }

    public void OnDrag(PointerEventData eventData) {
        // transform.position += (Vector3)eventData.delta;

        // Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newPos = (Vector3)eventData.delta + transform.position;
        newPos.z = 0;

        // Check for connection
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(newPos, .2f);
        // foreach (Collider2D collider in colliders) {
            // Make sure not my own collider
            // if (collider.gameObject != gameObject && collider.transform.parent.tag == "Wire") {
            //     UpdateWire(collider.transform.GetChild(1).transform.position);

            //     // Check color
            //     if (transform.parent.name.Equals(collider.transform.parent.name)) {
            //         Debug.Log("snapped");
            //         Done();
            //     }
            //     return;
            // }
        // }

        UpdateWire(newPos);
    }

    public void Done() {
        Destroy(this);
    }

    public void OnEndDrag(PointerEventData eventData) {
        UpdateWire(startPos);
    }

    private void UpdateWire(Vector3 newPos) {
        // Update position
        transform.position = newPos;
        // Update Direction
        Vector3 direction = newPos - startPoint;
        transform.right = direction * transform.lossyScale.x;

        if (newPos == startPos) {
            BaseObject.transform.localScale = new Vector2(1, 1);
        }
        else {
            float dist = Vector2.Distance(startPoint, newPos);
            // wireEnd.rectTransform.sizeDelta = new Vector2(dist, wireEnd.rectTransform.sizeDelta.y);
            // wireEnd.rectTransform.localScale = new Vector2(dist, wireEnd.rectTransform.localScale.y);
            // wireEnd.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dist);
            BaseObject.transform.localScale = new Vector2(dist, BaseObject.transform.localScale.y);
        }
        // Update scale
    }
}
