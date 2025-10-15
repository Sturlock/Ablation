using System;
using System.Collections;
using System.Linq;
using Interface;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Object = System.Object;
using Random = UnityEngine.Random;

namespace Enemy
{
	[RequireComponent(typeof(MenaceHandler))]
	public class AIDirector : Singleton<AIDirector>
	{
		private Transform _PlayerTransform;
		private Boolean _ProtectedArea;
		private Single _DistanceFromPlayer;
		private CharacterAI _CharacterAI;
		private IPlayer _Player;
		private Single _StationDownTimer;
		private Single _RequiredHoldTime;
		private NavMeshPath _AIPath;
		private Vector3 _OnNmPosition;
		private Vector3 _SamplePosition;
		private Coroutine _TensionHandle;
		private Boolean _PlayerKilled;
		private Single _TimeSincePassive;
		private MicroState _MicroState;
		private MenaceHandler _MenaceHandler;

		private Single Menace => _MenaceHandler.Menace;

		private void Start()
		{
			_CharacterAI = FindFirstObjectByType<CharacterAI>();
			_MenaceHandler = GetComponent<MenaceHandler>();
			_AIPath = new NavMeshPath();

			MonoBehaviour found = FindObjectsOfType<MonoBehaviour>().ToList().Find(behaviour => behaviour is IPlayer);
			if (!found || found is not IPlayer player)
			{
				Debug.LogException(new InvalidOperationException("Player not found in AIDirector"));
				return;
			}

			_Player = player;
		}

		private void Update()
		{
			if (_Player == null) return;

			_PlayerTransform = FindPlayer();
			_DistanceFromPlayer = CalculateMicroToPlayer();
		}

		private void LateUpdate()
		{
			if(_PlayerKilled) return;
			if (!_CharacterAI) return;
			if (_ProtectedArea) return;

			if (_DistanceFromPlayer < _CharacterAI.killRad)
			{
				_CharacterAI.Kill();
				_PlayerKilled = true;
			}

			TaskPriority priority = _TimeSincePassive switch
			{
				<= 25f => TaskPriority.LOW,
				<= 50f => TaskPriority.NORMAL,
				<= 75f => TaskPriority.HIGH,
				_      => TaskPriority.IMMEDIATE

			};

			if (_MicroState == MicroState.PASSIVE)
			{
				_CharacterAI.MoveToPosition(HintPlayerLocation(_PlayerTransform.position, false), priority);
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.white;
			if (!_Player.GameObject) return;

			Gizmos.DrawWireSphere(_Player.GameObject.transform.position, 15f);
		}

		public void GiveDestination(Vector3 position)
		{
			Debug.Log("[GiveDestination] Player near " + position);
			_CharacterAI.Interupt();
			_CharacterAI.Destination = position;
		}

		private Vector3 HintPlayerLocation(Vector3 position, Boolean protectedArea)
		{
			while (true)
			{
				Vector3 radius = Random.Range(8f, 15f) * Random.insideUnitSphere;
				radius.y = 0;

				if (protectedArea) return _CharacterAI.Destination;

				if (OnNavMesh(position + radius))
				{
					Debug.Log($"[HintPlayerLocation] Destination: {_OnNmPosition}");
					return _OnNmPosition;
				}

				Vector3 errorPos = position + radius;
				Debug.LogWarning($"[HintPlayerLocation] Destination: {errorPos} is not on the NavMesh");
			}
		}

		public void Interrupt()
		{
			if (_TensionHandle == null) return;

			StopCoroutine(_TensionHandle);
			_TensionHandle = null;
		}

		public Boolean IsPlayerNearAlienT1()
		{
			if (_AIPath.Length() <= 20)
			{
				return true;
			}

			return false;
		}

		public Boolean IsPlayerNearAlienT2()
		{
			if (_AIPath.Length() <= 10)
			{
				return true;
			}

			return false;
		}

		public Vector3 LeavePlayer(Vector3 position)
		{
			Vector3 rad = Random.Range(30f, 50f) * Random.insideUnitSphere;
			rad.y = 0;
			Vector3 targetPosition = position + rad;
			if (OnNavMesh(position + rad))
			{
				Debug.LogError("[WaveOff] Target Pos " + targetPosition);
				Debug.Log("[WaveOff] Destination: " + _OnNmPosition);
				return _OnNmPosition;
			}

			Vector3 errorPos = position + rad;
			Debug.LogWarning("[WaveOff] Destination: " + errorPos);
			return LeavePlayer(position);
		}

		public Boolean OnNavMesh(Vector3 targetDestination)
		{
			if (!NavMesh.SamplePosition(targetDestination, out NavMeshHit hit, 1f, NavMesh.AllAreas)) return false;

			_OnNmPosition = hit.position;
			return true;
		}

		public void WaveOff()
		{
			Interrupt();
			_CharacterAI.Interupt();
			_CharacterAI.Destination = LeavePlayer(_Player.GameObject.transform.position);
		}

		private Single CalculateMicroToPlayer()
		{
			_CharacterAI.NavMeshAgent.CalculatePath(_Player.GameObject.transform.position, _AIPath);
			return _AIPath.Length();
		}

		private Transform FindPlayer()
		{
			return _Player.GameObject.transform;
		}
	}
}