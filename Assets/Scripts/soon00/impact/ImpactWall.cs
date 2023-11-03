using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ImpactWall : MonoBehaviour
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
        if (other.CompareTag("Player") && Imapct3Trigger3.True3 == true)
        {
            TenAnim.enabled = true;
        }
    }
}