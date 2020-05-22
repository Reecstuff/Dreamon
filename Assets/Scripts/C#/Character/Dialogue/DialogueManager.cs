using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    public GameObject continueButton;
    public GameObject endButton;

    public GameObject decisions;
    public GameObject[] decisionsButtons = new GameObject[3];

    bool end;

    public int selectedOpinion;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        continueButton.SetActive(true);
        decisions.SetActive(false);

        Dialogue.Opinion opinion = dialogue.opinion[0];

        if (opinion.decisions.Length != decisionsButtons.Length && opinion.decisions.Length != 0)
        {
            if (opinion.decisions.Length == 2)
            {
                decisionsButtons[2].SetActive(false);
            }
            if (opinion.decisions.Length == 1)
            {
                decisionsButtons[1].SetActive(false);
                decisionsButtons[2].SetActive(false);
            }
        }

        for (int i = 0; i < opinion.decisions.Length; i++)
        {
            decisionsButtons[i].GetComponentInChildren<Text>().text = opinion.decisions[i].ToString();

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

    public void SelectOpinion(Dialogue dialogue)
    {
        continueButton.SetActive(true);
        decisions.SetActive(false);

        Dialogue.Opinion opinion = dialogue.opinion[selectedOpinion];

        if (opinion.decisions.Length != decisionsButtons.Length && opinion.decisions.Length != 0)
        {
            if (opinion.decisions.Length == 2)
            {
                decisionsButtons[2].SetActive(false);
            }
            if (opinion.decisions.Length == 1)
            {
                decisionsButtons[1].SetActive(false);
                decisionsButtons[2].SetActive(false);
            }
        }

        for (int i = 0; i < opinion.decisions.Length; i++)
        {
            decisionsButtons[i].GetComponentInChildren<Text>().text = opinion.decisions[i].ToString();

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
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //Animate the words
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
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
