using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Rigidbody rb;
    private bool isMoving;

    private float currentRotation;
    private float targetRotation;
    private float rotationVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isMoving = false;

        currentRotation = -5f;
        targetRotation = -90f;
        rotationVelocity = -90f;
    }

    private void Update()
    {
        // Проверяем, двигается ли объект по оси X
        if (rb.velocity.x != 0)
        {
            // Если движение начинается, включаем поворот
            if (!isMoving)
            {
                isMoving = true;
                targetRotation = -180f; // Желаемый угол поворота
            }
        }
        else
        {
            // Если движение останавливается, выключаем поворот
            if (isMoving)
            {
                isMoving = false;
                targetRotation = 0f; // Начальный угол поворота
            }
        }

        // Плавный поворот объекта по оси Z
        currentRotation = Mathf.SmoothDampAngle(currentRotation, targetRotation, ref rotationVelocity, 4f);
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }
}