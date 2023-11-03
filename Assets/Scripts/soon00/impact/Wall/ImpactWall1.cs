using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactWall1 : MonoBehaviour
{
    public static bool KinWall;
    private bool kintrue;

    private bool kill;
    public bool isdead = false; //переменная которая обозначает разрушился объект, или еще нет
    public float timeRemaining;
    public GameObject Opors;//время после которого должен удалится объект после разрушения (сделано во благо оптимизации)

    public float fadeDuration = 2f; // Время, за которое происходит плавное изменение прозрачности
    private float currentAlpha = 1f; // Текущая прозрачность (изначально 1 - непрозрачность)

    private Material material; // Ссылка на компонент Material

    void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;//включаем у риджидбоди синематик дабы наш объект не разрушался раньше времени
        kill = false;
        KinWall = false;
        // Получаем компонента Material объекта
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        // Проверяем, есть ли у объекта тег "Impcat1"
    }

    void Update()
    {
        if (KinWall == true || Imapct2Trigger.TriggerTrue == true)
        {
            Invoke("Trigger", 0f);
            GetComponent<Rigidbody>().isKinematic = false;//и если он с чем-то столкнулся, отключаем синематик тем самым разрушая его
            isdead = true;
            Destroy(Opors);
            Trigger();
            kintrue = true;
            //делаем переменную положительной, чтобы скрипт смог понять что обьект уже "отработан", и его можно удалить
        }
        if (kintrue == true)
        {
            KinWall = false;
        }

        if (isdead)//если переменная положительная, то запускаем таймер 
        {
            timeRemaining -= Time.deltaTime;//сам таймер			

            if (timeRemaining < 0) //и если время таймера меньше нуля, то 
            {
                // Выключение эмиссии
                // Вычисляем новое значение прозрачности на основе времени
                float newAlpha = currentAlpha - Time.deltaTime / fadeDuration;
                currentAlpha = Mathf.Clamp01(newAlpha); // Ограничиваем прозрачность, чтобы она не была меньше 0 и больше 1

                // Устанавливаем новое значение прозрачности в материале объекта
                Color color = material.color;
                color.a = currentAlpha;
                material.color = color;
                // Проверяем условие выполнено или нет             
                Destroy(gameObject, 3f);//просто удаляем объект                
            }
        }
    }
    private void Trigger()
    {
        kill = true;
    }
}