using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private PlayerMovementController player;
    public ItemData item;
    public bool isCollide { set; private get; }

    private void pickup()
    {
        InventoryManager.Instance.add(item);
        Destroy(gameObject);


        InventoryManager.Instance.SettingFeedbackUI(true);
        InventoryManager.Instance.UpdateUIFeedback(item.itemName, item.icon);
        player.emotesCanvas.SetActive(false);
    }

    private void Update()
    {
        if (isCollide && Input.GetKeyDown(KeyCode.E))
        {
            pickup();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerMovementController>();
            player.emotesCanvas.SetActive(true);

            isCollide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerMovementController>();
            player.emotesCanvas.SetActive(false);

            isCollide = false;
        }
    }
}
