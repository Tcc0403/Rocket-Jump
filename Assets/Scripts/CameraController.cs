using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float mouseSensitivity = 100.0f;
    public float xRotation = 0.0f;
    void Start()
    {

    }

    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        this.transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);// look up and down
        PlayerController playerController = playerTransform.GetComponent<PlayerController>();
        playerController.xRotation = xRotation;
        playerTransform.Rotate(Vector3.up * mouseX);// look left or right
    }
}
