using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    [SerializeField] private Animator ani;
    [SerializeField] private bool doorOpen;
    [SerializeField] private GameObject target;

    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (doorOpen)
        {
            ani.SetBool("Open", true);
        }
        else
        {
            ani.SetBool("Open", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.CompareTag("Monster"))
        {
            target = other.gameObject;
            doorOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.CompareTag("Monster"))
        {
            target = null;
            doorOpen = false;
        }
    }
}