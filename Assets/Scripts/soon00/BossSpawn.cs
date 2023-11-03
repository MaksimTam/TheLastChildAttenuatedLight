using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] public static bool Kin;
    private bool kintrue;
    public GameObject BossObject;
    private Animator BossAnim;
    public GameObject gameObjectMe;
    public GameObject Fog;

    public GameObject TriggerEffect;
    private bool kill;
    public bool isdead = false; //переменная которая обозначает разрушился объект, или еще нет
    public float timeRemaining;

    public float fadeDuration = 2f; // Время, за которое происходит плавное изменение прозрачности
    private float currentAlpha = 1f; // Текущая прозрачность (изначально 1 - непрозрачность)

    private Material material; // Ссылка на компонент Material
    public GameObject Opors;//время после которого должен удалится объект после разрушения (сделано во благо оптимизации)

    public Animator Battle;
    public Animator BattleContinous;
    public GameObject ImpactTriggerText;

    void Start()
    {
        ImpactTriggerText.SetActive(false);
        Battle.enabled = false;
        BattleContinous.enabled = false;
        Fog.SetActive(false);
        BossAnim = BossObject.GetComponent<Animator>();
        BossObject.SetActive(false);
        BossAnim.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;//включаем у риджидбоди синематик дабы наш объект не разрушался раньше времени
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
            Invoke("TextExit", 4f);
            Invoke("Text", 2f);
            Fog.SetActive(true);
            GetComponent<Rigidbody>().isKinematic = false;//и если он с чем-то столкнулся, отключаем синематик тем самым разрушая его
            isdead = true;
            Destroy(Opors);
            Trigger();
            kintrue = true;
            Invoke("TrueTrigger", 1f);
            BossAnim.enabled = true;
            BossObject.SetActive(true);
            if (material != null && material.HasProperty("_EmissionColor"))
            {
                material.SetColor("_EmissionColor", Color.black); // Устанавливаем эмиссию материала в черный цвет
                material.DisableKeyword("_EMISSION"); // Выключаем эмиссию материала
            }
            Invoke("Destroy", 3f);
            //делаем переменную положительной, чтобы скрипт смог понять что обьект уже "отработан", и его можно удалить
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
                Destroy(gameObject, 3f);
                //просто удаляем объект                
            }
        }
    }
    void Trigger()
    {
        TriggerEffect.SetActive(true);
    }
    void TrueTrigger()
    {
        Destroy(TriggerEffect);
        if (kintrue == true)
        {
            Kin = false;
        }
    }
    private void Destroy()
    {
        Destroy(gameObjectMe, 5f);
    }
    private void Text()
    {
        Invoke("TriggerText", 0.4f);
        Battle.enabled = true;
        BattleContinous.enabled = true;
    }
    private void TriggerText()
    {
        ImpactTriggerText.SetActive(true);
        Destroy(ImpactTriggerText, 2f);

    }
    private void TextExit()
    {
        Battle.SetTrigger("Exit");
        BattleContinous.SetTrigger("Exit1");
    }
}