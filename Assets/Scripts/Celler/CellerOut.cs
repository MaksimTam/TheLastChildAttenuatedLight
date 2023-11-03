using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CellerOut : MonoBehaviour
{
    public string nextSceneName;
    public AudioSource OutCeller;
    public Image Image;
    public Animator anim;
    public GameObject Luk;

    private void Start()
    {
        anim.enabled = false;
    }
    private void OnMouseDown()
    {
        // Запускаем таймер для перехода на следующую сцену
        anim.enabled = true;
        OutCeller.Play();
        Invoke("LoadNextScene", 6f);
        Luk.SetActive(false);
        
}

    private void LoadNextScene()
    {
        // Загружаем следующую сцену по ее имени
        SceneManager.LoadScene(nextSceneName);
        Image.enabled = false;
        Luk.SetActive(true);

    }
}
