using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public Button[] decisionsButtons = new Button[3];


    bool end;

    public int selectedOpinion;

    /// <summary>
    /// Save the size of one TextLine
    /// </summary>
    float oneTextLineSizeY = 0;

    void Start()
    {
        sentences = new Queue<string>();
        oneTextLineSizeY = nameText.GetPreferredValues("0").y;
    }

    // Set DialogTrigger instead of only Dialog
    public void StartDialogue (DialogueTrigger currentTrigger)
    {
        continueButton.SetActive(true);
        decisions.SetActive(false);

        Dialogue.Opinion opinion = currentTrigger.dialogue.opinion[0];

        if (opinion.decisions.Length != decisionsButtons.Length && opinion.decisions.Length != 0)
        {
            if (opinion.decisions.Length == 2)
            {
                decisionsButtons[2].gameObject.SetActive(false);
            }
            if (opinion.decisions.Length == 1)
            {
                decisionsButtons[1].gameObject.SetActive(false);
                decisionsButtons[2].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < opinion.decisions.Length; i++)
        {
            decisionsButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(opinion.decisions[i].ToString());

            decisionsButtons[i].GetComponent<DecisionButtons>().decisionNumber = opinion.nextDecisions[i];
        }

        animator.SetBool("IsOpen", true);

        nameText.text = opinion.name;

        sentences.Clear();

        foreach (string sentence in opinion.sentences)
        {
            sentences.Enqueue(sentence);
        }

        end = opinion.endSentence;

        // SetupButtons to select new Opinion
        for (int i = 0; i < decisionsButtons.Length; i++)
        {
            decisionsButtons[i].onClick.RemoveListener(() => SelectOpinion(currentTrigger.dialogue));
            decisionsButtons[i].onClick.AddListener(() => SelectOpinion(currentTrigger.dialogue));

        }

        DisplayNextSentence();
    }

    public void SelectOpinion(Dialogue dialogue)
    {
        continueButton.SetActive(true);
        decisions.SetActive(false);

        Dialogue.Opinion opinion = dialogue.opinion[selectedOpinion];

        if (opinion.decisions.Length != decisionsButtons.Length && opinion.decisions.Length != 0)
        {
            if (opinion.decisions.Length == 2)
            {
                decisionsButtons[2].gameObject.SetActive(false);
            }
            if (opinion.decisions.Length == 1)
            {
                decisionsButtons[1].gameObject.SetActive(false);
                decisionsButtons[2].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < opinion.decisions.Length; i++)
        {
            
            // Set Text
            decisionsButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(opinion.decisions[i].ToString());

            decisionsButtons[i].GetComponent<DecisionButtons>().decisionNumber = opinion.nextDecisions[i];
        }

        animator.SetBool("IsOpen", true);

        nameText.text = opinion.name;

        sentences.Clear();

        foreach (string sentence in opinion.sentences)
        {
            sentences.Enqueue(sentence);
        }

        end = opinion.endSentence;

        DisplayNextSentence();
    }

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

    //Animate the words
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
                dialogueText.rectTransform.sizeDelta = new Vector2(
                    dialogueText.rectTransform.sizeDelta.x,
                    oneTextLineSizeY * (dialogueText.textInfo.lineCount + 1)
                    );
                lastlineCount = dialogueText.textInfo.lineCount;
                
            }
            yield return new WaitForSeconds(timeForTextInSeconds);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);

        //Stop focusing any objects
        GameObject.Find("Player").GetComponent<PlayerController>().RemoveFocus();

        continueButton.SetActive(true);
        decisions.SetActive(true);
        endButton.SetActive(false);
    }
}
