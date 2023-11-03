using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoObject : MonoBehaviour
{
    public GameObject BackGround;
    public Animator BookOpenAnim;
    public GameObject Info;

    private void Start()
    {
        BookOpenAnim.enabled = false;
    }
    private void Update()
    {
        // Проверить, была ли нажата кнопка мыши
        if (Input.GetMouseButtonDown(0))
        {
            // Создать луч из позиции курсора мыши в мировом пространстве
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Проверить, пересек ли луч с объектом
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                BackGround.SetActive(true);
                BookOpenAnim.enabled = true;
                BookOpenAnim.SetBool("On", true);
                BookOpenAnim.SetBool("Off", false);
                Info.SetActive(false);

            }
        }
    }
}

