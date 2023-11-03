using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogFinish : MonoBehaviour
{
    public float targetFogEndDistance = 40f;
    public float targetFogEndDistance1 = 51f;// Желаемое значение fogStartDistance
    public float fogChangeSpeed = 3f;
    public float fogChangeSpeed1 = 3f; // Скорость изменения fogStartDistance

    private float originalFogEndDistance;    // Исходное значение fogStartDistance
    private bool fogChanging;

    private void Start()
    {
        originalFogEndDistance = RenderSettings.fogEndDistance;
        fogChanging = false;
    }

    private void Update()
    {
        if (fogChanging == true)
        {
            float currentFogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, targetFogEndDistance, fogChangeSpeed * Time.deltaTime);
            RenderSettings.fogEndDistance = currentFogEndDistance;

            // Проверяем, достигло ли значение fogStartDistance целевого значения
            if (Mathf.Approximately(currentFogEndDistance, targetFogEndDistance))
            {
                RenderSettings.fogEndDistance = currentFogEndDistance;
            }
        }
        if (fogChanging == false)
        {
            float currentFogEndDistance2 = Mathf.Lerp(RenderSettings.fogEndDistance, targetFogEndDistance1, fogChangeSpeed1 * Time.deltaTime);
            RenderSettings.fogEndDistance = currentFogEndDistance2;

            // Проверяем, достигло ли значение fogStartDistance целевого значения
            if (Mathf.Approximately(currentFogEndDistance2, targetFogEndDistance1))
            {
                RenderSettings.fogEndDistance = currentFogEndDistance2;
                Debug.Log("True");
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fogChanging = true; // Выключаем параметр fog
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fogChanging = false;
        }
    }
}