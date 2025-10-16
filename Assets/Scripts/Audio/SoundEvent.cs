// // --------------------------------
// // -- File Created 	: 23:10 16/10/2025
// // -- File Part of the Ablation Solution, project Assembly-CSharp
// // -- Edited By : Will Sturley
// // --------------------------------

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