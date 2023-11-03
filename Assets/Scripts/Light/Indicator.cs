using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    public Image healthBarRight, healthBarLeft;
    public float healthAmountRight = 100;
    public float healthAmountLeft = 100;

    public float UIhealthAmountRight = 100;
    public float UIhealthAmountLeft = 100;

    public float secondsToEmptyLeft = 60f;
    public float secondsToEmptyRight = 60f;

    public GameObject[] UI;
    // Start is called before the first frame update
    void Start()
    {
        healthBarRight.fillAmount = healthAmountRight / 100;
        healthBarLeft.fillAmount = healthAmountLeft / 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmountLeft > 0 && PlayerAbility.Light == false)
        {
            healthAmountLeft -= 100 / secondsToEmptyLeft * Time.deltaTime;
             healthBarLeft.fillAmount = healthAmountLeft / 100;
        }
        if (healthAmountLeft <= 0 && PlayerAbility.Light == false)
        {
            // Проходимся по каждому объекту в массиве
            foreach (GameObject obj in UI)
            {
                // Получаем компонент Rigidbody объекта
                obj.SetActive(false);
            }
            PersControl.testDieUp = true;
            Invoke("RestartGame", 5f);
        }
        if (healthAmountRight > 0 && PlayerAbility.Light == false)
        {
            healthAmountRight -= 100 / secondsToEmptyRight * Time.deltaTime;
            healthBarRight.fillAmount = healthAmountRight / 100;
        }
        if (healthAmountRight <= 0 && PlayerAbility.Light == false)
        {
            // Проходимся по каждому объекту в массиве
            foreach (GameObject obj in UI)
            {
                // Получаем компонент Rigidbody объекта
                obj.SetActive(false);
            }
            PersControl.testDieUp = true;
            Invoke("RestartGame", 5f);
        }
        if (PlayerAbility.Light == true)
        {
            healthAmountLeft += 6 * secondsToEmptyLeft * Time.deltaTime;
            healthBarLeft.fillAmount = healthAmountLeft / 100;


            healthAmountRight += 6 * secondsToEmptyLeft * Time.deltaTime;
            healthBarRight.fillAmount = healthAmountRight / 100;
            PlayerAbility.Light = false;
        }
        if (healthAmountLeft > 100 && healthAmountRight > 100)
        {
            healthAmountLeft = 100;
            healthAmountRight = 100;
        }
    }
    private void RestartGame()
    {
        // Ваш код для перезапуска игры
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Дополнительные действия, если нужно
    }
}