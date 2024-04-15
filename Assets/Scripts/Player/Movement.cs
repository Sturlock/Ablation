using TMPro;
using UnityEngine;

namespace Player
{
	public class Movement : MonoBehaviour
	{
		public CapsuleCollider cap;
		public Animator animator;
		public Rigidbody rb;
		public PlayerAudio playerAudio;
		private Vector3 controlInput;
		[SerializeField] private Vector3 rawInput;

		private float runMaxSpeed = 100f;
		private float sprintMaxSpeed = 240f;
		private float crouchingSpeed = 60f;
		private float maxSpeed;
		public float accel = 0.05f;
		public bool jumpBool;

		[Range(0, 2)]
		public int jumpCount = 0;

		private float jumpForce = 20;
		private bool sprintBool;
		private bool crouchBool;

		[SerializeField] private float walkTime = .5f;
		[SerializeField] private float sprintTime = .2f;

		public bool b_Flashlight = false;
		public Light o_Flashlight;

		[Header("Detection Settings")]
		private float detectRange = 0;

		public LayerMask layerMask;
		[SerializeField]
		private SphereCollider sphere;

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
			animator = GetComponent<Animator>();
			playerAudio = GetComponent<PlayerAudio>();
		}

		private void Inputs()
		{
			controlInput = Vector3.zero;
			controlInput += Input.GetAxisRaw("Horizontal") * transform.right;
			controlInput += Input.GetAxisRaw("Vertical") * transform.forward;
			rawInput.x = Input.GetAxisRaw("Horizontal");
			rawInput.z = Input.GetAxisRaw("Vertical");
		}

		// Update is called once per frame
		private void Update()
		{
			#region Movement Inputs

			Inputs();

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
			if (rawInput.z > 0.1f)
			{
				if (!sprintBool)
				{
					speed = 0.5f;
					float timer = walkTime -= Time.fixedDeltaTime;

					if (timer <= 0.0f)
					{
						//playerAudio.FootSteps();
						timer = 0.5f;
					}
				}
				else if (sprintBool)
				{
					speed = 1f;
					float timer = sprintTime -= Time.fixedDeltaTime;

					if (timer <= 0.0f)
					{
						//playerAudio.FootSteps();
						timer = 0.2f;
					}
				}
			}  
			else if (rawInput.z < -0.1f)
			{
				speed = -0.5f;
			}
			else speed = 0f;
			strafe = rawInput.x;
			crouch = crouchBool ? 1f : 0f;

			aniSpeed = Mathf.SmoothStep(speed, aniSpeed, accel);
			aniStrafe = Mathf.SmoothStep(strafe, aniStrafe, accel);
			aniCrouch = Mathf.SmoothStep(crouch, aniCrouch, accel);
			animator.SetBool("Crouch", crouchBool);
			animator.SetFloat("Crouching", aniCrouch);
			animator.SetFloat("Speed", aniSpeed);
			animator.SetFloat("Strafe", aniStrafe);

			#endregion Animation

			#region DEBUG

#if UNITY_EDITOR
			if (text != null)
			{
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
}