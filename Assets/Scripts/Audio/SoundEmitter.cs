using System;
using UnityEngine;

namespace Audio
{
	public class SoundEmitter: MonoBehaviour
	{
		public Single Radius = 8;
		public Single Intensity = 1f;

		public void Emit()
		{
			SoundSystem.Emit(new SoundEvent(transform.position, Radius, Intensity));
		}
	}
}