using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    [SerializeField] private Animator ani;
    [SerializeField] private bool doorOpen;
    [SerializeField] private GameObject target;
    [SerializeField] private AudioSource aud;
    public AudioClip Open;
    public AudioClip Close;

    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        aud = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (doorOpen)
        {
            ani.SetBool("Open", true);
            //aud.PlayOneShot(Open);
        }
        else
        {
            ani.SetBool("Open", false);
            //aud.PlayOneShot(Close);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            target = other.gameObject;
            if (!doorOpen)
            {
                aud.PlayOneShot(Open);
            }
            doorOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            target = null;
            if (doorOpen)
            {
                aud.PlayOneShot(Close);
            }
            doorOpen = false;
        }
    }
}