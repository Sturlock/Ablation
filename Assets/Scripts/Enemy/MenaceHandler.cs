// // --------------------------------
// // -- File Created 	: 22:10 15/10/2025
// // -- File Part of the Ablation Solution, project Assembly-CSharp
// // -- Edited By : Will Sturley
// // --------------------------------

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
			while (Menace < 100f)
			{
				_Menace += inc;
				yield return new WaitForSeconds(1f);
			}
		}

		public IEnumerator ReduceMenace(Single dec)
		{
			while (Menace > 0f)
			{
				Menace = Menace - dec;
				yield return new WaitForSeconds(1f);
			}
		}
	}
}