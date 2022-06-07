using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLogPickUp : MonoBehaviour, IInteractable
{
    [SerializeField] LogGiver lg;

    private void Start()
    {
        lg = GetComponent<LogGiver>();
    }
    public void Action(PlayerInteract script)
    {
        throw new System.NotImplementedException();
    }

    public void Interact(PlayerInteract script)
    {
        lg.GiveLog();
        Destroy(gameObject);
    }
}
