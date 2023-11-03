using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public GameObject Me;
    private Animator Anim;
    public GameObject Text;

    private void Start()
    {
        Anim = Text.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Anim.SetTrigger("Start");
            Text.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Anim.SetTrigger("End");
        }
    }
}
