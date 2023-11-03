using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static bool Click;
    public GameObject BossModel;
    private Animator BossAnimator;

    public bool Impact0;

    private void Start()
    {
        Click = false;
        BossAnimator = BossModel.GetComponent<Animator>();
    }
    public void Update()
    {
        if(Imapct3Trigger3.left == true || Imapct2Trigger.right == true)
        {
            Invoke("Imapct0", 0f);
        }
    }
    private void Imapct0()
    {
        BossAnimator.SetBool("Iddle", true);
    }
}