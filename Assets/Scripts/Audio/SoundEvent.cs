using System;
using UnityEngine;

namespace Audio
{
	public class SoundEvent
	{
		public Vector3 Position;
		public Single Radius;
		public Single Intensity;

		public SoundEvent(Vector3 position, Single radius, Single intensity)
		{
			Position = position;
			Radius = radius;
			Intensity = intensity;
		}
	}
}