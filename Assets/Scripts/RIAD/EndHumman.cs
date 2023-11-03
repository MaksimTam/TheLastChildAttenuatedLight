using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndHumman : MonoBehaviour
{
    public bool Play;

    public GameObject Humman;
    private Animator HummanAnim;

    public GameObject Text;
    private Animator TextAnim;

    public GameObject Door;
    private Animator DoorAnim;

    public GameObject Black;
    private Animator BlackAnim;

    private void Start()
    {
        HummanAnim = Humman.GetComponent<Animator>();
        TextAnim = Text.GetComponent<Animator>();
        DoorAnim = Door.GetComponent<Animator>();
        BlackAnim = Black.GetComponent<Animator>();
        BlackAnim.enabled = true;
        DoorAnim.enabled = false;
        TextAnim.enabled = false;
        Black.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Play == true)
        {
            Black.SetActive(true);
            BlackAnim.SetTrigger("End");
            Invoke("Anim", 1f);
            HummanAnim.SetTrigger("Walk");
            HummanAnim.SetTrigger("Open");
            HummanMovement.isMoving = true;
        }
        if (other.CompareTag("Player") && Play == false)
        {
            HummanAnim.SetTrigger("LoopBlock");
            TextAnim.SetTrigger("Start");
            TextAnim.enabled = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HummanAnim.SetTrigger("Iddle");
            TextAnim.SetTrigger("Exit");
        }
    }
    private void Anim()
    {
        DoorAnim.enabled = true;
    }
}