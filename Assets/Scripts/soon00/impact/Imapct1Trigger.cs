using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imapct1Trigger : MonoBehaviour
{
    public GameObject TentacleWall1;
    private Animator TentacleWall1Anim;
    public GameObject TentacleWall2;
    private Animator TentacleWall2Anim;
    public GameObject Me;
    public static bool TriggerTrue;

    public GameObject Tentacle;
    private Animator TenAnim;
    public GameObject Barrier;
    public GameObject Barrier1;

    public GameObject Trigger;
    public GameObject Impact1Trig;


    private void Start()
    {
        TentacleWall1Anim = TentacleWall1.GetComponent<Animator>();
        TentacleWall2Anim = TentacleWall2.GetComponent<Animator>();
        TenAnim = Tentacle.GetComponent<Animator>();
        Barrier.SetActive(false);
        Barrier1.SetActive(false);
        TriggerTrue = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TentacleWall1Anim.SetTrigger("Save");
            TentacleWall2Anim.SetTrigger("Save");
            Impact1.Kin = true;
            Barrier.SetActive(true);
            Barrier1.SetActive(true);
            TenAnim.enabled = true;
            TriggerTrue = true;
            Invoke("BarrierOff", 12f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        }
    }
    private void BarrierOff()
    {
        Destroy(Me);
    }
}
