using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFarm : MonoBehaviour
{
    public Animator Dead;
    public GameObject animatedObject;
    public Animator animator;
    [SerializeField] public static bool play;
    public AudioSource Door;
    public GameObject Trigger;
    public AudioSource WoodHit;
    public GameObject Camera;

    public GameObject[] Wood;

    private void Start()
    {
        Dead.enabled = false;
        animator.enabled = false;
        Camera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (play == true && other.CompareTag("Player"))
        {
            animator.enabled = true;
            Door.Play();
            Trigger.SetActive(false);
#pragma warning disable UNT0016 // Unsafe way to get the method name
            Invoke("WoodAnim", 2);
#pragma warning restore UNT0016 // Unsafe way to get the method name
        }
    }
    private void WoodAnim()
    {
        // Проходимся по каждому объекту в массиве
        foreach (GameObject obj in Wood)
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
        Dead.enabled = true;
        WoodHit.Play();
        Destroy(WoodHit, 2f);
        Camera.SetActive(true);
        Destroy(Camera, 4f);
    }
}


