
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Mouse_Look : MonoBehaviour
{
    [SerializeField] private CinemachinePOV pOV;
    [SerializeField] private CinemachineVirtualCamera vcam;
    
    public Transform playerBody;

    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pOV = vcam.GetCinemachineComponent<CinemachinePOV>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, pOV.m_HorizontalAxis.Value, 0f);
    }
}
