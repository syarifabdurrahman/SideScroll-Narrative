using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using System;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager Instance;

    [Header("DialogueUI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return Instance;
    }


    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        //Getting all choices
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        // return if isnt play
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJson)
    {
        currentStory = new Story(inkJson.text);
        dialogueIsPlaying = true;

        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private IEnumerator ExitDialogue()
    {
        yield return new WaitForSeconds(.2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        PlayerMovementController.instance.canMove = true;
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            PlayerMovementController.instance.canMove = false;
            dialogueText.text = currentStory.Continue();

            DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogue());
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoice = currentStory.currentChoices;

        if (currentChoice.Count > choices.Length)
        {
            Debug.LogError("Error the choices on ink has more that UI choice, number choice given: " + currentChoice.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoice)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        // through the remaining choices the UI support the are hidded
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());

    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);

        //ContinueStory();
    }
}
