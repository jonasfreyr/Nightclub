using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width, height;
    public int xStart, yStart;
    public GameObject[,] GridArray;
    public float tileSize;
    public GameObject tilePrefab;

    public float _spacing = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        GridArray = new GameObject[width, height];
        
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var newTile = Instantiate(tilePrefab, new Vector3(xStart + (tileSize * x) + x*_spacing, yStart + tileSize * y + y*_spacing), Quaternion.identity, transform);

                var tileScript = newTile.GetComponent<Tile>();
                tileScript.tileSize = tileSize;
                tileScript.Set();

                GridArray[x, y] = newTile;
            }
        }
    }

    public GameObject GetTileFromPos(Vector3 mousePos)
    {
        var tilePosX = Mathf.FloorToInt((mousePos.x - xStart) / (tileSize + _spacing));
        var tilePosY = Mathf.FloorToInt((mousePos.y - yStart) / (tileSize + _spacing));
            
        Debug.Log(tilePosX);
        Debug.Log(tilePosY);

        if (tilePosX < 0 || tilePosY < 0)
        {
            return null;
        }
        
        return GridArray[tilePosX, tilePosY];
    }
    
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var clickedTile = GetTileFromPos(GameManager.GetMouseTo2DWorldPos());
            
            if (clickedTile == null) return;
            
            var lineRenderer = clickedTile.GetComponent<LineRenderer>();
            
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
            
        }
    }
}
