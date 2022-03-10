using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width, height;
    public int xStart, yStart;
    public GameObject[,] GridArray;
    public float tileSize;
    public GameObject tile;
    
    // Start is called before the first frame update
    void Start()
    {
        GridArray = new GameObject[width, height];
        
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var newTile = Instantiate(tile, new Vector3(xStart + (tileSize * x), yStart + tileSize * y), Quaternion.identity);

                newTile.GetComponent<Tile>().Set();
                
                GridArray[x, y] = newTile;
            }
        }
    }
}
