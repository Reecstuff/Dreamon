using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    Transform target;       //Target to follow
    NavMeshAgent agent;     //Reference to our agent
    Animator anim;          // Reference to Animationcontroller

    [SerializeField]
    string[] animStates;

    [SerializeField]
    AudioClip[] footsteps;

    [SerializeField]
    AudioSource footSource;

    int currentAnimationIndex = 0;
    int currentFootstepIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        if (footSource && footsteps != null)
            footSource.clip = footsteps[0];
    }

    private void Update()
    {
        if (target != null && !agent.isStopped)
        {
            MoveToPoint(target.position);
            FaceTarget();
        }
        if (agent.remainingDistance < 0.1f)
        {
            Idle();
        }
        else if (agent.velocity.magnitude > 0.1f)
            Walk();

    }

    void Walk()
    {
        if (agent.isStopped)
            return;

        if (footSource)
            PlaySound();

        PlayAnimation(1);
    }

    void Idle()
    {
        PlayAnimation(0, true);
        StopSound();
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

    public void PlayAnimation(int index, bool crossfade = false)
    {
        if(anim)
        {
            if(currentAnimationIndex != index)
            {
                currentAnimationIndex = index;
                if (crossfade)
                {
                    anim.CrossFade(animStates[index], 0.3f);
                }
                else
                {
                    anim.Play(animStates[index]);
                }
            }
        }
    }

    public string GetAnimationState()
    {
        return animStates[currentAnimationIndex];
    }

    public void SetAnimationState(string animationState)
    {
        if (animationState.Equals(animStates[1]))
            anim.Play(animStates[0]);
        else
            anim.Play(animationState);
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
