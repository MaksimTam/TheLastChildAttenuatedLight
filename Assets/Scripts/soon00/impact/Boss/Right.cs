using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : MonoBehaviour
{
    public GameObject Monstr;
    private Animator MonstrAnim;

    private void Start()
    {
        MonstrAnim = Monstr.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MonstrAnim.SetTrigger("Right");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MonstrAnim.SetTrigger("RightExit");
        }
    }
}