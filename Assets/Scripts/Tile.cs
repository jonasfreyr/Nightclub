using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float tileSize;
    private LineRenderer _lineRenderer;
    public GameObject objectInTile;
    
    public void Set()
    {
        _lineRenderer = gameObject.GetComponent<LineRenderer>();
        var points = new Vector3[_lineRenderer.positionCount];

        var position = transform.position;
        var x = position.x;
        var y = position.y;
        
        
       points[0] = new Vector3(x, y, 0);
       points[1] = new Vector3(x + tileSize, y, 0);
       points[2] = new Vector3(x + tileSize, y + tileSize, 0);
       points[3] = new Vector3(x, y + tileSize, 0);
       
       /*
         points[0] = new Vector3(x - tileSize/2, y - tileSize/2, 0);
         points[1] = new Vector3(x + tileSize/2, y - tileSize/2, 0);
         points[2] = new Vector3(x + tileSize/2, y + tileSize/2, 0);
         points[3] = new Vector3(x - tileSize/2, y + tileSize/2, 0);
       */
        _lineRenderer.SetPositions(points);
    }
}
