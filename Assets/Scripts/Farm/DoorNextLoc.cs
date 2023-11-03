using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class DoorNextLoc : MonoBehaviour
{
    public Animator Dooranim;
    public GameObject TriggerDoor;
    public string nextSceneName;
    public Animator BlackBackground;
    public GameObject Black;

    private void Start()
    {
        Black.SetActive(false);
        Dooranim.enabled = false;
        BlackBackground.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Dooranim.enabled = true;
            Black.SetActive(true);
            BlackBackground.enabled = true;
            BlackBackground.SetTrigger("BlackLong");
#pragma warning disable UNT0016 // Unsafe way to get the method name
            Invoke("LoadNextScene", 4f);
#pragma warning restore UNT0016 // Unsafe way to get the method name
        }
    }
    private void LoadNextScene()
    {
        // Загружаем следующую сцену по ее имени
        Destroy(TriggerDoor);
        SceneManager.LoadScene(nextSceneName);
    }
}
