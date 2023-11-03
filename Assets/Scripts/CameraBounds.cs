using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public static CinemachineVirtualCamera virtualCamera;
    public Transform player;

    private void Update()
    {
        virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Останавливаем следование камеры за игроком
            virtualCamera.Follow = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Возобновляем следование камеры за игроком
            virtualCamera.Follow = player;
        }
    }
}
