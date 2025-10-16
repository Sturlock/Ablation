// // --------------------------------
// // -- File Created 	: 23:10 16/10/2025
// // -- File Part of the Ablation Solution, project Assembly-CSharp
// // -- Edited By : Will Sturley
// // --------------------------------

using System;

namespace Audio
{
	public class SoundSystem
	{
		public static event Action<SoundEvent> OnSound;
		public static void Emit(SoundEvent soundEvent)
		{
			OnSound?.Invoke(soundEvent);
		}
	}
}