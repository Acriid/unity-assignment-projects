using Unity.VisualScripting;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] protected string interactionPrompt = "Interact";
    [SerializeField] protected bool canInteract = true;

    // Virtual method to be overridden by child classes
    public virtual void OnInteract(GameObject player)
    {
        Debug.Log($"Interacted with {gameObject.name}");
    }

    // Get the prompt text for UI
    public virtual string GetPrompt()
    {
        return interactionPrompt;
    }

    // Check if this interaction is currently available
    public virtual bool CanInteract()
    {
        return canInteract;
    }

    // Enable/disable interaction
    public void SetInteractable(bool state)
    {
        canInteract = state;
    }

}
