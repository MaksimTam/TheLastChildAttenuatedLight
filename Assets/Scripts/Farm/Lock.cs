using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Lock : MonoBehaviour
{
    private bool isPlayerInside;
    private bool isPlaying;
    [SerializeField] public static bool Finish00;
    public Animator Door;
    public Animator Lock1;
    public AudioSource DoorSound;
    public AudioSource audioSource;
    public  Lock LockS;



    private void Start()
    {
        StopAnimation();
        Door.enabled = false;
        Lock1.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAnimation();
            isPlayerInside = false;

        }
    }

    private void Update()
    {
        if (isPlayerInside)
        {
            if (Input.GetMouseButton(0))
            {
                if (!isPlaying)
                {
                    StartAnimation();
                }
            }
            else
            {
                StopAnimation();
            }

        }
        AnimationFinished();
    }

    private void StartAnimation()
    {
        Lock1.enabled = true;
        audioSource.Play();
        isPlaying = true;

    }

    private void StopAnimation()
    {
        Lock1.enabled = false;
        audioSource.Pause();
        isPlaying = false;
    }

    public void AnimationFinished()
    {
        if(Finish00  == true)
        {
            Debug.Log("Click");
            Destroy(audioSource);
            Door.enabled = true;
            DoorSound.Play();
            Destroy(LockS, 6f);
            Destroy(DoorSound, 9f);
            Destroy(audioSource);
        }
    }
    
}

