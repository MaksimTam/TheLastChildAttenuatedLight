using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sit : MonoBehaviour
{
    public GameObject Text;
    private Animator TextAnim;

    private void Start()
    {
        TextAnim = Text.GetComponent<Animator>();
        TextAnim.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            TextAnim.enabled = true;
            TextAnim.SetTrigger("Start");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TextAnim.SetTrigger("End");
        }
    }
}
