using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleTile : MonoBehaviour
{
    public BoxCollider2D collider;

    public bool placed;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }
}
