using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] protected string interactionPrompt = "Interact";
    [SerializeField] protected bool canInteract = true;
    [SerializeField] protected List<GameObject> _enableObjects;
    [SerializeField] protected List<GameObject> _disableObjects;
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

    public virtual void EnableObjects(bool newValue)
    {
        if(_enableObjects.Count == 0) return;
        foreach(GameObject enableObject in _enableObjects)
        {
            enableObject.SetActive(newValue);
        }
    }
    public virtual void DisableObjects(bool newValue)
    {
        if(_disableObjects.Count == 0) return;
        foreach(GameObject disableObject in _disableObjects)
        {
            disableObject.SetActive(newValue);
        }
    }
}
