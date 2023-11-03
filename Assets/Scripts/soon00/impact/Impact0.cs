using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact0 : MonoBehaviour
{
    private Animator TextExitAnim;
    public GameObject TextExit;
#pragma warning disable UNT0013 // Remove invalid SerializeField attribute
    [SerializeField] public static bool Impact00;
#pragma warning restore UNT0013 // Remove invalid SerializeField attribute

    public AudioSource StartBossImpact;

    private void Start()
    {
        TextExitAnim = TextExit.GetComponent<Animator>();
        TextExitAnim.enabled = false;
        Impact00 = false;
        StartBossImpact.Stop();
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartBossImpact.Play();
            Debug.Log("Exit");
            Impact00 = true;
            BossSpawn.Kin = true;
            TextExitAnim.enabled = true;
            Destroy(TextExit, 2f);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enter");
            Impact00 = false;
            BossSpawn.Kin = false;
        }
    }
}