using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerInteract script);

    void Action(PlayerInteract script);
}
