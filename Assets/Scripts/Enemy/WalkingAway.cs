using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Random = UnityEngine.Random;

namespace Enemy
{
	public class WalkingAway : MonoBehaviour
	{
		private NavMeshAgent _navMeshAgent;
		private Animator _animator;
		private AudioSource _audioSource;
		public BoxCollider _box;
		public AudioClip Roar;

		private Boolean _heard = false;
		private Boolean _stopAI;

		[Header("Animation Settings"), Space]
		[ReadOnly] public String isMoving = "IsMoving";
		[ReadOnly] public String roar1 = "Roar1";
		[ReadOnly] public String roar2 = "Roar2";
		private Boolean _setRoar;
		private Coroutine _roarHandler = null;

		public Boolean go;
		public Transform target;
		// Start is called before the first frame update
		private void Start()
		{
			_navMeshAgent = GetComponent<NavMeshAgent>();
			_animator = GetComponentInChildren<Animator>();
			_audioSource = GetComponent<AudioSource>();
			_roarHandler = null;
		}

		// Update is called once per frame
		public IEnumerator MovePlace()
		{
			_navMeshAgent.SetDestination(target.position);
			yield return new WaitForSeconds(2f);
		}
		private void Update()
		{
			AnimationUpdate();
		}
		private void AnimationUpdate()
		{
			Single speed;
			if (_navMeshAgent.velocity.magnitude > 0.1f)
			{
				speed = _heard ? 1f : 0.5f;
				_animator.SetFloat("Speed", speed);
			}
			else
			{
				_animator.SetFloat("Speed", 0f);
			}
			String roar;
			Single seconds;
			Single i;
			Single x;

			if (_setRoar && _roarHandler == null)
			{
				_setRoar = false;
				i = Random.Range(0, 100);
				if (i >= 80)
				{
					x = 0;
				}
				else if (i < 80 && i >= 50)
				{
					x = 1;
				}
				else { x = 2; }
				switch (x)
				{
					case 0:
						roar = roar1;
						seconds = 2.567f;
						break;

					case 1:
						roar = roar2;
						seconds = 2.033f;
						break;

					default:
						roar = null;
						seconds = 0;
						break;
				}
				if (roar != null)
				{
					_roarHandler = StartCoroutine(PlayRoar(roar, seconds));
				}
			}

			if (_roarHandler == null)
				_stopAI = false;
		}

		private IEnumerator PlayRoar(String roar, Single seconds)
		{
			_animator.SetTrigger(roar);
			_audioSource.PlayOneShot(Roar);
			_stopAI = true;

			yield return new WaitForSeconds(seconds);

			_roarHandler = null;
		}
	}
}
