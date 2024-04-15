using UnityEngine;

namespace _3D.Animations.Player.Standard
{
	public class Movment : MonoBehaviour
	{
		private Animator animator;
		public bool CrouchToggle = false;
		// Start is called before the first frame update
		private void Start()
		{
			animator = GetComponent<Animator>();
			Debug.Log(animator);
		}

		// Update is called once per frame
		private void Update()
		{
			bool IsWalking = animator.GetBool("IsWalking");
			bool IsRunning = animator.GetBool("IsRunning");
			bool IsCrouching = animator.GetBool("IsCrouching");
			bool walking = Input.GetKey("w");
			bool running = Input.GetKey(KeyCode.LeftShift);
			bool Crouch = Input.GetKeyDown(KeyCode.LeftControl);

			if (!IsCrouching && Crouch)
			{
				Crouching();
			}

			if (CrouchToggle == true)
			{
				if (!IsWalking && walking)
				{
					animator.SetBool("IsWalking", true);
				}
				if (IsWalking && !walking)
				{
					animator.SetBool("IsWalking", false);
				}

				if (IsWalking && !IsRunning && running)
				{
					animator.SetBool("IsRunning", true);
					Crouching();
				}
			}

			if (IsCrouching && Crouch)
			{
				Crouching();
			}

			if (!IsWalking && walking)
			{
				animator.SetBool("IsWalking", true);
			}

			if (IsWalking && !IsRunning && running)
			{
				animator.SetBool("IsRunning", true); 
			}

			if (IsWalking && IsRunning && !running)
			{
				animator.SetBool("IsRunning", false);
			}

			if (IsWalking && !walking)
			{
				animator.SetBool("IsWalking", false);
				animator.SetBool("IsRunning", false);
			}
		}

		private void Crouching()
		{
			CrouchToggle = !CrouchToggle;
			Debug.Log(CrouchToggle);
			animator.SetBool("IsCrouching", CrouchToggle);
			//animator.SetBool("IsRunning", false);

        
		}
	}
}
