using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaskaScelet : MonoBehaviour
{
    private GameObject head;

    void Update()
    {
        head = GameObject.FindGameObjectWithTag("head");

    }

    void OnMouseDown()
    {
        // Телепортируем объект на голову игрока
        head.SetActive(true);
    }
}