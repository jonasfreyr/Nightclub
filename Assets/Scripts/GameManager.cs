using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GridManager gridManager;
    public GameObject draggingObject = null;
    private static Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Awake()
    {
        Instance = this;
    }

    public static Vector3 GetMouseTo2DWorldPos()
    {
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        return mousePos;
 
    }
}
