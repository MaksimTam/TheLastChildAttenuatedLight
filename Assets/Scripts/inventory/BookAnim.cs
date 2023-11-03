using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAnim : MonoBehaviour
{
    public Animator objectAnimator; // аниматор объекта
    public GameObject BackGround;
    public GameObject Info;

    private void OnMouseDown()
    {
        objectAnimator.enabled = true;
        objectAnimator.SetBool("On", false);
        objectAnimator.SetBool("Off", true);
        BackGround.SetActive(false);
        Info.SetActive(true);
    }
}

