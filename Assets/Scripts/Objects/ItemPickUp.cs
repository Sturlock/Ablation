using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour, IInteractable
{
    public bool m_Held = false;

    [SerializeField] private Rigidbody m_ThisRigidbody = null;
    [SerializeField] private FixedJoint m_HoldJoint = null;


    private void Start()
    {

        gameObject.tag = "Intractable";
        m_ThisRigidbody = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        // If the holding joint has broken, drop the object
        if (m_HoldJoint == null && m_Held == true)
        {
            m_Held = false;
            m_ThisRigidbody.useGravity = true;
            //go.transform.SetParent(null);
        }
    }
    
    //Pick up the object, or drop it if it is already being held
    public void Interact(PlayerInteract playerScript)
    {
        throw new System.NotImplementedException();
    }
    // Throw the object
    public void Action(PlayerInteract script)
    {
        throw new System.NotImplementedException();
    }
}
