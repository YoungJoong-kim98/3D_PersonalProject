using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Equipment : MonoBehaviour
{
    public Equip curEquip;
    public Transform equipParent;

    private PlayerController controller;
    private PlayerCondition condition;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();

    }

    public void EquipNew(ItemData data)
    {
        UnEquip();
        curEquip = Instantiate(data.equipPrefab, equipParent).GetComponent<Equip>();
        if(data.speedUp)  // 장착한 아이템이 스피드업이 true이면
        {
            controller.moveSpeed += data.equipSpeed;
        }

    }

    public void UnEquip()
    {
        controller.moveSpeed = controller.startSpeed;
        if (curEquip != null)
        {
            Destroy(curEquip.gameObject);
            curEquip = null;
        }

    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && curEquip != null && controller.canLook)
        {
            curEquip.OnAttackInput();
        }
    }
}
