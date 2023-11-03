using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Left : MonoBehaviour
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
            MonstrAnim.SetTrigger("Left");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MonstrAnim.SetTrigger("LeftExit");
        }
    }
}
