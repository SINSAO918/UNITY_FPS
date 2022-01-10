using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //카메라 X축 회전 범위제한 값
    readonly float FPS_MIN_rotateY = -75;
    readonly float FPS_MAX_rotateY = 70;
    readonly float TPS_MIN_rotateY = 15;
    readonly float TPS_MAX_rotateY = 50;

    public enum CameraMode { FPS, TPS}
    public CameraMode cameraMode;


    public float currentRotateX { get; private set; }
    public float currentRotateY { get; private set; }

    [SerializeField] float mouseSensitivity = 1; //마우스 감도

    Camera mainCamera;
    bool cameraLock;
    public bool CameraLock { get => cameraLock;
        set
        {
            if (value == cameraLock) return;
            if(value)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            cameraLock = value;
        }
    }

    private void Awake()
    {
        //메인카메라 캐싱
        mainCamera = Camera.main;
        mainCamera.transform.position = transform.position;
        mainCamera.transform.SetParent(transform);
        CameraLock = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            SetCameraMode(cameraMode == CameraMode.FPS ? CameraMode.TPS : CameraMode.FPS);
        InputMouse();
    }

    private void FixedUpdate()
    {
        CameraRotate();
    }


    
    /// <summary>
    /// 카메라모드 설정
    /// </summary>
    /// <param name="mode"></param>
    public void SetCameraMode(CameraMode mode)
    {
        if (cameraMode == mode) return; //중복처리 안되게
        cameraMode = mode;
        if(mode == CameraMode.FPS) mainCamera.transform.position = transform.position;
    }

    void InputMouse()
    {
        currentRotateX += Input.GetAxis("Mouse X") * mouseSensitivity;
        if(cameraMode == CameraMode.FPS)
            currentRotateY = Mathf.Clamp(currentRotateY - Input.GetAxis("Mouse Y") * mouseSensitivity, FPS_MIN_rotateY, FPS_MAX_rotateY);
        else
            currentRotateY = Mathf.Clamp(currentRotateY - Input.GetAxis("Mouse Y") * mouseSensitivity, TPS_MIN_rotateY, TPS_MAX_rotateY);
    }
    void CameraRotate()
    {
        //FPS
        if (cameraMode == CameraMode.FPS)
        {
            transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, currentRotateX, transform.localEulerAngles.z);
            mainCamera.transform.localRotation = Quaternion.Euler(currentRotateY, mainCamera.transform.localEulerAngles.y, mainCamera.transform.localEulerAngles.z);
        }
        //TPS
        else if (cameraMode == CameraMode.TPS)
        {
            mainCamera.transform.LookAt(transform);
            transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, currentRotateX, transform.localEulerAngles.z);

            float cameraDistance = 5; //임시로 지정한 변수임
            mainCamera.transform.position = transform.position + new Vector3(
                Mathf.Sin((currentRotateX + 180) * Mathf.Deg2Rad) * Mathf.Cos(currentRotateY * Mathf.Deg2Rad) * cameraDistance,
                Mathf.Sin(currentRotateY * Mathf.Deg2Rad) * cameraDistance,
                Mathf.Cos((currentRotateX+ 180) * Mathf.Deg2Rad) * Mathf.Cos(currentRotateY * Mathf.Deg2Rad) * cameraDistance
            );
        }
    }
}