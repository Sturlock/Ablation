using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera m_CameraTransform = null;
    public Transform m_HandTransform = null;
    [SerializeField] private Image m_CursorImage = null;
    public float m_ThrowForce = 50;

    private RaycastHit m_RaycastFocus;
    [SerializeField]
    private bool m_CanInteract = false;


    private void Start()
    {
        m_CameraTransform = Camera.main;
    }

    private void Update()
    {
        // Has interact button been pressed whilst interactable object is in front of player?
        if (Input.GetButtonDown("Interact") && m_CanInteract)
        {
            IInteractable interactComponent = m_RaycastFocus.collider.transform.GetComponent<IInteractable>();

            if (interactComponent != null)
            {
                Debug.Log("hit");
                // Perform object's interaction
                interactComponent.Interact(this);

            }
        }

        // Has action button been pressed whilst interactable object is in front of player?
        if (Input.GetButtonDown("Fire2") && m_CanInteract)
        {
            IInteractable interactComponent = m_RaycastFocus.collider.transform.GetComponent<IInteractable>();

            if (interactComponent != null)
            {
                // Perform object's action
                //interactComponent.Action(this);
            }
        }
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(m_CameraTransform.transform.position, m_CameraTransform.transform.forward);
        Debug.DrawRay(m_CameraTransform.transform.localPosition, m_CameraTransform.transform.forward, Color.gray);
        // Is interactable object detected in front of player? 
        if (Physics.Raycast(ray, out m_RaycastFocus, 3) && (m_RaycastFocus.collider.transform.tag == "Intractable"))
        {
            //m_CursorImage.color = Color.green;
            m_CanInteract = true;
        }
        else
        {
           //m_CursorImage.color = Color.white;
            m_CanInteract = false;
        }
    }
}
