using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickColorEmissionRIAD : MonoBehaviour
{
    public static bool DialogClick;
    public GameObject NPC;
    private Animator NPCAnim;
    public float rotationAngle = 70f;

    public GameObject Me;

    public GameObject Dialog;

    private Renderer rend;
    private Color originalColor1;
    private Color highlightColor1;

    void Start()
    {
        NPCAnim = NPC.GetComponent<Animator>();
        Dialog.SetActive(false);
        rend = GetComponent<Renderer>();
        originalColor1 = rend.material.GetColor("_EmissionColor");
        highlightColor1 = Color.white;
    }
    private void OnMouseDown()
    {
        DialogClick = true;
        Me.SetActive(false);
        Dialog.SetActive(true);
        NPCAnim.SetTrigger("Dialog");
        NPC.transform.Rotate(0, rotationAngle, 0);
    }

    void OnMouseEnter()
    {
        rend.material.SetColor("_EmissionColor", highlightColor1);
    }

    void OnMouseExit()
    {
        rend.material.SetColor("_EmissionColor", originalColor1);
    }
}