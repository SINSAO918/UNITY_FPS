using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    readonly float MIN_rotateY = -75;
    readonly float MAX_rotateY = 70;

    public float currentRotateX { get; private set; }
    public float currentRotateY { get; private set; }

    [SerializeField] float cameraSensitivity = 1;


    Camera mainCamera;

    private void Awake()
    {
        //메인카메라 캐싱
        mainCamera = Camera.main;
        mainCamera.transform.position = transform.position;
        mainCamera.transform.SetParent(transform);
    }

    private void Update()
    {
        currentRotateX += Input.GetAxis("Mouse X") * cameraSensitivity;
        currentRotateY = Mathf.Clamp(currentRotateY - Input.GetAxis("Mouse Y") * cameraSensitivity, MIN_rotateY, MAX_rotateY);
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(currentRotateX, transform.localEulerAngles.y, transform.localEulerAngles.z);
        mainCamera.transform.localRotation = Quaternion.Euler(currentRotateY, mainCamera.transform.localEulerAngles.y, mainCamera.transform.localEulerAngles.z);
    }
}