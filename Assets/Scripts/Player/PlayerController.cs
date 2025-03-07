using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Moverment")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;


    [Header("Dash")]
    public float dashForce = 20f; // 대시 힘

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true; //인벤토리 커서용 변수

    public Action inventory;
    private Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }
    void Move() //이동 로직
    {
        //forward는 player의 z방향 curMovementInput은 y방향 앞으로 가려고하니 w키를 누르면 (0,0,1) * 1 이 곱해짐
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; //방향값을 정해줌

        dir *= moveSpeed; // 힘을 곱해줌
        dir.y = _rigidbody.velocity.y; //점프를 했을 때만 위 아래로 움직여야하니까 그 값을 계속 유지시켜주기 위해서 적용

        _rigidbody.velocity = dir;
    }

    void CameraLook() // 카메라 바라보는 로직
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started&& IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    bool IsGrounded() // 점프로직
    {
        Ray[] rays = new Ray[4] // 플레이어의 네 방향(앞, 뒤, 왼, 오)에서 아래로 레이를 쏨
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up*0.01f) ,Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up*0.01f) ,Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up*0.01f) ,Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up*0.01f) ,Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.9f, groundLayerMask)) //땅과 충돌하면 true 반환
            {
                return true; // 큐브 크기가 어느 정도 있으니 레이를 0.9정도 쏨
            }
        }
        return false; // 네 개의 Ray 모두 충돌하지 않으면 false 반환 (공중에 있음)

    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Dash();
        }
    }

    void Dash()
    {
        Vector3 dashDirection = transform.forward; // 현재 바라보는 방향
        _rigidbody.AddForce(dashDirection * dashForce, ForceMode.Impulse);
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }
    public void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    
}
