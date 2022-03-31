using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Wire : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private Image _image;
    private LineRenderer _lineRenderer;
    public Canvas canvas;
    private bool _isDragStarted;

    Vector3 startPoint;
    Vector3 startPos;
    public Image wireEnd;

    void Awake() {
        _image = GetComponent<Image>();
        _lineRenderer = GetComponent<LineRenderer>();
        // _lineRenderer.sortingOrder = 4;
        // _lineRenderer.sortingLayerName = "UI";
    }

    void Update() {
        // if (_isDragStarted) {
        //     Vector2 movePos;
        //     RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //         canvas.transform as RectTransform,
        //         Input.mousePosition,
        //         canvas.worldCamera,
        //         out movePos);
            
        //     Debug.Log(canvas.transform.TransformPoint(movePos));

        //     _lineRenderer.SetPosition(0, transform.position);
        //     _lineRenderer.SetPosition(1, canvas.transform.TransformPoint(movePos));
        // }
    }

    public void OnDrag(PointerEventData eventData) {
        // transform.position += (Vector3)eventData.delta;

        // Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newPos = (Vector3)eventData.delta + transform.position;
        newPos.z = 0;

        // Check for connection
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPos, .2f);
        foreach (Collider2D collider in colliders) {
            //Make sure not my own collider
            if (collider.gameObject != gameObject && collider.transform.parent.tag == "Wire") {
                // UpdateWire(collider.transform.GetChild(1).transform.position);
                UpdateWire(collider.transform.position);

                // Check color
                if (transform.parent.name.Equals(collider.transform.parent.name)) {
                    Debug.Log("snapped");
                    Done();
                }
                return;
            }
        }

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
            // BaseObject.transform.localScale = new Vector2(1, 1);
            wireEnd.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 15);     
        }
        else {
            float dist = Vector2.Distance(startPos, newPos);
            Debug.Log(dist);
            // wireEnd.rectTransform.sizeDelta = new Vector2(dist, wireEnd.rectTransform.sizeDelta.y);
            // wireEnd.rectTransform.localScale = new Vector2(dist, wireEnd.rectTransform.localScale.y);
            if (dist >= 15) {
                wireEnd.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dist);
            }
            // BaseObject.transform.localScale = new Vector2(dist, BaseObject.transform.localScale.y);
        }
        // Update scale
    }

    public void SetColor(Color color)
    {
        _image.color = color;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        _isDragStarted = true;
    }

    // public void OnEndDrag(PointerEventData eventData) {
    //     _isDragStarted = false;
    // }
}
