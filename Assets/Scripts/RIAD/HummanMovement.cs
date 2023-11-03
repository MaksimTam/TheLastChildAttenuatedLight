using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummanMovement : MonoBehaviour
{
    private Quaternion targetRotation; // Целевое значение поворота
    private float rotationSpeed = 180f; // Скорость поворота
    public float speed; // Скорость перемещения игрока
    public Vector3 targetPosition; // Определенные координаты, куда нужно переместить игрока

    public static bool isMoving = false; // Флаг, определяющий, перемещается ли игрок

    void Start()
    {
        // Вычисляем целевое значение поворота
        targetRotation = transform.rotation * Quaternion.Euler(0f, -180f, 0f);
    }
    private void Update()
    {
        if (isMoving)
        {
            // Вычисляем новую позицию игрока с учетом скорости и фреймрейта
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            // Применяем новую позицию к игроку
            transform.position = newPosition;
            // Поворачиваем объект к целевому значению поворота
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            // Если игрок достиг целевой позиции, останавливаем перемещение
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }

    public void MoveToPosition(Vector3 newPosition)
    {
        targetPosition = newPosition;
        isMoving = true;
    }
}