using UnityEngine;
using UnityEngine.AI;

public class Interactable : OutlineObject
{
	public float radius = 3f;
	public Transform interactionTransform;

	bool isFocus = false;
	Transform player;

	bool hasInteracted = false;

	protected override void InitValues()
	{
		base.InitValues();
		if (!interactionTransform)
			interactionTransform = transform;
	}

	//Allows interaction with integrable objects
	public virtual void Interact()
	{
		//This method is meant to be overwritten
	}

	void Update()
	{
		if (interactionTransform && isFocus && !hasInteracted)
		{
			float distance = Vector2.Distance(player.position, interactionTransform.position);
			if (distance < radius)
			{
				Interact();
				hasInteracted = true;
			}
		}
	}

	public void OnFocused(Transform playerTransform)
	{
		isFocus = true;
		player = playerTransform;
		hasInteracted = false;
	}

	public void OnDefocused()
	{
		isFocus = false;
		player = null;
		hasInteracted = false;
	}

	private void OnDrawGizmosSelected()
	{
		if(interactionTransform)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(interactionTransform.position, radius);
		}
	}
}
