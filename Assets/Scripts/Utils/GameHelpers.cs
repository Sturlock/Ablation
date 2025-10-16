using System;
using UnityEngine;
using UnityEngine.AI;

namespace Utils
{
	public static class GameHelpers
	{
		public static Vector3 Direction(this Vector3 origin, Vector3 destination)
		{
			return (destination - origin).normalized;
		}

		public static Single Length(this NavMeshPath path)
		{
			Single currentLenght = 0;

			if (path?.corners == null) return currentLenght;

			for (Int32 i = 0; i < path.corners.Length - 1; i++)
			{
				currentLenght += Vector3.Distance(path.corners[i], path.corners[i + 1]);
			}
			return currentLenght;
		}

	}
}
