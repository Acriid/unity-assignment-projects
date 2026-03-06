using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;


public class EndDialogManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _endText;
    [SerializeField] private TMP_Text _narratorGoodText;
    [SerializeField] private TMP_Text _narratorBadText;
    [SerializeField] private List<DialogSO> _dialogs;
    private bool _followedNarrator = true;
    private bool _endedDialog = false;
    void OnEnable()
    {
        InitializeDialog();
    }
    private void InitializeDialog()
    {
        _endText.text = "";
        _narratorGoodText.text = "";
        _narratorBadText.text = "";

        StartCoroutine(TypeDialog("You Escaped...",_endText,1f));
    }
    private IEnumerator TypeDialog(string textToShow, TMP_Text _tmpText, float dialogTime)
    {
        float letterTime = dialogTime/textToShow.Length;
        for(int i = 0; i < textToShow.Length ; i++)
        {
            _tmpText.text += textToShow[i];
            yield return new WaitForSecondsRealtime(letterTime);
        }

        if(!_endedDialog)
        {
            _endedDialog = true;
            if(_followedNarrator)
            {
                GoBack();
            }
        }
    }
    private IEnumerator DeleteDialog(float deleteTime, TMP_Text _tmpText,float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        StringBuilder s = new(_tmpText.text);
        float letterTime = deleteTime/s.Length;
        while(s.Length > 0)
        {
            s.Length--;
            _tmpText.text = s.ToString();
            yield return new WaitForSecondsRealtime(letterTime);
        }
    }
    private void GoBack()
    {
            StartCoroutine(TypeDialog(_dialogs[0].DialogText,_narratorBadText,3f));
            StartCoroutine(DeleteDialog(0.5f,_narratorBadText,6f));
    }
    
}
