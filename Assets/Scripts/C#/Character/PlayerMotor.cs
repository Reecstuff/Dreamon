﻿using System.Collections;
using System.Collections.Generic;
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

    int currentAnimationIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (target != null && !agent.isStopped)
        {
            MoveToPoint(target.position);
            FaceTarget();

           
        }
        if(agent.remainingDistance < 0.1f)
        {
            PlayAnimation(0, true);
        }
        else if (agent.velocity.magnitude > 0.1f)
            PlayAnimation(1);

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
        PlayAnimation(0, true);
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
}
