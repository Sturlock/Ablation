using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CapsuleCollider cap;
    public Animator animator;
    public Rigidbody rb;
    public Vector3 controlInput;

    

    [SerializeField] private float runMaxSpeed = 200f;
    [SerializeField] private float sprintMaxSpeed = 400f;
    [SerializeField] private float crouchingSpeed = 100f;
    [SerializeField] private float maxSpeed;
    public float accel = 0.05f;
    public bool jumpBool;

    [Range(0, 2)]
    public int jumpCount = 0;

    [SerializeField] private float jumpForce = 20;
    [SerializeField] private bool sprintBool;
    [SerializeField] private bool crouchBool;

    public bool b_Flashlight = false;
    public Light o_Flashlight;

    [Header("Detection Settings")]
    [SerializeField] private float detectRange = 0;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private SphereCollider sphere;

    [Header("Debug")]
    [SerializeField] private TextMeshProUGUI text;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, detectRange);
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
        sphere = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        #region Movement Inputs

        controlInput = Vector3.zero;
        controlInput += Input.GetAxisRaw("Horizontal") * transform.right;
        controlInput += Input.GetAxisRaw("Vertical") * transform.forward;

        if (Input.GetKey(KeyCode.LeftShift) && !crouchBool)
        {
            sprintBool = true;
        }
        else sprintBool = false;

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            jumpBool = true;

            jumpCount++;
        }

        if (Input.GetKey(KeyCode.LeftControl) && !sprintBool)
        {
            crouchBool = true;
        }
        else crouchBool = false;

        #endregion Movement Inputs

        if (Input.GetKeyDown(KeyCode.F))
        {
            b_Flashlight = !b_Flashlight;
        }

        DetectingPlayer();
        #region Animation
        float speed = 0f;
        float strafe = 0f;
        float crouch = 0f;
        float aniSpeed = animator.GetFloat("Speed");
        float aniStrafe = animator.GetFloat("Strafe");
        float aniCrouch = animator.GetFloat("Crouching");
        if (Input.GetAxis("Vertical") != 0f)
        speed = sprintBool ? 1f : 0.5f;
        else speed = 0f;
        strafe += Input.GetAxis("Horizontal");
        crouch = crouchBool ? 1f : 0f;

        
        aniSpeed = Mathf.SmoothStep(speed, aniSpeed, accel);
        aniStrafe = Mathf.SmoothStep(speed, aniStrafe, accel);
        aniCrouch = Mathf.SmoothStep(crouch, aniCrouch, accel);
        animator.SetBool("Crouch", crouchBool);
        animator.SetFloat("Crouching", aniCrouch);
        animator.SetFloat("Speed", aniSpeed);
        animator.SetFloat("Strafe", aniStrafe);
        
        
        #endregion


        #region DEBUG

#if UNITY_EDITOR
        if (sprintBool)
        {
            text.text = "SPRINTING";
        }
        else if (crouchBool)
        {
            text.text = "CROUCHING";
        }
        else
        {
            text.text = "RUNNING";
        }
#endif

        #endregion DEBUG
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

        if (b_Flashlight) o_Flashlight.enabled = true;
        else o_Flashlight.enabled = false;

        if (sprintBool)
        {
            maxSpeed = sprintMaxSpeed;
        }
        else if (crouchBool)
        {
            maxSpeed = crouchingSpeed;
        }
        else maxSpeed = runMaxSpeed;

        Vector2 inputVelocity = new Vector2(controlInput.x * maxSpeed * Time.fixedDeltaTime,
            controlInput.z * maxSpeed * Time.fixedDeltaTime);
        rb.velocity = new Vector3(inputVelocity.x, rb.velocity.y, inputVelocity.y);

        sphere.radius = detectRange;

        #region removed code

        // Code moved to Player Interact as it is more appropriate there
        //         RaycastHit hit;
        //         if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3f))
        //         {
        //             Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
        //             if (hit.transform.tag == "Generator")
        //             {
        //                 if (Input.GetKey(KeyCode.F))
        //                 {
        //                     PowerCharge powercharge = hit.transform.gameObject.GetComponent<PowerCharge>();
        //                     powercharge.GeneratorBool = true;
        //                 }
        //             }
        //         }

        #endregion removed code
    }

    private void Jump()
    {
        controlInput.y = jumpForce;
        //rb.velocity = controlInput * speed * Time.fixedDeltaTime; ;
        jumpBool = false;
        controlInput.y = 0f;
    }

    private void DetectingPlayer()
    {
        if (controlInput != Vector3.zero && !crouchBool)
        {
            sphere.enabled = true;
            if (sprintBool) detectRange = 10;
            else detectRange = 4;
        }
        else if (controlInput != Vector3.zero && crouchBool)
        {
            sphere.enabled = false;
            detectRange = 0;
        }
        else
        {
            sphere.enabled = false;
            detectRange = 0;
        }
    }
}