using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    float timeForTextInSeconds = 0.5f;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    public GameObject continueButton;
    public GameObject endButton;

    public GameObject decisions;
    public UnityEngine.UI.Button[] decisionsButtons = new UnityEngine.UI.Button[3];

    public MinigameManager minigameManager;

    bool end;

    public int selectedOpinion;

    public bool selectMinigame;
    public DialogueTrigger currentDialogObject;

    CameraController cameraController;
    PlayerController player;

    /// <summary>
    /// Save the size of one TextLine
    /// </summary>
    float oneTextLineSizeY = 0;

    void Awake()
    {
        sentences = new Queue<string>();
        oneTextLineSizeY = nameText.GetPreferredValues("0").y;
        cameraController = Camera.main.GetComponent<CameraController>();
        player = FindObjectOfType<PlayerController>();
    }

    /// <summary>
    /// Starts the dialog by triggering it from the DialogueTrigger
    /// </summary>
    /// <param name="currentTrigger"></param>
    public void StartDialogue (DialogueTrigger currentTrigger)
    {
        UnityEngine.Cursor.visible = true;
        currentDialogObject = currentTrigger;

        //Reset the Buttons
        continueButton.SetActive(true);
        decisions.SetActive(false);

        // Set up Camera between Dialogtrigger, Camera and Player
        cameraController.MoveToFixedPosition(Vector3.Lerp(player.facePoint.position, Vector3.Lerp(currentTrigger.transform.position, cameraController.transform.position, 0.5f), 0.5f), currentTrigger.interactionTransform);
        player.motor.StopAgent();

        //Opens the first dialog option
        Dialogue.Option option = currentTrigger.dialogue[currentTrigger.currentDialogue].option[0];

        //Checks the number of options and then activates the corresponding buttons
        if (option.decisions.Length != decisionsButtons.Length && option.decisions.Length != 0)
        {
            if (option.decisions.Length == 2)
            {
                decisionsButtons[2].gameObject.SetActive(false);
            }
            if (option.decisions.Length == 1)
            {
                decisionsButtons[1].gameObject.SetActive(false);
                decisionsButtons[2].gameObject.SetActive(false);
            }
        }

        //Changes the corresponding values of the buttons
        for (int i = 0; i < option.decisions.Length; i++)
        {
            //Set text
            decisionsButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(option.decisions[i].ToString());

            //Set decision number
            decisionsButtons[i].GetComponent<DecisionButtons>().decisionNumber = option.nextDecisions[i];
        }

        //Opens the dialog box
        animator.SetBool("IsOpen", true);

        sentences.Clear();

        for (int i = 0; i < option.talks.Length; i++)
        {
            if (!string.IsNullOrEmpty(option.talks[i].name) && !option.talks[i].name.Equals(nameText.text))
                nameText.text = option.talks[i].name;
            sentences.Enqueue(option.talks[i].sentence);
        }

        end = option.endSentence;

        // SetupButtons to select new options
        for (int i = 0; i < decisionsButtons.Length; i++)
        {
            decisionsButtons[i].onClick.RemoveListener(() => SelectOption(currentTrigger.dialogue[currentTrigger.currentDialogue], currentTrigger));
            decisionsButtons[i].onClick.AddListener(() => SelectOption(currentTrigger.dialogue[currentTrigger.currentDialogue], currentTrigger));
        }

        if (end == true && option.nextMinigame)
        {
            selectMinigame = option.nextMinigame;
        }

        DisplayNextSentence();
    }

    /// <summary>
    /// Opens the further dialogues
    /// </summary>
    /// <param name="dialogue"></param>
    /// <param name="currentTrigger"></param>
    public void SelectOption(Dialogue dialogue, DialogueTrigger currentTrigger)
    {
        //Reset the Buttons
        continueButton.SetActive(true);
        decisions.SetActive(false);

        //Opens the next correct dialog option
        Dialogue.Option option = dialogue.option[selectedOpinion];

        //Checks the number of options and then activates the corresponding buttons
        if (option.decisions.Length != decisionsButtons.Length && option.decisions.Length != 0)
        {
            if (option.decisions.Length == 2)
            {
                decisionsButtons[2].gameObject.SetActive(false);
            }
            if (option.decisions.Length == 1)
            {
                decisionsButtons[1].gameObject.SetActive(false);
                decisionsButtons[2].gameObject.SetActive(false);
            }
        }

        //Changes the corresponding values of the buttons
        for (int i = 0; i < option.decisions.Length; i++)
        { 
            //Set text
            decisionsButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(option.decisions[i].ToString());

            //Set decision number
            decisionsButtons[i].GetComponent<DecisionButtons>().decisionNumber = option.nextDecisions[i];
        }

        //Opens the dialog box
        animator.SetBool("IsOpen", true);


        sentences.Clear();

        for (int i = 0; i < option.talks.Length; i++)
        {
            if (!string.IsNullOrEmpty(option.talks[i].name) && !option.talks[i].name.Equals(nameText.text))
                nameText.text = option.talks[i].name;
            sentences.Enqueue(option.talks[i].sentence);
        }

        end = option.endSentence;

        //Checks whether the dialog is over to start a mini-game
        if (end == true)
        {
            selectMinigame = option.nextMinigame;
            minigameManager = currentTrigger.minigameManager;
        }

        DisplayNextSentence();
    }

    /// <summary>
    /// Displays the next sentence
    /// </summary>
    public void DisplayNextSentence()
    {
        if (end == true && sentences.Count == 1)
        {
            continueButton.SetActive(false);
            decisions.SetActive(false);
            endButton.SetActive(true);
        }
        else if (end == false && sentences.Count == 1)
        {
            continueButton.SetActive(false);
            decisions.SetActive(true);
        }

        string sentence = sentences.Dequeue();
        
        // Stop Typing
        StopAllCoroutines();

        // Star typing
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Animates the words
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        int lastlineCount = 1;


        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            // Check for new lines
            if (lastlineCount < dialogueText.textInfo.lineCount)
            {
                dialogueText.ForceMeshUpdate();

                // Resize Dialog rect to match the lines in Text
                dialogueText.rectTransform.sizeDelta = new Vector2(dialogueText.rectTransform.sizeDelta.x, oneTextLineSizeY * (dialogueText.textInfo.lineCount + 1));
                lastlineCount = dialogueText.textInfo.lineCount;
            }
            yield return new WaitForSeconds(timeForTextInSeconds);
        }
    }

    /// <summary>
    /// Ends the dialog
    /// </summary>
    public void EndDialogue()
    {
        UnityEngine.Cursor.visible = false;
        animator.SetBool("IsOpen", false);


        //Stop focusing any objects
        GameObject.Find("Player").GetComponent<PlayerController>().RemoveFocus();

        //Reset the Buttons
        continueButton.SetActive(false);
        decisions.SetActive(false);
        endButton.SetActive(false);

        //Starts Minigame
        if (selectMinigame)
        {
            selectMinigame = false;
            minigameManager.StartNewMinigame();
        }
        else
        {
            cameraController.MoveToOffset(player.transform);
            cameraController.StartResetCameraToPlayer();
            player.motor.ResumeAgent();
            currentDialogObject.TheEnd();
            currentDialogObject = null;
        }
    }
}
