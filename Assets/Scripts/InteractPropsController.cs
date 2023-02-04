using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractPropsController : MonoBehaviour
{
    public InteractPropsData interactPropsData;
    private Animator anim;
    public bool isCollide { set; private get; }
    private PlayerMovementController player;

    [Header("Chest Controller")]
    [SerializeField] private bool isChest;
    [SerializeField] private List<GameObject> pf_itemInventory= new List<GameObject>();
    [SerializeField] private Transform pf_itemTransformPosition;
    private BoxCollider2D chestBoxCol;

    private void Awake()
    {
        if (isChest)
        {
            anim = GetComponent<Animator>();
            chestBoxCol = GetComponent<BoxCollider2D>();
        }
    }


    private void Update()
    {
        if (isCollide && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

     async private void Interact()
    {
        if (!isChest)
        {
            InteractPropsManager.Instance.UpdateUI(interactPropsData.propName, interactPropsData.description);
            InteractPropsManager.Instance.OpenInteractPanel(true);

            player.emotesCanvas.SetActive(false);
        }
        else
        {
            //add something
            anim.SetTrigger("Open");
            await Task.Delay(1200);

            InteractPropsManager.Instance.UpdateUI(interactPropsData.propName, interactPropsData.description);
            InteractPropsManager.Instance.OpenInteractPanel(true);

            player.emotesCanvas.SetActive(false);

            int randomNumber = Random.Range(0, pf_itemInventory.Count);
            Debug.Log(randomNumber);

            Instantiate(pf_itemInventory[randomNumber], pf_itemTransformPosition.position, Quaternion.identity);
            chestBoxCol.enabled = false;
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
