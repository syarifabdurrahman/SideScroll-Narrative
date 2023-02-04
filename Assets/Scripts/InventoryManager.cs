using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemData> Items = new List<ItemData>();
    public int itemCount;
    public int itemLimit = 20;

    public ItemController[] inventoryItems;

    [Header("UI")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject inventoryFeedback;
    [SerializeField] private TextMeshProUGUI inventoryFeedbackText;
    [SerializeField] private Image inventoryIconFeedbackImage;

    [Header("Item Settings")]
    [SerializeField] private Transform itemContent;
    [SerializeField] private GameObject pf_InventoryItem;

    private void Awake()
    {
        Instance = this;

        inventoryFeedback.SetActive(false);
    }

    public void add(ItemData item)
    {
        if (itemCount <= itemLimit)
        {
            itemCount++;

            Items.Add(item);
        }
        else
        {
            Debug.Log(" inventory has reached limit, it's 20 max");
        }

    }

    public void remove(ItemData item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        // Cleaning before add
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject go = Instantiate(pf_InventoryItem, itemContent);

            var itemName = go.transform.Find("Item Name").GetComponent<TextMeshProUGUI>();
            var itemIcon = go.transform.Find("Item Icon").GetComponent<Image>();
            var removeButton=go.transform.Find("Remove Btn").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            itemIcon.preserveAspect = true;

            removeButton.gameObject.SetActive(true);
        }

        SetInventoryItems();
    }


    public void SetInventoryItems()
    {
        inventoryItems = itemContent.GetComponentsInChildren<ItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            inventoryItems[i].Additem(Items[i]);
        }
    }

    #region UI Settings

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);

        PlayerMovementController.instance.canMove = false;
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);

        PlayerMovementController.instance.canMove = true;
    }

    public void SettingFeedbackUI(bool isActivated)
    {
        inventoryFeedback.SetActive(isActivated);
        PlayerMovementController.instance.canMove = !isActivated;
    }

    public void UpdateUIFeedback(string itemName, Sprite itemIcon)
    {
        inventoryFeedbackText.text = $"You obtained \"{itemName}\"";
        inventoryIconFeedbackImage.sprite = itemIcon;
    }

    public void CloseFeedbackUI()
    {
        SettingFeedbackUI(false);
    }
    #endregion
}
