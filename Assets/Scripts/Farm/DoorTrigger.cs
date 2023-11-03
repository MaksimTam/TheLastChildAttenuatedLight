using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public float yThreshold = -0.7f;
    private AudioSource audioSource;
    private bool isAudioEnabled = false;

    private void Start()
    {
        // Получаем компонент AudioSource для данного объекта
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Проверяем, достиг ли объект указанной позиции по Y
        if (transform.position.y <= yThreshold && !isAudioEnabled)
        {
            // Включаем AudioSource
            audioSource.Play();
            isAudioEnabled = true;
            Lock.Finish00 = true;
            Debug.Log("Click");
        }
    }
}
