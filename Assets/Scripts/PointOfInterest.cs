using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{

    private BoxCollider2D _box;
    // Start is called before the first frame update
    void Start()
    {
        _box = gameObject.GetComponent<BoxCollider2D>();
    }

    public Vector3 GetRandomPointWithinCollider()
    {
        var size = _box.size;
        var position = (Vector2)transform.position + _box.offset;
        var x = Random.Range(position.x - (size.x / 2), position.x + (size.x / 2));
        var y = Random.Range(position.y - (size.y / 2), position.y + (size.y / 2));

        return new Vector3(x, y);
    }
}
