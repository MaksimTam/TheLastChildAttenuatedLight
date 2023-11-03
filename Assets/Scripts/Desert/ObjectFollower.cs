using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    [SerializeField]private Transform playerTransform; // Ссылка на трансформ игрока
    public float followSpeed = 2f; // Скорость, с которой объект будет следовать за игроком

    private void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // Получаем позицию игрока и объекта
        Vector3 playerPosition = playerTransform.position;
        Vector3 objectPosition = transform.position;

        // Проверяем, в какую сторону движется игрок по оси X
        if (playerPosition.x > objectPosition.x)
        {
            // Если игрок движется в положительном направлении по оси X,
            // передвигаем объект в эту же сторону с помощью плавного сглаживания
            objectPosition.x = Mathf.Lerp(objectPosition.x, playerPosition.x, followSpeed * Time.deltaTime);
        }
        else if (playerPosition.x < objectPosition.x)
        {
            // Если игрок движется в отрицательном направлении по оси X,
            // передвигаем объект в эту же сторону с помощью плавного сглаживания
            objectPosition.x = Mathf.Lerp(objectPosition.x, playerPosition.x, followSpeed * Time.deltaTime);
        }
        if (playerPosition.y > objectPosition.y)
        {
            // Если игрок движется в положительном направлении по оси X,
            // передвигаем объект в эту же сторону с помощью плавного сглаживания
            objectPosition.y = Mathf.Lerp(objectPosition.y, playerPosition.y, followSpeed * Time.deltaTime);
        }
        else if (playerPosition.y < objectPosition.y)
        {
            // Если игрок движется в отрицательном направлении по оси X,
            // передвигаем объект в эту же сторону с помощью плавного сглаживания
            objectPosition.y = Mathf.Lerp(objectPosition.y, playerPosition.y, followSpeed * Time.deltaTime);
        }

        // Обновляем позицию объекта
        transform.position = objectPosition;
    }
}

