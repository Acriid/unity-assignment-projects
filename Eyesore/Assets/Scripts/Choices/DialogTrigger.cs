using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private int _dialogIndex = 0;
    [SerializeField] private List<GameObject> _dialogDisableTriggers;
    [SerializeField] private List<GameObject> _dialogEnableTriggers;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            DialogManager.Instance.ShowDialog(_dialogIndex);
            gameObject.SetActive(false);

        }
    }

    public void TriggerDialog()
    {
        DialogManager.Instance.ShowDialog(_dialogIndex);
        gameObject.SetActive(false);
    }
}
