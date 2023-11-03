using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogTrigger : MonoBehaviour
{
    public float targetFogEndDistance = 8f;
    public float fogChangeSpeed = 3f;

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


