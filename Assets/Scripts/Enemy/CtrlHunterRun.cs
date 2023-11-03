using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CtrlHunterRun : MonoBehaviour
{
    public static CtrlHunterRun Instance;
    
    
    [SerializeField] private Animator animator;
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int JumpRun = Animator.StringToHash("JumpRun");
    private static readonly int Attack = Animator.StringToHash("Attack");

    public void AnimationEventStep()
    {
        Debug.Log("CtrlHunterRun : AnimationEventStep");
        CtrlSound.Instance.PlayHunterSteps();
    }
    
    public void AnimationEventJumpLanding()
    {
        Debug.Log("CtrlHunterRun : AnimationEventJumpLanding");
        CtrlSound.Instance.PlayHunterJumpLanding(0F);
    }


    public void ResetAllTriggers()
    {
        animator.ResetTrigger(Run);
        animator.ResetTrigger(JumpRun);
        animator.ResetTrigger(Attack);
    }
    

    public void StartAttack()
    {
        PlayAnimationAttack();
        PlaySoundAttack(0F);
    }

    public void StartJump()
    {
        PlayAnimationRunJump();
        PlaySoundJump();
    }
    
    

    private IEnumerator PlayScreamsLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3F, 6F));
            PlaySoundScream();
        }
        yield return null;
    }
    
    private void PlaySoundScream()
    {
        CtrlSound.Instance.PlayHunterScream();
    }

    private void PlaySoundAttack(float delay)
    {
        CtrlSound.Instance.PlayHunterAxeWoosh(delay);
        CtrlSound.Instance.PlayHunterAttack();
    }
    
    private void PlaySoundJump()
    {
        CtrlSound.Instance.PlayHunterJumpBegin();
    }

    
    
    
    private void PlayAnimationRunJump()
    {
        Debug.Log("CtrlHunterRun : PlayAnimationRunJump");
        animator.SetTrigger(JumpRun);
        RunAgain();
    }

    private void PlayAnimationAttack()
    {
        Debug.Log("CtrlHunterRun : PlayAnimationAttack");
        animator.SetTrigger(Attack);
        RunAgain();
    }

    private void PlayAnimationRun()
    {
        Debug.Log("CtrlHunterRun : PlayAnimationRun");
        animator.SetTrigger(Run);
    }

    private void RunAgain()
    {
        PlayAnimationRun();
    }



    public bool testAttackSound = false;

    private void UpdateTests()
    {
        if (testAttackSound)
        {
            PlaySoundAttack(0F);
            testAttackSound = false;
        }
    }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine( PlayScreamsLoop());
    }

    private void Update()
    {
      //  UpdateJumpsAttacks();
        UpdateTests();
    }
}
