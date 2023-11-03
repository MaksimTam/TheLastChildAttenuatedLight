using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject WallObject;
    public GameObject Black;
    private Animator BlackAnim;

    private void Start()
    {
        BlackAnim = Black.GetComponent<Animator>();
        BlackAnim.enabled = false;
        WallObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            WallObject.SetActive(false);
            BlackAnim.enabled = true;
            BlackAnim.SetBool("isEnter", true);
            BlackAnim.SetBool("Exit", false);
        }
    }
}
