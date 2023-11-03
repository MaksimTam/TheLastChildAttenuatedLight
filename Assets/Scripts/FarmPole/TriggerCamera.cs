using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCamera : MonoBehaviour
{
    public GameObject Camera;
    public GameObject CameraStart;

    private void Start()
    {
        Camera.SetActive(false);
        CameraStart.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.SetActive(true);
            CameraStart.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.SetActive(false);
            CameraStart.SetActive(true);
        }
    }
}
