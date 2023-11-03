using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public Transform player;
    private Animator PAnim;

    private Vector3 initialPosition;
    private Vector3 playerPreviousPosition;
    private float speed = 1.9f;
    private float speedstop = 0f;


    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PAnim = player.GetComponent<Animator>();
        if (player.position.x > playerPreviousPosition.x && ClickSit.SitPlayer == true)
        {
            MoveRight();
        }
        else if (player.position.x < playerPreviousPosition.x && ClickSit.SitPlayer == true)
        {
            MoveLeft();
        }
        else
        {
            StopMoving();
        }

        playerPreviousPosition = player.position;
    }

    private void MoveRight()
    {
        PAnim.SetBool("Boat", true);
        PAnim.SetBool("isRunning", false);
        // Двигаем объект вправо
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void MoveLeft()
    {
        PAnim.SetBool("Boat", true);
        PAnim.SetBool("isRunning", false);
        // Двигаем объект влево
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    private void StopMoving()
    {
        PAnim.SetBool("Boat", false);
        PAnim.SetBool("isRunning", false);
        PAnim.SetTrigger("SitSoon");
        // Останавливаем движение объекта
        transform.Translate(Vector3.forward * speedstop * Time.deltaTime);
    }
}
