using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour, IInteractable
{
    public bool m_Held = false;

    [SerializeField] private Rigidbody m_ThisRigidbody = null;
    [SerializeField] private FixedJoint m_HoldJoint = null;

    GameObject go;


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

    // Pick up the object, or drop it if it is already being held
    public void Interact(PlayerInteract playerScript)
    {
        // Is the object currently being held?
        if (m_Held)
        {
            Drop();
        }
        else
        {
            m_Held = true;
            m_ThisRigidbody.useGravity = false;
            
            m_HoldJoint = playerScript.m_HandTransform.gameObject.AddComponent<FixedJoint>();
            m_HoldJoint.breakForce = 10000f; // Play with this value
            m_HoldJoint.connectedBody = m_ThisRigidbody;
            
       
            //go = m_ThisRigidbody.gameObject;
            //go.transform.SetParent(m_HoldJoint.transform);
        }
    }

    // Throw the object
    public void Action(PlayerInteract playerScript)
    {
        // Is the object currently being held?
        if (m_Held)
        {
            Drop();

            // Force the object away in the opposite direction of the player
            Vector3 forceDir = transform.position - playerScript.m_HandTransform.position;
            m_ThisRigidbody.AddForce(forceDir * playerScript.m_ThrowForce);

        }
    }

    // Drop the object
    public void Drop()
    {
        m_Held = false;
        m_ThisRigidbody.useGravity = true;
        go = m_ThisRigidbody.gameObject;
        m_ThisRigidbody.freezeRotation = false;
        go.transform.SetParent(null);
        Destroy(m_HoldJoint);
    }
}
