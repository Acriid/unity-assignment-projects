using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/DialogSO")]
public class DialogSO : ScriptableObject
{
    public string DialogText;
    public string DialogDescription;
    public int DialogFontSize;
    public Color DialogColour;
    public Font DialogFont;
    public Vector2 DialogPosition;
    public int DialogDuration;
    public bool InstantDialog;
    public DialogSO NextDialog;

    public void GetNextDialogList(ref List<DialogSO> dialogList)
    {
        dialogList.Add(this);
        if(NextDialog != null)
        {
            NextDialog.GetNextDialogList(ref dialogList);
        }
    }
}
