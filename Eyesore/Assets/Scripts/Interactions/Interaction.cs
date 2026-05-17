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
    [SerializeField] protected DialogSO _dialogSO = null;
    [SerializeField] protected bool _repeatDialog = false;
    protected bool _shownDialog = false;

    public virtual void OnInteract(GameObject player)
    {
        Debug.Log($"Interacted with {gameObject.name}");
    }


    public virtual string GetPrompt()
    {
        return interactionPrompt;
    }

   
    public virtual bool CanInteract()
    {
        return canInteract;
    }


    public void SetInteractable(bool state)
    {
        canInteract = state;
    }

    public virtual void EnableObjects(bool newValue)
    {
        if(_enableObjects.Count == 0) return;
        foreach(GameObject enableObject in _enableObjects)
        {
            if(enableObject != null)
            enableObject.SetActive(newValue);
        }
    }
    public virtual void DisableObjects(bool newValue)
    {
        if(_disableObjects.Count == 0) return;
        foreach(GameObject disableObject in _disableObjects)
        {
            if(disableObject != null)
            disableObject.SetActive(newValue);
        }
    }
    public virtual void ChangeInteractionPrompt(string newPrompt)
    {
        interactionPrompt = newPrompt;
    }
    public virtual void ShowDialog()
    {
        if ((_repeatDialog && !_shownDialog) || (!_repeatDialog && !_shownDialog)) return;
        if(DialogManager.Instance != null && _dialogSO != null)
        DialogManager.Instance.ShowDialog(_dialogSO);
    }
}
