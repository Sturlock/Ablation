using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectedArea : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        AIDirector.Instance.protectedArea = false;
    }
}
