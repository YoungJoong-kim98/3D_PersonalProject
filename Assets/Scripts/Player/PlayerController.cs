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
    public float dashForce = 20f; // ��� ��

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true; //�κ��丮 Ŀ���� ����

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
    void Move() //�̵� ����
    {
        //forward�� player�� z���� curMovementInput�� y���� ������ �������ϴ� wŰ�� ������ (0,0,1) * 1 �� ������
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; //���Ⱚ�� ������

        dir *= moveSpeed; // ���� ������
        dir.y = _rigidbody.velocity.y; //������ ���� ���� �� �Ʒ��� ���������ϴϱ� �� ���� ��� ���������ֱ� ���ؼ� ����

        _rigidbody.velocity = dir;
    }

    void CameraLook() // ī�޶� �ٶ󺸴� ����
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

    bool IsGrounded() // ��������
    {
        Ray[] rays = new Ray[4] // �÷��̾��� �� ����(��, ��, ��, ��)���� �Ʒ��� ���̸� ��
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up*0.01f) ,Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up*0.01f) ,Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up*0.01f) ,Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up*0.01f) ,Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.9f, groundLayerMask)) //���� �浹�ϸ� true ��ȯ
            {
                return true; // ť�� ũ�Ⱑ ��� ���� ������ ���̸� 0.9���� ��
            }
        }
        return false; // �� ���� Ray ��� �浹���� ������ false ��ȯ (���߿� ����)

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
        Vector3 dashDirection = transform.forward; // ���� �ٶ󺸴� ����
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
