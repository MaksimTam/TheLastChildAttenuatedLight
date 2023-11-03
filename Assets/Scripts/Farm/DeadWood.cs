using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadWood : MonoBehaviour
{
    // Координаты для респауна игрока
    public Transform respawnPoint;
    public GameObject Black;
    public Animator BlackAnim;
    private bool isPlayerDead = false;


    private void Start()
    {
        Black.SetActive(false);
        BlackAnim.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Проверяем, не умер ли игрок уже
            if (!isPlayerDead)
            {
                // Установить флаг смерти игрока и начать процесс респауна
                isPlayerDead = true;
                Black.SetActive(true);
                // Вызываем метод для запуска процесса респауна после 2 секунд
                Invoke("RespawnPlayer", 1f);

            }
        }
    }

    private void RespawnPlayer()
    {
        // Возвращаем игрока на изначальные координаты для респауна
        GameObject.FindGameObjectWithTag("Player").transform.position = respawnPoint.position;
        // Добавьте здесь код для восстановления здоровья, очков и других значений игрока, если требуется

        // Установить флаг смерти обратно в false, чтобы игрок не мог мгновенно умереть повторно при повторном попадании в триггер зону
        isPlayerDead = false;
        Invoke("onBlack", 3f);
    }

    private void onBlack()
    {
        BlackAnim.enabled = true;
        Invoke("onBlackAnim", 2f);
    }
    private void onBlackAnim()
    {
        Black.SetActive(false);
    }
}

