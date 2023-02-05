using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink Json")]
    [SerializeField] private TextAsset inkJson;

    private bool playerInRange;


    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(inkJson.text);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

}

