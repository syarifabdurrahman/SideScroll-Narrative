using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractPropsManager : MonoBehaviour
{
    public static InteractPropsManager Instance;

    [Header("UI Settings")]
    [SerializeField] private GameObject InteractPanel;
    [SerializeField] private TextMeshProUGUI descriptionUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenInteractPanel(bool isOpen)
    {
        InteractPanel.SetActive(isOpen);

        PlayerMovementController.instance.canMove = false;
    }

    public void CloseInteractPanel()
    {
        InteractPanel.SetActive(false);

        PlayerMovementController.instance.canMove = true;
    }

    public void UpdateUI(string propName, string description)
    {
        descriptionUI.text = $"The \"{propName}\", {description}";
    }
}
