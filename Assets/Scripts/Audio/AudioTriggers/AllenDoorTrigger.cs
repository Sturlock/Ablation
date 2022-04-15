using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllenDoorTrigger : MonoBehaviour
{
    public void Activate()
    {
        gameObject.GetComponent<SphereCollider>().enabled = true;
    }

}
