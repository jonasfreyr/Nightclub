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

    private bool _placed = false;
    private Vector2Int _tilePos;
    
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
        var mousePos = GameManager.GetMouseTo2DWorldPos();
        
        if (Input.GetButtonDown("Fire1"))
        {
            var currentMousePos = GameManager.GetMouseTo2DWorldPos();
            if (_collider.bounds.Contains(currentMousePos) && GameManager.Instance.draggingObject == null)
            {
                _offset = currentMousePos - transform.position;
                _offset.z = 0;

                _isDragging = true;
                GameManager.Instance.draggingObject = gameObject;
                
                
                GameManager.Instance.gridManager.RemoveFromGridByGridPos(_tilePos, tilesX, tilesY);
            }
        }
        
        if (!_isDragging) return;
        
        var tile = GameManager.Instance.gridManager.GetTileFromMousePos(mousePos);
        
        if (tile == null)
        {
            mousePos -= _offset;
            transform.position = mousePos;
            return;
        }

        if (!GameManager.Instance.gridManager.IsTileFree(mousePos, tilesX, tilesY))
        {

            mousePos -= _offset;
            transform.position = mousePos;
            return;
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
            GameManager.Instance.gridManager.PlaceInGrid(mousePos, gameObject, tilesX, tilesY);
            _tilePos = GameManager.Instance.gridManager.GetGridPos(mousePos);
            _placed = true;
        }
    }
}
