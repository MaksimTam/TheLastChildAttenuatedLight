using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public GameObject Impuls;
    public GameObject Text;
    private Animator TextAnim;
    public GameObject UI;
    public GameObject objectToSpawn;
    private GameObject spawnedObject;
    private Vector3 playerPosition;
    private bool isMoving = false;

    private void Start()
    {
        Impuls.SetActive(false);
        TextAnim = Text.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Impuls.SetActive(true);
            TextAnim.SetTrigger("Enter");
            // Сохраняем позицию игрока
            playerPosition = other.transform.position;

            // Проверяем, начинает ли игрок двигаться
            InvokeRepeating("CheckMovement", 0.5f, 0.5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TextAnim.SetTrigger("Exit");
        }
    }

    private void CheckMovement()
    {
        // Проверяем, стоит ли игрок на месте
        if (!isMoving && spawnedObject == null && ClickColorEmissionForest.Click == true)
        {
            // Создаем и позиционируем объект
            UI.SetActive(false);
            objectToSpawn.SetActive(true);
        }
        else if (isMoving && spawnedObject != null)
        {
            // Удаляем объект
            Destroy(spawnedObject);
            spawnedObject = null;
        }

        // Сбрасываем флаг движения игрока
        isMoving = false;
    }

    private void LateUpdate()
    {
        // Проверяем, двигается ли игрок
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            isMoving = true;
        }
    }
}