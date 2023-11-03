using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLight : MonoBehaviour
{
    public static bool LightButton = false;
    private bool isButtonPressed = false;
    private bool canClick = true;
    private Image image;
    private float transparency = 1f; // Изначальная прозрачность изображения
    private Button button;

    private bool click;

    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, transparency);
    }
    private void Update()
    {
        if (click == true && SimpleSonarShader_Object.CollisionEnter == true)
        {
            transparency = Mathf.Lerp(transparency, 0f, Time.deltaTime * 5f); // Плавное изменение прозрачности до полного прозрачного значения
            image.color = new Color(image.color.r, image.color.g, image.color.b, transparency);
            button.enabled = false;
        }
        if (click == false)
        {
            transparency = Mathf.Lerp(transparency, 1f, Time.deltaTime * 5f); // Плавное изменение прозрачности до полного прозрачного значения
            image.color = new Color(image.color.r, image.color.g, image.color.b, transparency);
        }
    }
    public void OnButtonClick()
    {
        click = true;
        LightButton = true;
        Invoke("Dissible", 0.5f);
    }
    private void Dissible()
    {
        Invoke("RestoreButton", 10f);
        Invoke("ButtonDis", 13f);
        LightButton = false;
    }
    private void RestoreButton()
    {
        click = false;
    }
    private void ButtonDis()
    {
        button.enabled = true;
    }
}