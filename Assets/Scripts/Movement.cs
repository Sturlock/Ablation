using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    public CapsuleCollider cap;
    public Vector3 input;
    public float speed  = 20;
    public Rigidbody rb;

    float runMaxSpeed = 7.5f;
    float sprintMaxSpeed = 12f;
    [SerializeField]float maxSpeed;
    public bool jumpBool;
    [Range(0,2)]
    public int jumpCount = 0;
    [SerializeField] private float jumpForce = 20;
    private bool sprintBool;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        input = Vector3.zero;
        input += Input.GetAxisRaw("Horizontal") * transform.right;
        input += Input.GetAxisRaw("Vertical") * transform.forward;


        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintBool = true;
        }
        else sprintBool = false;

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            jumpBool = true;

            jumpCount++;
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity2D = new Vector2(rb.velocity.x, rb.velocity.z);
        if (velocity2D.magnitude > maxSpeed)
        {
            velocity2D = velocity2D.normalized * maxSpeed;
        }
        rb.velocity = new Vector3(velocity2D.x, rb.velocity.y, velocity2D.y);
        if (jumpBool)
        {
            //Jump();
        }

        if (sprintBool)
        {
            maxSpeed = sprintMaxSpeed;
        }
        else maxSpeed = runMaxSpeed;
        Vector2 inputVelocity = new Vector2(input.x * speed * Time.fixedDeltaTime, 
            input.z * speed * Time.fixedDeltaTime);
        rb.velocity = new Vector3(inputVelocity.x, rb.velocity.y, inputVelocity.y);

    }
    void Jump()
    {
        input.y = jumpForce;
        rb.velocity = input * speed * Time.fixedDeltaTime; ; 
        jumpBool = false;
        input.y = 0f;
        
    }
    
}
