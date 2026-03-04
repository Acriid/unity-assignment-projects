using UnityEngine;

public class GameOverSystem : MonoBehaviour
{
    public static GameOverSystem Instance {get; private set;}
    private int _nextDialogSave;
    public void SaveData(ref DialogSaveData data)
    {
        data.StartDialogID = _nextDialogSave;
    }
}
