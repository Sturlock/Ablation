using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Camera playerCamera;
    public float pickupLength;
    
    GameObject inHands;
    RaycastHit hit;
    [SerializeField] bool holdingItem;
 
    

    void Start()
    {
        holdingItem = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (!holdingItem)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (Physics.Raycast(playerCamera.transform.position,
                         playerCamera.transform.TransformDirection(Vector3.forward), out hit, pickupLength) && !holdingItem)
                {
                    if (hit.transform.tag == "Big Object" || hit.transform.tag == "Small Object")
                    {
                        holdingItem = true;
                        inHands = GameObject.Find(hit.transform.name);
                        Debug.Log(inHands.name);
                        inHands.GetComponent<Pickupable>().PickUp();
                    }
                }
            }
        }
        else
        {
            if(inHands.tag == "Big Object")
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    inHands.GetComponent<Pickupable>().Drop();
                    holdingItem = false;
                    inHands = null;
                }
            }
            else if(inHands.tag == "Small Object")
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    inHands.GetComponent<Pickupable>().Drop();
                    holdingItem = false;
                    inHands = null;
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    inHands.GetComponent<Pickupable>().Throw();
                    holdingItem = false;
                    inHands = null;
                }
                
                if (Input.GetButtonDown("Interact"))
                  //  Debug.Log("Placing Small Object");

                if (Input.GetButtonDown("Fire2"))
                    Debug.Log("Throw Small Object");
            }

        }
    }

    public void OverrideHoldingItem()
    {
        holdingItem = false;
    }

}
