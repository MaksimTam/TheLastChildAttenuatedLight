using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerFog : MonoBehaviour
{
    public Color Colorfog = new Color(0.05077428f, 0.05077428f, 0.06603771f, 1f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RenderSettings.fogColor = Colorfog;
            RenderSettings.fogDensity = 0.1f;
        }
    }
}
