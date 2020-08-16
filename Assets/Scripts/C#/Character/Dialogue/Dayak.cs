using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Needs to be Switched
/// </summary>
public class Dayak : DialogueTrigger
{
    [SerializeField]
    GameObject[] objectsToDeactivate;

    [SerializeField]
    float deathAnimationTime = 6;

    [SerializeField]
    AudioClip dayakTheme;

    AudioClip oldBackgroundMusic;

    protected override void PlaySound()
    {
        base.PlaySound();

        if(AudioManager.Instance && dayakTheme)
        {
            oldBackgroundMusic = AudioManager.Instance.GetSourceClip();
            AudioManager.Instance.SetSourceClip(dayakTheme);
        }
    }

    public override void TheEnd(bool isLose)
    {
        base.TheEnd(isLose);


        Debug.Log(string.Join(" ", gameObject.name, nameof(isLose),  isLose));

        Sequence s = DOTween.Sequence();

        // Fly Away
        s.Append(transform.DOScale(Vector3.zero, deathAnimationTime));
        s.Play();

        if (objectsToDeactivate.Length > 0)
        {
            for (int i = 0; i < objectsToDeactivate.Length; i++)
            {
                objectsToDeactivate[i].SetActive(false);
            }
        }

        if(AudioManager.Instance)
        {
            AudioManager.Instance.SetSourceClip(oldBackgroundMusic);
        }

        Invoke(nameof(SetInactive), deathAnimationTime + 0.5f);

    }

    public override void SetEndState(bool isLose)
    {
        base.SetEndState(isLose);
        TheEnd(isLose);
    }
}
