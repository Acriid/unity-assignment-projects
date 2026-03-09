using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private int _dialogIndex = 0;
    [SerializeField] private List<GameObject> _dialogDisableTriggers;
    [SerializeField] private List<GameObject> _dialogEnableTriggers;
    [SerializeField] private bool _removeItemAfterTrigger = true;
    [SerializeField] private bool _useTriggerEnter = true;
    private bool _triggered = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_useTriggerEnter) return;
        if(_triggered) return;
        if(collision.CompareTag("Player"))
        {
            DialogManager.Instance.ShowDialog(_dialogIndex);
            if(_removeItemAfterTrigger)
                gameObject.SetActive(false);
            _triggered = true;

            ChangeTriggers();
        }
        
    }

    public void TriggerDialog()
    {
        if(_triggered) return;
        DialogManager.Instance.ShowDialog(_dialogIndex);
        if(_removeItemAfterTrigger)
            gameObject.SetActive(false);

        _triggered = true;

        ChangeTriggers();
    }

    private void ChangeTriggers()
    {
        EnableTriggers();
        DisableTriggers();
    }
    private void EnableTriggers()
    {
        if(!(_dialogEnableTriggers.Count > 0))
            return;

        foreach(GameObject dialogTrigger in _dialogEnableTriggers)
        {
            dialogTrigger.SetActive(true);
        }
    }
    private void DisableTriggers()
    {
        if(!(_dialogDisableTriggers.Count > 0))
            return;
        
        foreach(GameObject dialogTrigger in _dialogDisableTriggers)
        {
            dialogTrigger.SetActive(false);
        }
    }

    public void ChangeIndex(int newValue)
    {
        _dialogIndex = newValue;
    }
    public void ClearEnableTriggers()
    {
        _dialogEnableTriggers.Clear();
    }
}
