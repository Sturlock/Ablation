using Cinemachine;
using UnityEngine;

public class Mouse_Look : MonoBehaviour
{
    [SerializeField] private CinemachinePOV pOV;
    [SerializeField] private CinemachineVirtualCamera vcam;
    private ShowHideUI_Canvas ShowHideUI;

    public Transform playerBody;

    // Start is called before the first frame update
    public void Start()
    {
        ShowHideUI = FindObjectOfType<ShowHideUI_Canvas>();
        Cursor.lockState = CursorLockMode.Locked;
        pOV = vcam.GetCinemachineComponent<CinemachinePOV>();
    }

    // Update is called once per frame
    public void Update()
    {
        //if (!ShowHideUI.Retract)
        //{
            transform.rotation = Quaternion.Euler(0f, pOV.m_HorizontalAxis.Value, 0f);
        //}

        //if (!ShowHideUI.Retract)
        //{
        //    pOV.enabled = true;
        //}
        //else
        //{
        //    pOV.enabled = false;
        //}
    }
}