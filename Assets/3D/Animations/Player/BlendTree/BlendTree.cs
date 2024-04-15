using UnityEngine;

namespace _3D.Animations.Player.BlendTree
{
	public class BlendTree : MonoBehaviour
	{
		private Animator animator;
		public float Sped = 0f;
		public float Croch = 0f;
		public float accel = 0.02f;
		// Start is called before the first frame update
		private void Start()
		{
			animator = GetComponent<Animator>();
		}

		// Update is called once per frame
		private void Update()
		{
			float Sped = animator.GetFloat("Speed");
			float Croch= animator.GetFloat("Crouching");
			bool walking = Input.GetKey("w");
			bool running = Input.GetKey(KeyCode.LeftShift);
			bool crouching = Input.GetKey(KeyCode.LeftControl);


			if (Sped <=0.5 && walking)
			{
				Sped += accel;
				if (running)
				{
					Sped += accel;
				}
				animator.SetFloat("Speed", Sped);
			}

			if (Sped >= 0.5 && walking && running)
			{
				Sped += accel;
				if (Sped > 1)
				{
					Sped = 1f;
				}
				animator.SetFloat("Speed", Sped);
			}

			if (Sped > 0.5 && walking && !running)
			{
				Sped -= accel;
				if (Sped == 0.5)
				{
					Sped = 0.5f;
				}
				animator.SetFloat("Speed", Sped);
			}

			if (!walking)
			{
				Sped -= accel;
				if (Sped <= 0)
				{
					Sped = 0f;
				}
				animator.SetFloat("Speed", Sped);
			}

			if (crouching)
			{
				Croch += accel;
				if (Croch > 1)
				{
					Croch = 1f;
				}
				animator.SetFloat("Crouching", Croch);
			}
			if (!crouching)
			{
				Croch -= accel;
			}
			if (Croch < 0)
			{
				Croch = 0f;
			}
			animator.SetFloat("Crouching", Croch);
		}
	}
}
