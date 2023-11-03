using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class DoorOpenArena : MonoBehaviour
{
    public Animator DoorOpen;
    public GameObject TextEnter;
    public Animator Text;
    public AudioSource AudioDoor;

    private void Start()
    {
        DoorOpen.enabled = false;
        TextEnter.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        // При столкновении с триггер зоной активируем player и начинаем лазать
        if (other.CompareTag("Player"))
        {
            AudioDoor.Play();
            DoorOpen.enabled = true;
            TextEnter.SetActive(true);
            Text.SetBool("isInvite", true);
#pragma warning disable UNT0016 // Unsafe way to get the method name
            Invoke("OnAnim", 1f);
#pragma warning restore UNT0016 // Unsafe way to get the method name
            Destroy(AudioDoor, 4f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Когда player покидает триггер зону, выключаем лазание
        if (other.CompareTag("Player"))
        {
            DoorOpen.enabled = false;
        }
    }
    public void OnAnim()
    {
        Text.SetBool("isInvite", false);
        Text.SetBool("isStatic", true);
    }
}
