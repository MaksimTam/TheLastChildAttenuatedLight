using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField]private Transform player;

    [SerializeField] private Animator playerAnimator;
    public Transform rope;
    public Transform ropeStartPoint;
    public Transform ropeEndPoint;
    public float climbingSpeed = 5f;
    public GameObject Barier;

    private bool isClimbing = false;
    private bool isMousePressed = false;

    private void Start()
    {
        // Отключаем player при старте
        player.gameObject.SetActive(false);
        Barier.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // При столкновении с триггер зоной активируем player и начинаем лазать
        if (other.CompareTag("Player"))
        {
            player.gameObject.SetActive(true);
            isClimbing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Когда player покидает триггер зону, выключаем лазание
        if (other.CompareTag("Player"))
        {
            isClimbing = false;
        }
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerAnimator = player.GetComponent<Animator>();
        // Проверяем нажатие мыши
        if (Input.GetMouseButton(0))
        {
            isMousePressed = true;
        }
        else
        {
            isMousePressed = false;
        }

        // Если лазание активно и мышь нажата, двигаем player по веревке
        if (isClimbing && isMousePressed)
        {
            // Получаем направление движения по веревке
            Vector3 ropeDirection = (ropeEndPoint.position - ropeStartPoint.position).normalized;

            // Двигаем player вверх по веревке
            player.position += ropeDirection * climbingSpeed * Time.deltaTime;
            playerAnimator.SetBool("isClimbing", true);
            Barier.SetActive(true);
        }
        else
        {
            Barier.SetActive(false);
        }    
    }
}





