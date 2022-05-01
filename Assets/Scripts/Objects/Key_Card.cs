using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Card : MonoBehaviour, IInteractable
{
    private SecurityClearance security;

    private void Awake()
    {
        security = FindObjectOfType<SecurityClearance>();
    }

    public void Action(PlayerInteract script)
    {
        throw new System.NotImplementedException();
    }

    public void Interact(PlayerInteract script)
    {
        security.IncreaseLevel();
        Destroy(gameObject);
    }
}
