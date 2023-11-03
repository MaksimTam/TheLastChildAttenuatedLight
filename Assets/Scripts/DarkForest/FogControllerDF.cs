using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogControllerDF : MonoBehaviour
{
    public float fogStartValue = 18f; // Начальное значение fog.end
    public float fogEndValue = 26f; // Конечное значение fog.end
    public float duration = 2f; // Продолжительность изменения fog.end

    private bool isInTrigger = false;
    private float elapsedTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = true;
            elapsedTime = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = false;
            elapsedTime = 0f;
        }
    }

    private void Update()
    {
        if (isInTrigger)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            RenderSettings.fogEndDistance = Mathf.Lerp(fogStartValue, fogEndValue, t);
        }
        else
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            RenderSettings.fogEndDistance = Mathf.Lerp(fogEndValue, fogStartValue, t);
        }
    }
}
