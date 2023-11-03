using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using VideoClip;

public class DialogButton : MonoBehaviour
{
    public Animator anim;
    
    //TODO это потом удалится, тут только анимация вставания, диалог перенесен. Это тоже надо куда то втулить.
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("SIT", true);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetBool("SIT", false);
            anim.SetTrigger("isGetUP1");
            anim.SetTrigger("isSpeak");
        }
    }
}
