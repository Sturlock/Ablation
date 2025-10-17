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