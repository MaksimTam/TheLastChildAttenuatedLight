using UnityEngine;


public class Key : MonoBehaviour
{
    public GameObject objectToDestroy; // объект, который нужно удалить


    private void OnMouseDown()
    {
        Destroy(objectToDestroy);

        // Включаем переменную bool в другом скрипте
        DoorFarm.play = true;
        Debug.Log("dd");

    }

}

