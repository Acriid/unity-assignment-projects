using UnityEngine;

public class KeyTrigger : MonoBehaviour
{

    public GameObject TriggerToChange;
    public int DialogID1;
    public void ChangeDialog()
    {
        TriggerToChange.TryGetComponent<DialogTrigger>(out DialogTrigger trigger);
        trigger.ChangeIndex(DialogID1);
        trigger.ClearEnableTriggers();
    }
}
