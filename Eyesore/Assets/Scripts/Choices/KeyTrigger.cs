using UnityEngine;

public class KeyTrigger : MonoBehaviour
{

    public GameObject TriggerToChange;
    public DialogSO DialogSO;
    public void ChangeDialog()
    {
        TriggerToChange.TryGetComponent<DialogTrigger>(out DialogTrigger trigger);
        trigger.ChangeDialog(DialogSO);
        trigger.ClearEnableTriggers();
    }
}
