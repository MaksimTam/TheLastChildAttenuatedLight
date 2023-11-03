using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickColorEmissionForest : MonoBehaviour
{
    public GameObject Effect;
    private Animator EffectAnim;
    public GameObject me;
    private Animator MeAnim;
    public static bool Click;
    private Renderer rend;
    private Color originalColor1;
    private Color highlightColor1;

    private void OnMouseDown()
    {
        MeAnim.SetTrigger("Exit");
        Invoke("UI", 3);
        Click = true;
    }
    void Start()
    {
        EffectAnim = Effect.GetComponent<Animator>();
        MeAnim = me.GetComponent<Animator>();
        EffectAnim.enabled = false;
        Effect.SetActive(false);
        rend = GetComponent<Renderer>();
        originalColor1 = rend.material.GetColor("_EmissionColor");
        highlightColor1 = Color.white;
    }
    void OnMouseEnter()
    {
        rend.material.SetColor("_EmissionColor", highlightColor1);
    }

    void OnMouseExit()
    {
        rend.material.SetColor("_EmissionColor", originalColor1);
    }
    void UI()
    {
        Effect.SetActive(true);
        EffectAnim.enabled = true;
    }
}