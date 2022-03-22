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
        target = other.gameObject;
        if (other.CompareTag("Player"))
        {
            doorOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
        if (other.CompareTag("Player"))
        {
            doorOpen = false;
        }
    }
}