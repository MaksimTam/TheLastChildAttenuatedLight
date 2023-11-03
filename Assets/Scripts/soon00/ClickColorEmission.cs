using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class ClickColorEmission : MonoBehaviour
{

    private DepthOfField depthOfField;
    public PostProcessVolume postProcessVolume;

    public GameObject Light;
    private Renderer rend;
    private Color originalColor;
    private Color highlightColor;
    public GameObject Location;
    public Transform targetPosition;
    public GameObject Black;
    public Animator BlackAnim;
    public GameObject UIBlack;
    private Animator UI;
    public Animator Anim;
    public AudioSource Portal;

    private void Awake()
    {
        // Получаем компонент DepthOfField
        postProcessVolume.profile.TryGetSettings(out depthOfField);
    }
    void Start()
    {
        UI = UIBlack.GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        originalColor = rend.material.GetColor("_EmissionColor");
        highlightColor = Color.red;
        Location.SetActive(true);
        Black.SetActive(false);
        BlackAnim.enabled = false;
        Anim.enabled = false;
    }

    void OnMouseEnter()
    {
        rend.material.SetColor("_EmissionColor", highlightColor);
    }

    void OnMouseExit()
    {
        rend.material.SetColor("_EmissionColor", originalColor);
    }
    private void OnMouseDown()
    {
        UIBlack.SetActive(true);
        UI.SetTrigger("EndSpeed");
        Light.SetActive(false);
        Invoke("LocationOff", 0.6f);
        Anim.enabled = true;
        Portal.Play();
    }
    private void LocationOff()
    {
        Invoke("BackOn", 5f);
        Black.SetActive(true);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            player.transform.position = targetPosition.position;
        }
    }
    private void BackOn()
    {
        BlackAnim.enabled = true;
        UIBlack.SetActive(false);
        depthOfField.enabled.value = false;
    }
}