using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public bool leaveColliderOn;
    public Transform placement;
    public float throwForce;

    private new Collider collider;
    private Rigidbody rb;
    private bool inAir = false;

    private float touchedWall;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (GetComponent<BoxCollider>())
            collider = GetComponent<BoxCollider>();
        else
            collider = GetComponent<MeshCollider>();
    }

    private void FixedUpdate()
    {
        if (inAir)
        {
            transform.position = placement.transform.position;
            
            Debug.Log(placement.transform.localRotation.y);
        }
    }

    private void ForceDrop()
    {
        placement.DetachChildren();
        if (!leaveColliderOn)
            collider.enabled = true;
        rb.useGravity = true;
        inAir = false;
        GameObject.Find("Player").GetComponent<PlayerInteract>().OverrideHoldingItem();
    }

    public void PickUp()
    {
        this.transform.position = placement.transform.position;
        this.transform.SetParent(placement.transform);
        if (!leaveColliderOn)
            collider.enabled = false;
        rb.useGravity = false;
        inAir = true;
    }

    public void Drop()
    {
        placement.DetachChildren();
        if (!leaveColliderOn)
            collider.enabled = true;
        rb.useGravity = true;
        inAir = false;
    }

    public void Place()
    {
        placement.DetachChildren();
        inAir = false;
        transform.tag = "Untagged";
    }

    public void Throw()
    {
        inAir = false;
        rb.AddForce(placement.transform.up * (throwForce / 2) + (placement.transform.forward * throwForce), ForceMode.Impulse);
        placement.DetachChildren();
        rb.useGravity = true;
    }
}