using Cinemachine;
using UnityEngine;

public class Mouse_Look : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private CinemachinePOV pOV;
    [SerializeField] private CinemachineHardLookAt lookAt;
    [SerializeField] private ShowHideUI_Canvas _showHideUI;
    HandyMan _handyMan;

    // Start is called before the first frame update
    public void Start()
    {
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        pOV = vcam.GetCinemachineComponent<CinemachinePOV>();
        _handyMan = FindObjectOfType<HandyMan>();
        _showHideUI = FindObjectOfType<ShowHideUI_Canvas>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    public void Update()
    {
        if (!_showHideUI.Show)
        {
            transform.rotation = Quaternion.Euler(0f, pOV.m_HorizontalAxis.Value, 0f);
            
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
}