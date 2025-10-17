using System;
using Audio;
using UnityEngine;

namespace Enemy
{
	public class AlienPerception : MonoBehaviour
	{
		public Single HearingSensitivity = 1;
		public Transform RightEar;
		public Transform LeftEar;

		private void OnEnable()
		{
			SoundSystem.OnSound += HandleSound;
		}

		private void OnDisable()
		{
			SoundSystem.OnSound -= HandleSound;
		}

		private void HandleSound(SoundEvent soundEvent)
		{
			Single distance = Vector3.Distance(transform.position, soundEvent.Position);
			if (distance > soundEvent.Radius * HearingSensitivity)
			{
				SendMessage("OnHeardSound", soundEvent, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}