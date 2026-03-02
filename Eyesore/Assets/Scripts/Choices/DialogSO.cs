using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/DialogSO")]
public class DialogSO : ScriptableObject
{
    public string DialogText;
    public int DialogFontSize;
    public Color DialogColour;
    public Font DialogFont;
    public Vector2 DialogPosition;
    public int DialogID;
    public int NextDialogID;
    public bool PauseDialog;
    public int DialogDuration;
}
