using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable, //장착
    Consumable,// 소비 아이템
    Resource // 기타아이템
}

public enum ConsumableType
{
    Health,
    Stamina
}
[Serializable]
public class ItemDataConsumbale
{
    public ConsumableType type;
    public float value;
}
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumbale[] consumbales;

    [Header("Equip")]
    public GameObject equipPrefab;
}
