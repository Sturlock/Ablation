using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    public CapsuleCollider cap;
    public Vector3 controlInput;

    public Rigidbody rb;

    [SerializeField] private float runMaxSpeed = 200f;
    [SerializeField] private float sprintMaxSpeed = 400f;
    [SerializeField] private float crouchingSpeed = 100f;
    [SerializeField] private float maxSpeed;
    public bool jumpBool;

    [Range(0, 2)]
    public int jumpCount = 0;

    [SerializeField] private float jumpForce = 20;
    [SerializeField] private bool sprintBool;
    [SerializeField] private bool crouchBool;
    

    public bool b_Flashlight = false;
    public Light o_Flashlight;

    [Header("Detection Settings")]
    [SerializeField] float detectRange = 0;
    [SerializeField] LayerMask layerMask;
    [SerializeField] SphereCollider sphere;

    [Header("Debug")]
    [SerializeField]TextMeshProUGUI text;
    
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
    }

    

    // Update is called once per frame
    private void Update()
    {
        #region Movement Inputs
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

        if (Input.GetKey(KeyCode.LeftControl))
        {
            crouchBool = true;
        }
        else crouchBool = false;
#endregion

        if (Input.GetKeyDown(KeyCode.F))
        {
            b_Flashlight = !b_Flashlight;
        }
       
        DetectingPlayer();

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
#endregion
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
        #endregion
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
            if (sprintBool) detectRange = 5;
            else detectRange = 2;
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