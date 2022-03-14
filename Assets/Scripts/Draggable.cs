using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{

    public int tilesX = 1;
    public int tilesY = 1;

    private float _width;
    private float _height;
    
    private Collider2D _collider;
    private bool _isDragging;
    private Vector3 _offset;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();

        _width = ((tilesX - 1) / 2.0f) * GameManager.Instance.gridManager.tileSize;
        _height = ((tilesY - 1) / 2.0f) * GameManager.Instance.gridManager.tileSize;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var currentMousePos = GameManager.GetMouseTo2DWorldPos();
            if (_collider.bounds.Contains(currentMousePos))
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
        
        
        var tilePos = tile.transform.position;
        var tileSizeHalf = GameManager.Instance.gridManager.tileSize / 2;

        tilePos.y += tileSizeHalf + _height;
        tilePos.x += tileSizeHalf + _width;

        transform.position = tilePos;
        
        if (Input.GetButtonUp("Fire1"))
        {
            _isDragging = false;
            GameManager.Instance.draggingObject = null;
        }
    }
}
