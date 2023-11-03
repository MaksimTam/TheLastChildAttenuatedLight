using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class ClickSit : MonoBehaviour
{
    private GameObject Player;
    private Animator PlAnim;

    private Renderer rend;
    private Color originalColor;
    private Color highlightColor;

    public float speed = 5f; // Скорость перемещения игрока
    public Vector3 targetPosition; // Определенные координаты, куда нужно переместить игрока

    public bool isMoving = false; // Флаг, определяющий, перемещается ли игрок
    public static bool SitPlayer;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.GetColor("_EmissionColor");
        highlightColor = Color.red;
    }
    private void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlAnim = Player.GetComponent<Animator>();
        if (isMoving == true)
        {
            // Вычисляем новую позицию игрока с учетом скорости и фреймрейта
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, targetPosition, speed * Time.deltaTime);

            if (Player.transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }
    void OnMouseEnter()
    {
        rend.material.SetColor("_EmissionColor", highlightColor);
    }

    void OnMouseExit()
    {
        rend.material.SetColor("_EmissionColor", originalColor);
    }
    private void OnMouseDown()
    {
        Invoke("Sit", 1.5f);
        isMoving = true;
        PlAnim.SetTrigger("Forward");
        PlAnim.SetTrigger("SitSoon");
    }
    private void Sit()
    {
        SitPlayer = true;
    }
}