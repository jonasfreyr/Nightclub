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
    public float spacing = 0.2f;
    public float lineThickness = 0.0001f;

    private bool testing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GridArray = new GameObject[width, height];
        
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var newTile = Instantiate(tilePrefab, new Vector3(xStart + (tileSize * x) + x*spacing, yStart + tileSize * y + y*spacing), Quaternion.identity, transform);

                var tileScript = newTile.GetComponent<Tile>();
                tileScript.tileSize = tileSize;
                tileScript.lineThickness = lineThickness;
                tileScript.Set();

                GridArray[x, y] = newTile;
            }
        }
    }

    public GameObject GetTileFromPos(Vector3 mousePos)
    {
        var tilePosX = Mathf.FloorToInt((mousePos.x - xStart) / (tileSize + spacing));
        var tilePosY = Mathf.FloorToInt((mousePos.y - yStart) / (tileSize + spacing));
        
        if (tilePosX < 0 || tilePosY < 0)
        {
            return null;
        }
        
        return GridArray[tilePosX, tilePosY];
    }
    
    private void Update()
    {
        if (!testing) return;
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
