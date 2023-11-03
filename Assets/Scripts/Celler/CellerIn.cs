using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class CellerIn : MonoBehaviour

{
    public string nextSceneName;
    public AudioSource InCeller;
    public Image Image;
    public Animator anim;
    public Animator Luk;
    public GameObject Text;
    public Animator TextAnim;


    private void Start()
    {
        anim.enabled = false;
        Luk.enabled = false;
    }
    private void OnMouseDown()
    {
        // Запускаем таймер для перехода на следующую сцену
        anim.enabled = true;
        TextAnim.enabled = true;
        InCeller.Play();
        Invoke("LoadNextScene", 6f);
        Luk.enabled = true;
        TextAnim.SetTrigger("Text00");
    }

    private void LoadNextScene()
    {
        // Загружаем следующую сцену по ее имени
        anim.enabled = false;
        SceneManager.LoadScene(nextSceneName);
    }
}
