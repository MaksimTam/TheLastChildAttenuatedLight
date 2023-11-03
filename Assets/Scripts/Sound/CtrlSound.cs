using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CtrlSound : MonoBehaviour
{
    public static CtrlSound Instance;

    [Header("Hunter")]
    [SerializeField] private AudioSource wooshAxe;
    [SerializeField] private AudioSource hunterSteps;
    [SerializeField] private AudioSource hunterAttack;
    [SerializeField] private AudioSource hunterJumpBegin;
    [SerializeField] private AudioSource hunterJumpLanding;

    [SerializeField] private List<AudioSource> hunterScreams;

    [Header("Player")]
    [SerializeField] private List<AudioSource> playerSteps;

    private int screamCounter = 0;


    [Header("FallingTree")]
    [SerializeField] private AudioSource treeCut;

    [SerializeField] private AudioSource fallingTree;
   
    [SerializeField] private List<AudioSource> fallingTrees;

    

   
    
    #region Player

    private int _counterPlayerSteps = 0;
    public void PlayPlayerSteps()
    {
        _counterPlayerSteps = Random.Range(0, playerSteps.Count - 1);
        PlaySound(0,playerSteps[_counterPlayerSteps]);
       // _counterPlayerSteps++;
       // if (_counterPlayerSteps >= playerSteps.Count-1) _counterPlayerSteps = 0;
    }
    

    #endregion


    #region Hunter

    public void PlayHunterSteps()
    {
        PlaySound(0,hunterSteps);
    }
    
    public void PlayHunterAttack()
    {
        PlaySound(0,hunterAttack);
    }
    
    public void PlayHunterJumpBegin()
    {
        PlaySound(0,hunterJumpBegin);
    }
    public void PlayHunterJumpLanding(float delay)
    {
        PlaySound(delay,hunterJumpLanding);
    }
    
    
    public void PlayHunterScream()
    {
        PlaySound(0,hunterScreams[screamCounter]);
        screamCounter++;
        if (screamCounter >= hunterScreams.Count-1) screamCounter = 0;
    }
    
    public void PlayHunterAxeWoosh(float delay)
    {
        PlaySound(delay, wooshAxe);
    }

    #endregion

    
    private int _counterFallingTree = 0;
    public void PlayFallingTree()
    {
        PlaySound(0,fallingTrees[_counterFallingTree]);
        _counterFallingTree++;
        if (_counterFallingTree >= fallingTrees.Count-1) _counterFallingTree = 0;
    }
    
    public void PlayCutTree()
    {
        PlaySound(0,treeCut);
    }
    
    
    private IEnumerator PlaySoundDelayed(float t, AudioSource audioSource)
    {
        yield return new WaitForSeconds(t);
        audioSource.Play();
    }

    private void PlaySound(float delay, AudioSource audioSource)
    {
        if (audioSource == null)
        {
            Debug.LogError("PlaySound : audioSource = NULL");
            return;
        }
        if (delay == 0F)
        {
            audioSource.Play();
        }
        else
        {
            StartCoroutine(PlaySoundDelayed(delay,audioSource));
        }
    }
    
    
    private void Awake()
    {
        Instance = this;
    }
}
