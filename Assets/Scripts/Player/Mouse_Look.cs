using Cinemachine;
using UnityEngine;

public class Mouse_Look : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private CinemachinePOV pOV;
    [SerializeField] private CinemachineHardLookAt lookAt;
    [SerializeField] private ShowHideHandy _showHideUI;
    private HandyMan _handyMan;

    // Start is called before the first frame update
    public void Start()
    {
        //vcam = FindObjectOfType<CinemachineVirtualCamera>();
        pOV = vcam.GetCinemachineComponent<CinemachinePOV>();
        _handyMan = FindObjectOfType<HandyMan>();
        //_showHideUI = FindObjectOfType<ShowHideUI_Canvas>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    public void Update()
    {
        if (!_showHideUI.Show)
        {
            if (pOV == null)
                pOV = vcam.GetCinemachineComponent<CinemachinePOV>();
            if (pOV != null)
                transform.rotation = Quaternion.Euler(0f, pOV.m_HorizontalAxis.Value +120f, 0f);
        }
        else
        {
            vcam.LookAt = _handyMan.transform.parent.transform;
        }
    }

    public void AddCineComp(bool _retract)
    {
        if (_retract)
        {
            lookAt = vcam.AddCinemachineComponent<CinemachineHardLookAt>();
        }
        else
        {
            pOV = vcam.AddCinemachineComponent<CinemachinePOV>();
        }
    }

    public void FOVChange(float fov)
    {
        vcam.m_Lens.FieldOfView = fov;
    }
}