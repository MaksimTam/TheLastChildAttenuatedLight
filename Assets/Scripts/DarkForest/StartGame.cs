using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private GameObject Alie;
    public GameObject objectToFollow; // Объект, который будет следовать над головой игрока

    private bool isPlayerInside; // Флаг, указывающий, находится ли игрок в триггере


    private Renderer renderer;
    private Material originalMaterial;
    private Color originalColor;
    private bool isFadingOut = false;
    private float fadeSpeed = 2.6f; // Скорость плавного исчезновения


    private void Start()
    {
        // Получаем рендерер и оригинальный материал целевого объекта
        renderer = objectToFollow.GetComponent<Renderer>();
        originalMaterial = renderer.material;
        originalColor = renderer.material.color;
    }
    private void Update()
    {
        Alie = GameObject.FindGameObjectWithTag("Player").gameObject;
        Alie.GetComponent<Transform>();
        if (isFadingOut)
        {
            Color newColor = renderer.material.color;
            newColor.a -= fadeSpeed * Time.deltaTime;

            // Устанавливаем новую прозрачность материала целевого объекта
            renderer.material.color = newColor;

            // Если объект полностью прозрачен, удаляем его
            if (newColor.a <= 0f)
            {
                Destroy(objectToFollow);
            }
        }
        if (isPlayerInside)
        {
            // Получаем позицию игрока
            Vector3 playerPosition = Alie.transform.position;

            // Устанавливаем позицию объекта для следования над головой игрока
            objectToFollow.transform.position = new Vector3(playerPosition.x, playerPosition.y + 2f, playerPosition.z);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            isFadingOut = true;

        }
    }
}