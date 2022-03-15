using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform realCamera;
    public bool reversedMovement = true;
    public float moveSpeed = 0.5f;

    private CinemachineConfiner _cameraConfiner;
    private CinemachineVirtualCamera _virtualCamera;
    private float RealMoveSpeed => reversedMovement ? -moveSpeed : moveSpeed;

    private void Awake()
    {
        _cameraConfiner = GetComponent<CinemachineConfiner>();
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            var moveX = 0f;
            var moveY = 0f;
            
            if (Input.GetAxis("Mouse X") != 0)
            {
                GetComponent<CinemachineConfiner>();
                moveX = Input.GetAxis("Mouse X") * RealMoveSpeed;
            }
            if (Input.GetAxis("Mouse Y") != 0)
            {
                moveY = Input.GetAxis("Mouse Y") * RealMoveSpeed;
            }
            
            _moveCamera(new Vector3(moveX, moveY));

            if (_cameraConfiner.CameraWasDisplaced(_virtualCamera))
            {
                transform.position = realCamera.transform.position;
            }
        }
    }

    private void _moveCamera(Vector3 moveBy)
    {
        transform.position += moveBy;
    }
}
