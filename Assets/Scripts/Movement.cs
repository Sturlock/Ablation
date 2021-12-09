using UnityEngine;

public class Movement : MonoBehaviour
{
    public CapsuleCollider cap;
    public Vector3 controlInput;
    
    public Rigidbody rb;

    [SerializeField] private float runMaxSpeed = 200f;
    [SerializeField] private float sprintMaxSpeed = 400f;
    [SerializeField] private float maxSpeed;
    public bool jumpBool;

    [Range(0, 2)]
    public int jumpCount = 0;

    [SerializeField] private float jumpForce = 20;
    private bool sprintBool;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    private void Update()
    {
        controlInput = Vector3.zero;
        controlInput += Input.GetAxisRaw("Horizontal") * transform.right;
        controlInput += Input.GetAxisRaw("Vertical") * transform.forward;

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
        Vector2 inputVelocity = new Vector2(controlInput.x * maxSpeed * Time.fixedDeltaTime,
            controlInput.z * maxSpeed * Time.fixedDeltaTime);
        rb.velocity = new Vector3(inputVelocity.x, rb.velocity.y, inputVelocity.y);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
            if (hit.transform.tag == "Generator")
            {
                if (Input.GetKey(KeyCode.F))
                {
                    PowerCharge powercharge = hit.transform.gameObject.GetComponent<PowerCharge>();
                    powercharge.GeneratorBool = true;
                }
            }
        }
    }

    private void Jump()
    {
        controlInput.y = jumpForce;
        //rb.velocity = controlInput * speed * Time.fixedDeltaTime; ;
        jumpBool = false;
        controlInput.y = 0f;
    }
}