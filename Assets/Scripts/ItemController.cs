using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public ItemData item;

    public Button removeButton;

    public void RemoveItem()
    {
        InventoryManager.Instance.remove(item);

        Destroy(gameObject);
    }

    public void Additem(ItemData newItem)
    {
        item = newItem;
    }


}
