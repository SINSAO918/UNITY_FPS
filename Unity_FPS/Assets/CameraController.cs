using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //ī�޶� X�� ȸ�� �������� ��
    readonly float FPS_MIN_rotateY = -75;
    readonly float FPS_MAX_rotateY = 70;
    readonly float TPS_MIN_rotateY = 15;
    readonly float TPS_MAX_rotateY = 50;

    public float currentRotateX { get; private set; }
    public float currentRotateY { get; private set; }

    public enum CameraMode { FPS, TPS}
    public CameraMode cameraMode;

    [SerializeField] float mouseSensitivity = 1; //���콺 ����

    Camera mainCamera;

    private void Awake()
    {
        //����ī�޶� ĳ��
        mainCamera = Camera.main;
        mainCamera.transform.position = transform.position;
        mainCamera.transform.SetParent(transform);
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

    public void SetCameraMode(CameraMode mode)
    {
        if (cameraMode == mode) return; //�ߺ�ó�� �ȵǰ�
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

            float cameraDistance = 5; //�ӽ÷� ������ ������
            mainCamera.transform.position = new Vector3(
                Mathf.Sin(currentRotateX * Mathf.Deg2Rad) * Mathf.Cos(currentRotateY * Mathf.Deg2Rad) * cameraDistance,
                Mathf.Sin(currentRotateY * Mathf.Deg2Rad) * cameraDistance,
                Mathf.Cos(currentRotateX * Mathf.Deg2Rad) * Mathf.Cos(currentRotateY * Mathf.Deg2Rad) * cameraDistance
            );
        }
    }
}