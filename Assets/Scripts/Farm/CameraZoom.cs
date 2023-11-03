using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    private GameObject Camera;
    private CinemachineVirtualCamera virtualCamera;
    public float zoomDistance = 5f;
    public float rotationValue = 10f;

    private float originalZoomDistance;
    private float originalRotationValue;

    private void Start()
    {
        originalZoomDistance = virtualCamera.m_Lens.FieldOfView;
        originalRotationValue = virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.m_MaxSpeed;
    }
    private void Update()
    {
        Camera = GameObject.FindGameObjectWithTag("Cinemachine");
        virtualCamera = Camera.GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Изменяем значение приближения камеры
            virtualCamera.m_Lens.FieldOfView = zoomDistance;

            // Изменяем значение поворота камеры по оси X
            virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.m_MaxSpeed = rotationValue;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Возвращаем камеру в исходное состояние при выходе из триггера
            virtualCamera.m_Lens.FieldOfView = originalZoomDistance;
            virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.m_MaxSpeed = originalRotationValue;
        }
    }
}

