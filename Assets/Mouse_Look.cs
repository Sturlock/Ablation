
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Mouse_Look : MonoBehaviour
{
   
    public AxisState m_HorizontalAxis;
    public Transform playerBody;

    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        

        transform.localRotation = Quaternion.Euler(m_HorizontalAxis.Value, 0f, 0f);
        playerBody.Rotate(Vector3.up * m_HorizontalAxis.Value);
    }
}
