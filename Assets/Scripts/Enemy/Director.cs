using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public class Director : MonoBehaviour
	{
		public Single InterestDecayRate = 1f;
		private List<InterestPoint>  _InterestPoints = new List<InterestPoint>();

		public static Director Instance { get; private set; }

		private void Awake()
		{
			if (!Instance)
			{
				Instance = this;
				return;
			}

			Destroy(gameObject);
		}


		private void Update()
		{
			for (Int32 i = _InterestPoints.Count; i >= 0; i--)
			{
				InterestPoint interestPoint = _InterestPoints[i];
				interestPoint.Duration -=  Time.deltaTime;
				interestPoint.Weight -=  InterestDecayRate * Time.deltaTime;
				if(interestPoint.Duration <= 0 || interestPoint.Weight <= 0) _InterestPoints.RemoveAt(i);
			}
		}

		public void AddPointOfInterest(InterestPoint interestPoint)
		{
			_InterestPoints.Add(interestPoint);
		}

		public void AddPointOfInterest(Vector3 position, Single weight = 1f, Single duration = 10f)
		{
			_InterestPoints.Add(new InterestPoint(position, weight, duration));
		}

		public InterestPoint? GetBestInterestPoint(Vector3 position, Single radius)
		{
			InterestPoint? bestPoint = null;
			Single bestScore = 0;
			foreach (InterestPoint interestPoint in _InterestPoints)
			{
				Single distance = Vector3.Distance(position, interestPoint.Position);

				if(distance > radius) continue;

				Single score = interestPoint.Weight / (1 + distance);

				if (score <= bestScore) continue;

				bestScore = score;
				bestPoint = interestPoint;
			}
			return bestPoint;
		}
	}
}