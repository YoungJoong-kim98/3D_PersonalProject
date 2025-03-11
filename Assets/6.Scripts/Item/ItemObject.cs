using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = data; //플레이어에게 아이템 정보를 넘겨줌
        CharacterManager.Instance.Player.addItem?.Invoke();
        Debug.Log(gameObject.name);
        Destroy(gameObject);
    }
}
