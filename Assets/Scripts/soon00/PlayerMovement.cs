using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Скорость перемещения игрока
    public Vector3 targetPosition; // Определенные координаты, куда нужно переместить игрока
    
    public static  bool isMoving = false; // Флаг, определяющий, перемещается ли игрок

    private void Update()
    {
        if (isMoving)
        {
            // Вычисляем новую позицию игрока с учетом скорости и фреймрейта
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            
            // Применяем новую позицию к игроку
            transform.position = newPosition;

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

