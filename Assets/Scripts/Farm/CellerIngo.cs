using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CellerIngo : MonoBehaviour
{
    private static bool CellerAxeTrue;
    public string nextSceneName;

    private void Start()
    {
        CellerAxeTrue = false;
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
                if(CellerAxeTrue == true)
                {
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.Log("No Axe");
                }    
            }
        }
    }
}
