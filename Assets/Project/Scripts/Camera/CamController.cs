using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public bool lockedMouse = true;
    public LayerMask playerLayer;
    private Vector3 cameraMoveSpeed = Vector3.zero;
    public Transform followObject;
    public float limitAngle = 65;
    public float inputSensitX = 150;
    public float inputSensitY = 75;
    private float mouseX;
    private float mouseY;
    private float rotX;
    private float rotY;
    private Vector3 rot;
    private KeyCode RightMouseButton = KeyCode.Mouse1;
    private Quaternion localRot;

    private Vector3 speed = Vector3.zero;
    private RaycastHit hit;
    private Camera cam;
    public Transform posCamera;
    void Start()
    {
        Init();
        cam = Camera.main;
        if (!lockedMouse)
        {
            return;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    
    void Update()
    {
        UpdateCam();
    }

    private void LateUpdate()
    {
        transform.position = followObject.position;
        cam.gameObject.transform.LookAt(followObject.position);
        if(!Physics.Linecast(posCamera.position, transform.position) || Physics.Linecast(posCamera.position, transform.position, playerLayer))
        {
            cam.gameObject.transform.position = Vector3.SmoothDamp(cam.gameObject.transform.position, posCamera.position, ref cameraMoveSpeed, 0.1f);
            Debug.DrawLine(posCamera.position, transform.position);
        }else if (Physics.Linecast(posCamera.position, transform.position, out hit))
        {
            cam.gameObject.transform.position = Vector3.SmoothDamp(cam.gameObject.transform.position, hit.point, ref cameraMoveSpeed, 0.1f);
            Debug.DrawLine(hit.point, cam.gameObject.transform.position);
        }
    }

    private void Init()
    {
        rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
    }

    private void UpdateCam()
    {
        if(Input.GetKey(RightMouseButton))
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            rotY += mouseX * inputSensitY * Time.deltaTime;
            rotX += mouseY * inputSensitX * Time.deltaTime;
            rotX = Mathf.Clamp(rotX, -limitAngle, limitAngle);
            localRot = Quaternion.Euler(rotX, rotY, 0);
            transform.rotation = localRot;
        }
    }
}
