
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject[] Spot;
    public GameObject targetObject;
    public GameObject targetObject1;
    public GameObject targetObject2;// объект, материал которого будет меняться
    public Material newMaterial;// новый материал для объекта
    public GameObject Effect;
    public AudioSource LeverSound;
    public AudioSource Lever00Sound;
    public Animator LeverAnim;
    public GameObject LeverObject;

    private void Start()
    {
        // Проходимся по каждому объекту в массиве
        foreach (GameObject obj in Spot)
        {
            // Получаем компонент Rigidbody объекта
            obj.SetActive(false);
        }
        Effect.SetActive(false);
        LeverSound.Stop();
        Lever00Sound.Stop();
        LeverAnim.enabled = false;
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
                foreach (GameObject obj in Spot)
                {
                    // Получаем компонент Rigidbody объекта
                    obj.SetActive(true);
                }
                LeverSound.Play();
                Lever00Sound.Play();
                Effect.SetActive(true);
                // Если объект нажат, заменить его материал на новый материал
                targetObject.GetComponent<Renderer>().material = newMaterial;
                targetObject.GetComponent<Light>().color = Color.white;

                targetObject1.GetComponent<Renderer>().material = newMaterial;
                targetObject1.GetComponent<Light>().color = Color.white;

                targetObject2.GetComponent<Renderer>().material = newMaterial;
                targetObject2.GetComponent<Light>().color = Color.white;
                LeverAnim.enabled = true;
                Destroy(LeverObject);

            }
        }
    }
}






