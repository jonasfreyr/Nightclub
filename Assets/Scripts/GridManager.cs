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
    
    // Start is called before the first frame update
    void Start()
    {
        GridArray = new GameObject[width, height];
        
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var newTile = Instantiate(tilePrefab, new Vector3(xStart + (tileSize * x), yStart + tileSize * y), Quaternion.identity, transform);

                var tileScript = newTile.GetComponent<Tile>();
                tileScript.Set();
                tileScript.tileSize = tileSize;

                GridArray[x, y] = newTile;
            }
        }
    }

    private GameObject GetTileClicked()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        var tilePosX = Mathf.FloorToInt((mousePos.x - xStart) / tileSize);
        var tilePosY = Mathf.FloorToInt((mousePos.y - yStart) / tileSize);
            
        Debug.Log(tilePosX);
        Debug.Log(tilePosY);

        return GridArray[tilePosX, tilePosY];
    }
    
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var clickedTile = GetTileClicked();

            var _linerenderer = clickedTile.GetComponent<LineRenderer>();
            
            _linerenderer.startColor = Color.green;
            _linerenderer.endColor = Color.green;
            
        }
    }
}
