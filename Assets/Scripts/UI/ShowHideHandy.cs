using System.Collections;
using Player;
using UnityEngine;

namespace UI
{
	public class ShowHideHandy : MonoBehaviour
	{
		public bool can;
		public Mouse_Look _look;
		public AccessHM _hm;
		public GameObject _hmPrefab;
		[SerializeField] private KeyCode toggleKey = KeyCode.Tab;
		[SerializeField] private GameObject uiContainer = null;
		[SerializeField] private bool show = false;
		private bool aniShow = false;

		public bool Show
		{
			get => show;
		}

		public bool AniShow
		{
			get => aniShow;
		}

		// Start is called before the first frame update
		private void Start()
		{
			show = false;
			aniShow = false;
		}

		private void CursorState(bool bShow)
		{
			if (bShow)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = bShow;
			}
			else
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = bShow;
			}
		}

		// Update is called once per frame
		private void Update()
		{
			if (Input.GetKeyDown(toggleKey))
			{
				StartCoroutine(Toggle());
			}
			if (!can)
			{
				_hmPrefab.SetActive(false);
			}
			else
			{
				_hmPrefab.SetActive(true);
			}
		}

		public IEnumerator Toggle()
		{
			if (can)
			{
				aniShow = !aniShow;

				show = !show;
				//_look.AddCineComp(aniShow);
				_look.FOVChange(aniShow);
				_hm.AccessHandy(aniShow);
				uiContainer.GetComponent<Animator>().SetBool("retract", aniShow);
				CursorState(aniShow);
				yield return new WaitForSeconds(.1f);
				show = !show;
			}
        
		}
	}
}