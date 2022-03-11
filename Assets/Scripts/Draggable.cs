using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Collider2D _collider;
    private bool _isDragging;
    private Vector3 _offset;
    
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log(GameManager.Instance.draggingObject);
            
            var currentMousePos = GameManager.GetMouseTo2DWorldPos();
            if (_collider.bounds.Contains(currentMousePos) && GameManager.Instance.draggingObject == null)
            {
                _offset = currentMousePos - transform.position;
                _offset.z = 0;
        
                _isDragging = true;
                GameManager.Instance.draggingObject = gameObject;
            }
        }
        
        if (!_isDragging) return;
        
        var mousePos = GameManager.GetMouseTo2DWorldPos();

        var tile = GameManager.Instance.gridManager.GetTileFromPos(mousePos);

        if (tile == null)
        {
            mousePos -= _offset;
            transform.position = mousePos;
        }
        else
        {   
            var tilePos = tile.transform.position;
            var tileSizeHalf = GameManager.Instance.gridManager.tileSize / 2;

            tilePos.y += tileSizeHalf;
            tilePos.x += tileSizeHalf;

            transform.position = tilePos;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            _isDragging = false;
            GameManager.Instance.draggingObject = null;
        }
    }
}
