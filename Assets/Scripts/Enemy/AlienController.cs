using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
	public class AlienController : MonoBehaviour
	{
		public AlienState State = AlienState.PATROL;
		private NavMeshAgent _NavMeshAgent;
		private AlienPerception _AlienPerception;
		private Transform _Player;
		private Vector3 _LastHeardPosition;
		private Single _InvestigationTimer;

		private void Start()
		{
			
		}
	}
}