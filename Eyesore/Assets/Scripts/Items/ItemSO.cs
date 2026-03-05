using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item")]
public class ItemSO : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemSprite;
    public GameObject ItemObject;
}
