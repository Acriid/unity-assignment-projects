using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ActivationTrigger : MonoBehaviour
{
    [SerializeField] protected string _collisionTag;
    [SerializeField] protected List<GameObject> _enableGameObject = new();
    [SerializeField] protected List<GameObject> _disableGameObject = new();
    [SerializeField] protected bool _disableSelf = false;
    [SerializeField] protected GameObject _uiMessage;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_collisionTag))
        {
            if(_enableGameObject.Count != 0) EnableGameObjects();
            if(_disableGameObject.Count != 0) DisableGameObjects();
            if(_disableSelf) this.gameObject.SetActive(false);
        }
    }

    protected virtual void EnableGameObjects()
    {
        foreach(GameObject enableObject in _enableGameObject)
        {
            enableObject.SetActive(true);
        }
    }
    protected virtual void DisableGameObjects()
    {
        foreach(GameObject disableObject in _disableGameObject)
        {
            disableObject.SetActive(false);
        }
    }

    public virtual void ShowUI(string uiText)
    {
        if(_uiMessage == null) return;
        _uiMessage.SetActive(true);
        _uiMessage.GetComponent<TMP_Text>().text = uiText;
    }

    public virtual void HideUI()
    {
        if(_uiMessage == null) return;
        _uiMessage.SetActive(false);
    }
    
    public virtual void RemoveFromEnableObjects(GameObject objectToRemove)
    {
        _enableGameObject.Remove(objectToRemove);
    }
    public virtual void AddToEnableObjects(GameObject objectToAdd)
    {
        _enableGameObject.Add(objectToAdd);
    }
    public virtual void RemoveFromDisableObjects(GameObject objectToRemove)
    {
        _disableGameObject.Remove(objectToRemove);
    }
    public virtual void AddToDisableObjects(GameObject objectToAdd)
    {
        _disableGameObject.Add(objectToAdd);
    }
}
