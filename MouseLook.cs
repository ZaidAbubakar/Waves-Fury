using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{


    public float mouseSensitivity = 100f;
   // public Transform Player;
     float xRot = 0f;
     float yRot = 0f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, topClamp, bottomClamp);

        yRot += mouseX;

        transform.localRotation = Quaternion.Euler(xRot, yRot, 0f);

        //Player.Rotate(Vector3.up * mouseX);
        
    }
}
