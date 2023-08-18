using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private float InputX;
    private float InputZ;
    private Vector3 dirMovDesired;
    public float speedRotDesired = 0.1f;
    private float Speed;
    public float allowRotPlayer = 0.3f;
    private Camera cam;
    private float verticalSpeed;
    private Vector3 moveVector;

    void Start()
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
    }


    void Update()
    {
        InputMagnitude();
    }

    private void PlayerMovement()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Vector3 front = cam.transform.forward;
        Vector3 right = cam.transform.right;
        front.Normalize();
        right.Normalize();
        dirMovDesired = front * InputZ + right * InputX;
        dirMovDesired = new Vector3(dirMovDesired.x, 0, dirMovDesired.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirMovDesired), speedRotDesired);
    }

    private void InputMagnitude()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        animator.SetFloat("Z", InputZ, 0.0f, Time.deltaTime * 2);
        animator.SetFloat("X", InputX, 0.0f, Time.deltaTime * 2);

        Speed = new Vector2(InputX, InputZ).sqrMagnitude;
        if(Input.GetKey(KeyCode.LeftShift) && Speed > 0.3)
        {
            Speed = 2;
        }else if (!Input.GetKey(KeyCode.LeftShift) && Speed > 0.3)
        {
            Speed = 1;
        }

        if (Speed > allowRotPlayer)
        {
            animator.SetFloat("InputMagnitude", Speed, 0.1f, Time.deltaTime);
            PlayerMovement();
        }
        else if (Speed < allowRotPlayer)
        {
            animator.SetFloat("InputMagnitude", Speed, 0.1f, Time.deltaTime);
        }
    }
}