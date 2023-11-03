using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactWall4 : MonoBehaviour
{
    public bool Kin;
    private bool kintrue;

    public GameObject TriggerEffect;
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
        // Получаем компонента Material объекта
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        TriggerEffect.SetActive(false);
        // Проверяем, есть ли у объекта тег "Impcat1"
    }

    void Update()
    {
        if (Kin == true)
        {
            Invoke("Trigger", 0f);
            GetComponent<Rigidbody>().isKinematic = false;//и если он с чем-то столкнулся, отключаем синематик тем самым разрушая его
            isdead = true;
            Destroy(Opors);
            Trigger();
            Invoke("TrueTrigger", 1f);
            kintrue = true;
            //делаем переменную положительной, чтобы скрипт смог понять что обьект уже "отработан", и его можно удалить
        }
        if (kintrue == true)
        {
            Kin = false;
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
        TriggerEffect.SetActive(true);
    }
    private void TrueTrigger()
    {
        Destroy(TriggerEffect);
    }
}