using UnityEngine;

public class PlayerPushObject : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip movingSound;
    public AudioClip collisionSound;
    public string mountTag = "Mount";
    public Transform player;

    private Rigidbody rigidbody;
    private bool collided = false;
    public GameObject StartPush;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        StartPush.SetActive(false);
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (rigidbody.velocity.magnitude > 0 && !audioSource.isPlaying)
        {
            audioSource.clip = movingSound;
            audioSource.Play();
        }
        else if (rigidbody.velocity.magnitude == 0 && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(mountTag) && !collided)
        {
            audioSource.clip = collisionSound;
            audioSource.Play();
            StartPush.SetActive(true);
            collided = true;
            Destroy(rigidbody);
        }
    }
}









