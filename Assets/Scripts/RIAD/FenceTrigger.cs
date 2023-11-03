using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceTrigger : MonoBehaviour
{
    public Material normalMaterial;
    public Material transparentMaterial;
    private bool isPlayerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            SetTransparentMaterial();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            SetNormalMaterial();
        }
    }

    private void SetTransparentMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = transparentMaterial;
        Color color = renderer.material.color;
        color.a = 0f; // Можно изменять значение для разной степени прозрачности
        renderer.material.color = color;
    }

    private void SetNormalMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = normalMaterial;
    }
}