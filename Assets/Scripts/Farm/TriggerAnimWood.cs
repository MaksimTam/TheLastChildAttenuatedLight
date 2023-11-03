using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimWood : MonoBehaviour
{
    public GameObject[] objectsArray; // Массив объектов
    public GameObject Trigger;
    public GameObject EffectDirt;
    public AudioSource Wood;


    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;//включаем у риджидбоди синематик дабы наш объект не разрушался раньше времени
        EffectDirt.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Проходимся по каждому объекту в массиве
            foreach (GameObject obj in objectsArray)
            {
                // Получаем компонент Rigidbody объекта
                Rigidbody rigidbody = obj.GetComponent<Rigidbody>();

                // Если компонент Rigidbody присутствует
                if (rigidbody != null)
                {
                    // Отключаем параметр kinematic
                    rigidbody.isKinematic = false;
                }
                else
                {
                    Debug.LogWarning("Obj " + obj.name + " doesn't have a Rigidbody component.");
                }
            }
            Wood.Play();
            EffectDirt.SetActive(true);
            Destroy(EffectDirt, 2f);
            Destroy(Wood, 2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(Trigger);

        }
    }
}