using System;
using System.Text;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class DialogObject : MonoBehaviour
{
//Will be used to hold the dialog within the worlds
    public DialogSO DialogSO;
    [SerializeField] private TMP_Text _tmpText;
    public bool DialogPaused = false;
    public event Action<int> OnTextFinished;
    void Awake()
    {
        


        if(_tmpText == null)
            _tmpText = GetComponent<TMP_Text>();
    }
    public void ShowDialog()
    {
        _tmpText.text = "";
        this.transform.position = DialogSO.DialogPosition;

        _tmpText.fontSize = DialogSO.DialogFontSize;
        _tmpText.color = DialogSO.DialogColour;

        StartCoroutine(TypeDialog(DialogSO.DialogText,DialogSO.DialogDuration));
        

    }
    private IEnumerator TypeDialog(string textToShow, float dialogDuration)
    {
        if(DialogSO.InstantDialog)
        {
            _tmpText.text = DialogSO.DialogText;
        }
        else
        {
            float letterTime = 1f/textToShow.Length;
            for(int i = 0; i < textToShow.Length ; i++)
            {
                _tmpText.text += textToShow[i];
                yield return new WaitForSeconds(letterTime);
            }
        }
        yield return new WaitForSeconds(dialogDuration);

        StartCoroutine(DeleteDialog(0.15f));
    }
    private IEnumerator DeleteDialog(float deleteTime)
    {
        StringBuilder s = new(_tmpText.text);
        float letterTime = deleteTime/s.Length;
        while(s.Length > 0)
        {
            s.Length--;
            _tmpText.text = s.ToString();
            yield return new WaitForSeconds(letterTime);
        }
        OnTextFinished?.Invoke(DialogSO.DialogID);
    }

}
