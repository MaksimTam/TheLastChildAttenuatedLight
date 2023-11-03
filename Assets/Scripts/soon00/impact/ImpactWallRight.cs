using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactWallRight : MonoBehaviour
{
    public GameObject Tentacle;
    private Animator TenAnim;

    private void Start()
    {
        TenAnim = Tentacle.GetComponent<Animator>();
        TenAnim.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Imapct2Trigger.True2 == true)
        {
            Debug.Log("Anim+Dead");
            TenAnim.enabled = true;
            Invoke("AnimOff", 3f);
        }
    }
    private void AnimOff()
    {
        TenAnim.enabled = false;
    }    
}
