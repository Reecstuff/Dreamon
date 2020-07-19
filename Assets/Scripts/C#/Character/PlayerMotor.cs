﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls the movement of the Player
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    Transform target;       //Target to follow
    NavMeshAgent agent;     //Reference to our agent
    Animator animElios;     // Reference to Animationcontroller
    Animator animEgo;

    [SerializeField]
    string[] animStatesElios;

    [SerializeField]
    string[] animIdleStatesEgo;

    [SerializeField]
    AudioClip[] footsteps;

    [SerializeField]
    AudioSource footSource;

    string currentEliosState = string.Empty;
    int currentFootstepIndex = 0;
    string currentEgoState = string.Empty;
    int currentEgoStateCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitValues();
    }

    void InitValues()
    {
        agent = GetComponent<NavMeshAgent>();
        animElios = GetComponentInChildren<Animator>();
        animEgo = GetComponentsInChildren<Animator>()[1];

        if (footSource && footsteps != null)
            footSource.clip = footsteps[0];
    }


    private void Update()
    {
        SwitchMovement();
    }

    /// <summary>
    /// Switch between different animations and movements
    /// </summary>
    void SwitchMovement()
    {
        if (target != null && !agent.isStopped)
        {
            MoveToPoint(target.position);
            FaceTarget();
        }
        if (agent.remainingDistance < 0.05f)
        {
            Idle();
        }
        else if (agent.velocity.magnitude > 0.1f)
            Walk();
    }

    /// <summary>
    /// Player is walking
    /// </summary>
    void Walk()
    {
        if (!agent.isStopped)
        {
            if (footSource)
                PlaySound();

            PlayAnimation(ref animElios, ref animStatesElios[1], ref currentEliosState);
        }
    }

    /// <summary>
    /// Player is not walking
    /// </summary>
    void Idle()
    {
        PlayAnimation(ref animElios, ref animStatesElios[0], ref currentEliosState, true);
        PlayAnimation(ref animEgo, ref animIdleStatesEgo[currentEgoStateCounter], ref currentEgoState, true);


        if(animEgo.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animEgo.IsInTransition(0))
        {
            // Set another Ego animation after an specfic amount of time
            StartCoroutine(WaitforSecondsToSetEgoAnimation(5));
        }

        StopSound();
    }

    
    IEnumerator WaitforSecondsToSetEgoAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // Count in sequence for Ego Idle Animation
        currentEgoStateCounter = currentEgoStateCounter + 1 < animIdleStatesEgo.Length ? currentEgoStateCounter + 1 : 0;
    }


    /// <summary>
    /// Move Player to Point on Layer Ground
    /// </summary>
    /// <param name="point"></param>
    public void MoveToPoint(Vector3 point)
    {
        if(!agent.isStopped)
        {
            agent.SetDestination(point);
        }
    }

    /// <summary>
    /// Stop moving Player on Navmesh
    /// </summary>
    public void StopAgent()
    {
        FaceTarget();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        Idle();
    }

    /// <summary>
    /// Resume Moving Player around
    /// </summary>
    public void ResumeAgent()
    {
        agent.isStopped = false;
    }

    //Moves the player towards the object he wants to interact with
    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * 0.5f;
        agent.updateRotation = false;

        target = newTarget.interactionTransform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }


    //The player turns to the object he wants to interact with so that he looks at it
    void FaceTarget()
    {
        if(target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20f);
        }
    }

    void PlaySound()
    {
        if (!footSource.isPlaying)
            footSource.Play();
    }

    void StopSound()
    {
        footSource.Stop();
    }

    /// <summary>
    /// Play animation from an animator
    /// </summary>
    /// <param name="anim">Animator</param>
    /// <param name="state">Next state</param>
    /// <param name="currentState">Current state</param>
    /// <param name="crossfade">Should the animation crossfade?</param>
    void PlayAnimation(ref Animator anim, ref string state, ref string currentState, bool crossfade = false)
    {
        if(!state.Equals(currentState))
        {
            currentState = state;

            if (crossfade)
                anim.CrossFade(state, 0.3f);
            else
                anim.Play(state);
        }

    }

    /// <summary>
    /// Get the current animationstate of Elios
    /// </summary>
    public string GetAnimationState()
    {
        return currentEliosState;
    }

    /// <summary>
    /// Set the current animationstate of Elios
    /// </summary>
    public void SetAnimationState(string animationState)
    {
        if (animationState.Equals(animStatesElios[1]))
            animElios.Play(animStatesElios[0]);
        else
            animElios.Play(animationState);
    }

    public int GetFootstepIndex()
    {
        return currentFootstepIndex;
    }

    public void ChangeFootstepsSound(int index)
    {
        if (index < footsteps.Length)
        {
            currentFootstepIndex = index;
            footSource.Stop();
            footSource.clip = footsteps[index];
        }
    }
}
