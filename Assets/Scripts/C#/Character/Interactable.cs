using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Objects that the player can interact with
/// </summary>
public class Interactable : OutlineObject
{
	public float radius = 3f;
	public Transform interactionTransform;

	bool isFocus = false;
	Transform player;

	bool hasFocused = true;

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
		CheckForInteraction();
	}

	void CheckForInteraction()
    {
		if (interactionTransform && isFocus && !hasFocused)
		{
			float distance = Vector2.Distance(new Vector2(player.position.x, player.position.z), new Vector2(interactionTransform.position.x, interactionTransform.position.z));
			if (distance < radius || distance < 0.2f)
			{
				Interact();
				hasFocused = true;
			}
		}
	}

	/// <summary>
	/// Interactable is focused by the player 
	/// </summary>
	public void OnFocused(Transform playerTransform)
	{
		isFocus = true;
		player = playerTransform;
		hasFocused = false;
	}

	/// <summary>
	/// Interactable is no longer focused by the player
	/// </summary>
	public void OnDefocused()
	{
		isFocus = false;
		player = null;
		hasFocused = false;
	}

	private void OnDrawGizmosSelected()
	{
		// Draw Interactionradius
		if(interactionTransform)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(interactionTransform.position, radius);
		}
	}
}
