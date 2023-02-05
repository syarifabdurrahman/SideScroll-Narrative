using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using System;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.UI;

[System.Serializable]
public class PortraitImage
{
    public string name;
    public Sprite imageSprite;
}

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager Instance;

    [Header("DialogueUI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Image displayImage;

    public List<PortraitImage> portraitImages;

    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    private List<Choice> currentChoice;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";


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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJson)
    {
        Cursor.visible = false;

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

        Cursor.visible = true;
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            PlayerMovementController.instance.canMove = false;
            dialogueText.text = currentStory.Continue();

            // Displaying choices if any
            DisplayChoices();

            //Handle Tags
            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogue());
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            // parse
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag cant be parsed :" + tag);
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    foreach (var portrait in portraitImages)
                    {
                        if (portrait.name == tagValue)
                        {
                            displayImage.sprite = portrait.imageSprite;
                        }
                    }
                    break;
                case LAYOUT_TAG:
                    Debug.Log("layout = " + tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but not currently handled:" + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        currentChoice = currentStory.currentChoices;

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

        StartCoroutine(SelectChoice(0));
    }

    private IEnumerator SelectChoice(int index)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[index].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
