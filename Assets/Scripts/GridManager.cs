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

    public Vector2Int GetGridPos(Vector3 mousePos)
    {
        var tilePosX = Mathf.FloorToInt((mousePos.x - xStart) / (tileSize + spacing));
        var tilePosY = Mathf.FloorToInt((mousePos.y - yStart) / (tileSize + spacing));

        return new Vector2Int(tilePosX, tilePosY);
    }
    
    public void RemoveFromGrid(Vector3 mousePos, int numOfTilesX, int numOfTilesY)
    {
        PlaceInGrid(mousePos, null, numOfTilesX, numOfTilesY, false);
    }

    public void RemoveFromGridByGridPos(Vector2Int gridPos, int numOfTilesX, int numOfTilesY)
    {
        PlaceInGridByGridPos(gridPos, null, numOfTilesX, numOfTilesY);
    }
    
    public GameObject GetTileFromGridPos(Vector2Int gridPos)
    {
        if (gridPos.x < 0 || gridPos.y < 0 || gridPos.x > width-1 || gridPos.y > height-1)
        {
            return null;
        }
        
        return GridArray[gridPos.x, gridPos.y];
    }
    
    public GameObject GetTileFromMousePos(Vector3 mousePos)
    {   
        var tilePos = GetGridPos(mousePos);

        return GetTileFromGridPos(tilePos);
    }

    public void PlaceInGridByGridPos(Vector2Int tilePos,GameObject objectToPlace, int numOfTilesX, int numOfTilesY, bool safe = true)
    {
        var tilePosY = tilePos.y;
        for (var y = 0; y < numOfTilesY; y++)
        {
            var tilePosX = tilePos.x;
            for (var x = 0; x < numOfTilesX; x++)
            {
                var tile = GetTileFromGridPos(new Vector2Int(tilePosX, tilePosY));
                
                if (!safe && tile == null) continue;
                
                var tileScript = tile.GetComponent<Tile>();

                tileScript.objectInTile = objectToPlace;
                

                tilePosX++;
            }
            tilePosY++;
        }
    }
    
    /// <summary>
    /// Sets the object in the grid with the correct number of tiles.
    /// Call first IsTileFree, to see if it is safe to place
    /// </summary>
    /// <param name="mousePos"></param>
    /// <param name="objectToPlace"></param>
    /// <param name="numOfTilesX"></param>
    /// <param name="numOfTilesY"></param>
    public void PlaceInGrid(Vector3 mousePos, GameObject objectToPlace, int numOfTilesX, int numOfTilesY, bool safe = true)
    {
        var tilePos = GetGridPos(mousePos);
        PlaceInGridByGridPos(tilePos, objectToPlace, numOfTilesX, numOfTilesY, safe);
    }
    
    public bool IsTileFree(Vector3 mousePos, int numOfTilesX, int numOfTilesY)
    {
        var tilePos = GetGridPos(mousePos);
        
        var tilePosY = tilePos.y;
        for (var y = 0; y < numOfTilesY; y++)
        {   
            var tilePosX = tilePos.x;
            for (var x = 0; x < numOfTilesX; x++)
            {
                var tile = GetTileFromGridPos(new Vector2Int(tilePosX, tilePosY));

                if (tile == null) return false;

                var tileScript = tile.GetComponent<Tile>();

                if (tileScript.objectInTile != null) return false;
                

                tilePosX++;
            }

            tilePosY++;
        }

        return true;
    }
    
    private void Update()
    {
        if (!testing) return;
        if (Input.GetButtonDown("Fire1"))
        {
            var clickedTile = GetTileFromMousePos(GameManager.GetMouseTo2DWorldPos());
            
            if (clickedTile == null) return;
            
            var lineRenderer = clickedTile.GetComponent<LineRenderer>();
            
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
            
        }
    }
}
