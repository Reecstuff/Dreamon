using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// Talk with a delay for longer readingtime
/// </summary>
[System.Serializable]
public class TimedTalk : Dialogue.Talk
{
    /// <summary>
    /// Is added to readingtime
    /// </summary>
    public float delay = 0;
    public int resultIndex = 0;
}


/// <summary>
/// Shows timed Text between Dialogues
/// </summary>
public class BetweenText : MonoBehaviour
{
    [SerializeField]
    DialogueManager dialogueManager;

    [SerializeField]
    TextMeshProUGUI nameField, textField;

    [SerializeField]
    float timeToRead = 5;

    List<TimedTalk> currentTalks;

    float oneTextLineSizeY = 0;

    int currentCount = 0;
    string currentName;
    Animator currentAnimator;
    AudioSource currentAudioSource;


    void Awake()
    {
        InitValues();
    }


    /// <summary>
    /// Set text to show on UI
    /// </summary>
    /// <param name="name">Name of Character</param>
    /// <param name="text">Text of Character</param>
    public void SetText(TimedTalk[] talks)
    {
        currentTalks.AddRange(talks);
        currentCount = currentTalks.Count;

        StopCoroutine(GoThroughDialogues());
        StartCoroutine(GoThroughDialogues());
    }

    public void ClearText()
    {
        StopCoroutine(GoThroughDialogues());
        currentCount = 0;
        currentTalks.Clear();
        dialogueManager.animator.SetBool("IsOpen", false);
    }

    void InitValues()
    {
        oneTextLineSizeY = nameField.GetPreferredValues("0").y;
        currentTalks = new List<TimedTalk>();
    }

    IEnumerator GoThroughDialogues()
    {
        int i = 0;

        while(i < currentCount)
        {
            if (currentTalks.Count <= 0)
                break;

            // Delay before Betweentext
            yield return new WaitForSeconds(2);

            if (!dialogueManager.CheckIsOpen())
            {
                // Play Text
                if (!string.IsNullOrEmpty(currentTalks[i].sentence))
                ShowText(currentTalks[i].name, currentTalks[i].sentence);

                // Play Animation
                if (!string.IsNullOrEmpty(currentTalks[i].animation.AnimationStateName))
                    ShowAnimation(currentTalks[i].animation.AnimationStateName, currentTalks[i].animation.animator);

                // Play Audio
                if (currentTalks[i].audio.clip)
                    PlayAudio(currentTalks[i].audio.source, currentTalks[i].audio.clip);

                yield return new WaitForSeconds(timeToRead + currentTalks[i].delay);
                i++;
                currentCount = currentTalks.Count;
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }

        currentTalks.Clear();
    }

    /// <summary>
    /// Show Text on Textfield
    /// </summary>
    void ShowText(string name, string text)
    {
        StopCoroutine(TypeSentence(string.Empty));

        dialogueManager.DisableButtons();
        dialogueManager.animator.SetBool("IsOpen", true);

        
        if (!string.IsNullOrEmpty(name))
            currentName = name;

        if(!string.IsNullOrEmpty(currentName))
            nameField.SetText(currentName);

        if(!string.IsNullOrEmpty(text))
            StartCoroutine(TypeSentence(text));

        StartCoroutine(CloseDialogue(text));
    }

    /// <summary>
    /// Show an Animation
    /// </summary>
    /// <param name="animationState">State of the animated Object</param>
    /// <param name="animator">Animator</param>
    void ShowAnimation(string animationState, Animator animator)
    {
        if (animator)
            currentAnimator = animator;

        if (currentAnimator)
            currentAnimator.CrossFade(animationState, 0.3f);
    }

    /// <summary>
    /// Play audio at a specific Source
    /// </summary>
    /// <param name="source">Audiosource</param>
    /// <param name="clip">Audioclip</param>
    void PlayAudio(AudioSource source, AudioClip clip)
    {
        if (source)
            currentAudioSource = source;

        if (currentAudioSource)
        {
            currentAudioSource.clip = clip;
            currentAudioSource.Play();
        }
    }


    /// <summary>
    /// Animates the words
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    IEnumerator TypeSentence(string sentence)
    {
        textField.SetText(string.Empty);

        int lastlineCount = 1;

        for (int i = 0; i < sentence.ToCharArray().Length; i++)
        {
            if (!dialogueManager.CheckIsOpen())
                break;

            textField.text += sentence.ToCharArray()[i];


            // Check for new lines
            if (lastlineCount < textField.textInfo.lineCount)
            {
                textField.ForceMeshUpdate();

                // Resize Dialog rect to match the lines in Text
                textField.rectTransform.sizeDelta = new Vector2(textField.rectTransform.sizeDelta.x, oneTextLineSizeY * (textField.textInfo.lineCount + 1));
                lastlineCount = textField.textInfo.lineCount;
            }

            yield return new WaitForSeconds(0.02f);
        }
    }

    /// <summary>
    /// Clear text after a specific amount of time
    /// </summary>
    IEnumerator CloseDialogue(string text)
    {
        yield return new WaitForSeconds(timeToRead);


        if(textField.text.Equals(text))
            dialogueManager.animator.SetBool("IsOpen", false);
    }
}
