using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMaterial : MonoBehaviour
{
    public GameObject objectToChange; // объект, материал которого нужно изменить
    public Material newMaterial; // новый материал для объекта
    public GameObject TextAct;
    public GameObject TextInfo;

    private void Start()
    {
        TextAct.SetActive(false);
        TextInfo.SetActive(true);
    }
    private void OnMouseEnter()
    {
        // при попадании курсора в изображение, меняем материал объекта
        if (objectToChange != null)
        {
            TextAct.SetActive(true);
            TextInfo.SetActive(false);
            Renderer renderer = objectToChange.GetComponent<Renderer>();
            if (renderer != null)
            {
                Debug.Log("Ok");
                renderer.material = newMaterial;
            }
        }
    }
    private void OnMouseExit()
    {
        // при ухода курсора с изображение
        if (objectToChange != null)
        {
            TextAct.SetActive(false);
            TextInfo.SetActive(true);
        }
    }
}
