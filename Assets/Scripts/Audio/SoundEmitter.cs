// // --------------------------------
// // -- File Created 	: 23:10 16/10/2025
// // -- File Part of the Ablation Solution, project Assembly-CSharp
// // -- Edited By : Will Sturley
// // --------------------------------

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