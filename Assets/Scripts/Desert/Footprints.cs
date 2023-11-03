using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Footprints : MonoBehaviour
{
    public GameObject footprintPrefab;
    public GameObject footprintStopPrefab;
    public float footprintInterval;
    public float footprintDuration;

    private bool PlayMoving;
    private Rigidbody rb;
    private float nextFootprintTime;
    private bool conditionMet = false; // Флаг, указывающий, было ли условие выполнено
    public GameObject player;

    public GameObject JumpEffect;
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
        JumpEffect.GetComponent<ParticleSystem>();
        if (player != null)
        {
            // Проверяем, если скорость равна 0 по оси X
            if (rb.velocity.x == 0f)
            {
                // Произойдет, если скорость равна 0 по оси X
                PlayMoving = false;
            }
            else
            {
                // Произойдет, если скорость не равна 0 по оси X
                PlayMoving = true;
            }

        }
        if (player != null)
        {
            float playerVelocityX = player.GetComponent<Rigidbody>().velocity.x;
            float playerVelocityY = player.GetComponent<Rigidbody>().velocity.y;
            if (PlayMoving == true)
            {
                if (playerVelocityX > 1 && playerVelocityY < 1.1)
                {
                    // Создание следа, если достигнуто время
                    if (Time.time >= nextFootprintTime)
                    {
                        CreateFootprint(player.transform.position);
                        nextFootprintTime = Time.time + footprintInterval;
                    }

                }
                if (playerVelocityX < -1 && playerVelocityY < 1.1)
                {
                    // Создание следа, если достигнуто время
                    if (Time.time >= nextFootprintTime)
                    {
                        CreateFootprint(player.transform.position);
                        nextFootprintTime = Time.time + footprintInterval;
                    }

                }
             
            }
            if(playerVelocityY > 1)
            {
                Invoke("OnJumpEffect", 0.7f);
            }

        }

    }

    private void CreateFootprint(Vector3 position)
    {
        if(PersControl.isGrounded == true)
        {
            GameObject footprint = Instantiate(footprintPrefab, position, Quaternion.identity);
            Destroy(footprint, footprintDuration);
        }
    }
    private void CreateFootJumpprint(Vector3 position)
    {
        if (PersControl.isGrounded == true)
        {
            GameObject footprintJump = Instantiate(JumpEffect, position, Quaternion.identity);
            Destroy(footprintJump, footprintDuration);
        }
    }
    private void OnJumpEffect()
    {
        CreateFootJumpprint(player.transform.position);
    }
}