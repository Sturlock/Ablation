using System;
using UnityEngine;

namespace Enemy
{
	[Serializable]
	public struct InterestPoint
	{
		public Vector3 Position;
		public Single Weight;
		public Single Duration;

		/// <summary>
		///    Creates a copy of another <see cref="InterestPoint"/>
		/// </summary>
		/// <param name="interestPoint">
		///    The <see cref="InterestPoint"/> to be copied.
		/// </param>
		public InterestPoint(InterestPoint interestPoint)
		{
			Position = interestPoint.Position;
			Weight = interestPoint.Weight;
			Duration = interestPoint.Duration;
		}

		public InterestPoint(Vector3 position, Single weight, Single duration)
		{
			Position = position;
			Weight = weight;
			Duration = duration;
		}
	}
}