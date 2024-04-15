using Cinemachine;
using UI;
using UnityEngine;

namespace Player
{
	public class Mouse_Look : MonoBehaviour
	{
		public Animator _animator;

		[SerializeField] private CinemachineVirtualCamera vcam1;
		[SerializeField] private CinemachineVirtualCamera vcam2;
		[SerializeField] private CinemachinePOV pOV;
		[SerializeField] private CinemachineHardLookAt lookAt;
		[SerializeField] private ShowHideHandy _showHideUI;
		private HandyMan _handyMan;

		// Start is called before the first frame update
		public void Start()
		{
			//vcam = FindObjectOfType<CinemachineVirtualCamera>();
			pOV = vcam1.GetCinemachineComponent<CinemachinePOV>();
			_handyMan = FindObjectOfType<HandyMan>();
			//_showHideUI = FindObjectOfType<ShowHideUI_Canvas>();
			Cursor.lockState = CursorLockMode.Locked;
			pOV.m_HorizontalAxis.Value = transform.rotation.y;
		}

		// Update is called once per frame
		public void Update()
		{
			if (!_showHideUI.Show)
			{
				if (pOV == null)
					pOV = vcam1.GetCinemachineComponent<CinemachinePOV>();
				if (pOV != null)
					transform.rotation = Quaternion.Euler(0f, pOV.m_HorizontalAxis.Value, 0f);
			}
			else
			{
				//vcam.LookAt = _handyMan.transform;
			}
		}

		public void FOVChange(bool fov)
		{
			_animator.SetBool("Change", fov);
		}
	}
}