using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Enemy
{
	public class AIDirector : Singleton<AIDirector>
	{
		[Range(0f, 100f)]
		public float tension;

		private Vector3 samplePosition;
		private Vector3 onNMPosition;
		public Vector3 playerLocation;
		public bool protectedArea;
		private Coroutine tensionHandle;
		private NavMeshPath AIPath;
		public float distanceFromPlayer;

		[Space, Header("Characters")]
		public GameObject AI;

		public GameObject Player;
		public CharacterAI characterAI;

		[Space, SerializeField]
		private float stationDownTimer;

		[SerializeField]
		private float requiredHoldTime;

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.white;
			Gizmos.DrawWireSphere(Player.transform.position, 15f);
		}

		public bool Heard
		{
			get => characterAI._heard;
		}

		public bool OnNavMesh(Vector3 targetDestination)
		{
			NavMeshHit hit;
			if (NavMesh.SamplePosition(targetDestination, out hit, 1f, NavMesh.AllAreas))
			{
				onNMPosition = hit.position;
				return true;
			}
			return false;
		}

		private void Start()
		{
			characterAI = AI.GetComponent<CharacterAI>();
			AIPath = new NavMeshPath();
		}

		private void Update()
		{
			playerLocation = FindPlayer();
		}
		private void LateUpdate()
		{
			if (characterAI != null)
			{
				if (!protectedArea)
				{
					Vector2 distance = new Vector2(AI.transform.position.x - playerLocation.x, AI.transform.position.z - playerLocation.z);
					if (distance.magnitude < characterAI.killRad)
						characterAI.Kill();
				}
			}
		}

		public Vector3 FindPlayer()
		{
			characterAI.NavMeshAgent.CalculatePath(Player.transform.position, AIPath);
			distanceFromPlayer = AIPath.Length();

			return Player.transform.position;
		}

		public IEnumerator IncreaseTension(float inc)
		{
			while (tension < 100f)
			{
				tension = tension + inc;
				yield return new WaitForSeconds(1f);
			}
			tensionHandle = null;
		}

		public IEnumerator ReduceTension(float dec)
		{
			while (tension > 0f)
			{
				tension = tension - dec;
				yield return new WaitForSeconds(1f);
			}
			tensionHandle = null;
		}

		public void WaveOff()
		{
			Interupt();
			characterAI.Interupt();
			characterAI.Destination = LeavePlayer(Player.transform.position);
		}

		public Vector3 LeavePlayer(Vector3 position)
		{
			Vector3 pos = position;
			Vector3 rad = Random.Range(30f, 50f) * Random.insideUnitSphere;
			rad.y = 0;
			Vector3 targetpos = pos + rad;
			if (OnNavMesh(pos + rad))
			{
				Debug.LogError("[WaveOff] Target Pos " + targetpos);
				Debug.Log("[WaveOff] Destination: " + onNMPosition);
				return onNMPosition;
			}
			else
			{
				Vector3 errorPos = pos + rad;
				Debug.LogWarning("[WaveOff] Destination: " + errorPos);
				return LeavePlayer(position);
			}
		}

		public void Interupt()
		{
			if (tensionHandle != null)
			{
				StopCoroutine(tensionHandle);
				tensionHandle = null;
			}
		}

		public Vector3 HintPlayerLocation(Vector3 position, bool procArea)
		{
			Vector3 pos = position;
			Vector3 rad = Random.Range(8f, 15f) * Random.insideUnitSphere;
			rad.y = 0;

			if (!procArea)
			{
				if (OnNavMesh(pos + rad))
				{
					Debug.Log("[HintPlayerLocation] Destination: " + onNMPosition);
					return onNMPosition;
				}
				else
				{
					Vector3 errorPos = pos + rad;
					Debug.LogWarning("[HintPlayerLocation] Destination: " + errorPos);
					return HintPlayerLocation(position, procArea);
				}
			}
			else
			{
				return characterAI.Destination;
			}
		}

		public void GiveDestination(Vector3 position)
		{
			Debug.Log("[GiveDestination] Player near " + position.ToString());
			characterAI.Interupt();
			characterAI.Destination = position;
		}

		public bool IsPlayerNearAlienT1()
		{
			if (AIPath.Length() <= 20)
			{
				return true;
			}
			return false;
		}

		public bool IsPlayerNearAlienT2()
		{
			if (AIPath.Length() <= 10)
			{
				return true;
			}
			return false;
		}
	}
}