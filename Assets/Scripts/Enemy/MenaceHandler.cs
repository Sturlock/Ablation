using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
	public class MenaceHandler : MonoBehaviour
	{
		[SerializeField] private Single _Menace;

		public Single Menace => _Menace;

		public IEnumerator IncreaseMenace(Single inc)
		{
			while (_Menace < 100f)
			{
				_Menace += inc;
				yield return new WaitForSeconds(1f);
			}
		}

		public IEnumerator ReduceMenace(Single dec)
		{
			while (_Menace > 0f)
			{
				_Menace = Menace - dec;
				yield return new WaitForSeconds(1f);
			}
		}
	}
}