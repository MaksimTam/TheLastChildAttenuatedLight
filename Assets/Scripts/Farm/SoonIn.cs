using UnityEngine;
using UnityEngine.SceneManagement;

public class SoonIn : MonoBehaviour
{
    public string nextSceneName;
    private bool playerEntered;
    public Animator Black;


    private void Start()
    {
        Black.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerEntered = true;
            Invoke("LoadNextScene", 5f);
            Black.enabled = true;
        }
    }

    private void LoadNextScene()
    {
        if (playerEntered)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
