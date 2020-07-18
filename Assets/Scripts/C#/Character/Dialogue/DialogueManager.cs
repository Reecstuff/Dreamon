using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
    Queue<Transform> positions;
    Queue<string> names;
    Queue<AnimationObject> animations;
    Queue<AudioObject> audios;

    public GameObject continueButton;
    public GameObject endButton;

    public GameObject decisions;
    public UnityEngine.UI.Button[] decisionsButtons = new UnityEngine.UI.Button[3];

    public MinigameManager minigameManager;

    bool end;

    public int selectedOpinion = 0;

    public bool selectMinigame;
    public DialogueTrigger currentDialogObject;
    public PlayerController player;

    public CameraController cameraController;
    Animator currentAnimator;

    /// <summary>
    /// Save the size of one TextLine
    /// </summary>
    float oneTextLineSizeY = 0;

    void Awake()
    {
        sentences = new Queue<string>();
        positions = new Queue<Transform>();
        names = new Queue<string>();
        animations = new Queue<AnimationObject>();
        audios = new Queue<AudioObject>();
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
        DisableButtons(true);

        continueButton.SetActive(true);
        decisions.SetActive(false);

        if (currentTrigger.camPosition != null)
        {
            cameraController.MoveToFixedPosition(currentTrigger.camPosition.position, currentTrigger.transform);
        }
        else
        {
            // Set up Camera between Dialogtrigger, Camera and Player
            cameraController.MoveToFixedPosition(Vector3.Lerp(player.facePoint.position, Vector3.Lerp(currentTrigger.transform.position, cameraController.transform.position, 0.1f), 0.2f), currentTrigger.transform);
        }

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

      

        StartTalk(option);

        end = option.endSentence;

        // SetupButtons to select new options
        for (int i = 0; i < decisionsButtons.Length; i++)
        {
            decisionsButtons[i].onClick.RemoveAllListeners();
            //decisionsButtons[i].onClick.RemoveListener(() => SelectOption(currentTrigger.dialogue[currentTrigger.currentDialogue], currentTrigger));
            decisionsButtons[i].onClick.AddListener(() => SelectOption(currentTrigger.dialogue[currentTrigger.currentDialogue], currentTrigger));
        }

        if (end == true && option.nextMinigame)
        {
            selectMinigame = option.nextMinigame;
            minigameManager = currentTrigger.minigameManager;
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



        StartTalk(option);

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
        string charName = names.Dequeue();
        Transform nextTarget = positions.Dequeue();
        AudioObject audio = audios.Dequeue();
        AnimationObject animation = animations.Dequeue();

        // Set Charactername
        if(!string.IsNullOrEmpty(charName) && !nameText.text.Equals(charName))
        {
            nameText.SetText(charName);
        }

        // Camera
        if(nextTarget && !nextTarget.Equals(cameraController.target))
        {
            cameraController.LerpLookAt(nextTarget);
        }

        // Audio
        if(audio.clip && audio.source)
        {
            // Clip Audio for Dialogue
            audio.source.clip = audio.clip;
            // Play Audio
            audio.source.Play();
        }

        // Animation
        if(!string.IsNullOrEmpty(animation.AnimationStateName))
        {
            if(animation.animator)
                currentAnimator = animation.animator;
            
            if(currentAnimator)
                currentAnimator.CrossFade(animation.AnimationStateName, 0.3f);
        }

        // Stop Typing
        StopAllCoroutines();

        // Start typing
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Animates the words
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.SetText("");

        int lastlineCount = 1;

        for (int i = 0; i < sentence.ToCharArray().Length; i++)
        {

            if (!CheckIsOpen())
                break;

            dialogueText.text += sentence.ToCharArray()[i];

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

    public void DisableButtons(bool enabled = false)
    {

        for (int i = 0; i < decisionsButtons.Length; i++)
        {
            decisionsButtons[i].gameObject.SetActive(enabled);
        }

        continueButton.gameObject.SetActive(enabled);
        endButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Ends the dialog
    /// </summary>
    public void EndDialogue()
    {
        UnityEngine.Cursor.visible = false;
        StopCoroutine(nameof(TypeSentence));
        animator.SetBool("IsOpen", false);

        //Stop focusing any objects
        player.RemoveFocus();

        //Reset the Buttons
        continueButton.SetActive(false);
        decisions.SetActive(false);
        endButton.SetActive(false);

        //Starts Minigame
        if (selectMinigame)
        {
            selectMinigame = false;
            minigameManager.StartNewMinigame();

            if (minigameManager.minigame.name == "Wimmelbild")
            {
                cameraController.MoveToOffset(player.transform);
                cameraController.StartResetCameraToPlayer();
                player.motor.ResumeAgent();
            }
        }
        else
        {
            cameraController.MoveToOffset(player.transform);
            cameraController.StartResetCameraToPlayer();
            player.motor.ResumeAgent();

            if(currentDialogObject)
            {
                if (currentDialogObject.isLost == true)
                {
                    currentDialogObject.TheEnd(true);
                }
                else
                {
                    currentDialogObject.TheEnd(false);
                }
            }

            currentDialogObject = null;
        }
    }

    public bool CheckIsOpen()
    {
        return animator.GetBool("IsOpen");
    }

    /// <summary>
    /// Check for running Dialogue
    /// </summary>
    /// <returns>Returns true if current a dialogue is running</returns>
    public bool CheckIsDialogue()
    {
        return currentDialogObject;
    }

    void StartTalk(Dialogue.Option option)
    {
        sentences.Clear();
        names.Clear();
        positions.Clear();
        animations.Clear();
        audios.Clear();


        // Go through Talks and switch Camera
        for (int i = 0; i < option.talks.Length; i++)
        {
            audios.Enqueue(option.talks[i].audio);
            animations.Enqueue(option.talks[i].animation);
            names.Enqueue(option.talks[i].name);
            positions.Enqueue(option.talks[i].cameraTarget);
            sentences.Enqueue(option.talks[i].sentence);
        }
    }
}
