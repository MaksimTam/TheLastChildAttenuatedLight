using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public float radius = 10f; // Радиус поиска объектов
    public string targetTag = "Light"; // Тег объектов, которые нужно найти
    public static bool Light;
    public AudioSource Audio;

    public ParticleSystem Particle;

    public void Update()
    {
        if (ButtonLight.LightButton == true && SimpleSonarShader_Object.CollisionEnter == true)
        {
            Audio.Play();
            // Получаем все объекты в радиусе с помощью Physics.OverlapSphere
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            bool lightFound = false;

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Light"))
                {
                    Particle.Play();
                    lightFound = true;
                    Material material = collider.GetComponent<Renderer>().material;
                    float emissionIntensity = material.GetFloat("_EmissionIntensity");
                    emissionIntensity--;

                    if (emissionIntensity <= 0f)
                    {
                        Destroy(collider.gameObject);
                    }
                    else
                    {
                        material.SetFloat("_EmissionIntensity", emissionIntensity);
                    }
                }
            }

            // Выводим сообщение в консоль в зависимости от результата
            if (lightFound)
            {
                Light = true;
                Debug.Log("Свет найден");
            }
            else
            {
                Debug.Log("нет света");
            }
        }
    }
}