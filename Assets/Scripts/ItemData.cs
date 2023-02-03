using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item Data")]
public class ItemData : ScriptableObject
{
    public int id;
    public string itemName;
    //public int value
    public Sprite icon;
}
