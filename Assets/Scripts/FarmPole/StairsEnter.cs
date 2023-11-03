using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsEnter : MonoBehaviour
{
    public GameObject Text;
    public GameObject StairsTrigger;

    private void Start()
    {
        StairsTrigger.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Создать луч из позиции курсора мыши в мировом пространстве
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Проверить, пересек ли луч с объектом
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {

                StairsTrigger.SetActive(true);
            }
        }
    }
}
