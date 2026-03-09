using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;
using System;
using UnityEngine.SceneManagement;


public class EndDialogManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _endText;
    [SerializeField] private TMP_Text _narratorGoodText;
    [SerializeField] private TMP_Text _narratorBadText;
    [SerializeField] private List<DialogSO> _dialogs;
    private bool _followedNarrator = true;
    private bool _endedDialog = false;
    private event Action _onDialogFinish;
    private int _counter = 0;
    void OnEnable()
    {
        Time.timeScale = 0;
        InitializeDialog();
    }
    void OnDisable()
    {
        Time.timeScale = 1;
    }
    private void InitializeDialog()
    {
        _endText.text = "";
        _narratorGoodText.text = "";
        _narratorBadText.text = "";

        

        if(_followedNarrator)
        {
            StartCoroutine(TypeDialog("You Escaped...",_endText,4f,false));
            _onDialogFinish += GoBack;
        }
        else
        {
            StartCoroutine(TypeDialog("You did not escape...",_endText,5f,false));
            _onDialogFinish += GoneDeeper;
        }
    }
    private IEnumerator TypeDialog(string textToShow, TMP_Text _tmpText, float dialogTime,bool deleteDialog)
    {
        float letterTime = dialogTime/textToShow.Length;
        for(int i = 0; i < textToShow.Length ; i++)
        {
            _tmpText.text += textToShow[i];
            yield return new WaitForSecondsRealtime(letterTime);
        }
        yield return new WaitForSecondsRealtime(dialogTime);


        if(deleteDialog)
        {
            StartCoroutine(DeleteDialog(0.5f,_tmpText));   
        }
        else
        {
            _onDialogFinish?.Invoke();
        }
    }
    private IEnumerator DeleteDialog(float deleteTime, TMP_Text _tmpText)
    {
        StringBuilder s = new(_tmpText.text);
        float letterTime = deleteTime/s.Length;
        while(s.Length > 0)
        {
            s.Length--;
            _tmpText.text = s.ToString();
            yield return new WaitForSecondsRealtime(letterTime);
        }
        _onDialogFinish?.Invoke();
    }


    #region GoBack
    private void GoBack()
    {
        _onDialogFinish -= GoBack;
        StartCoroutine(TypeDialog(_dialogs[0].DialogText,_narratorBadText,3f,true));
        _onDialogFinish += GoBack2; 
    }

    private void GoBack2()
    {
        _onDialogFinish -= GoBack2;
        StartCoroutine(TypeDialog(_dialogs[1].DialogText,_narratorBadText,3f,true));
        _onDialogFinish += GoBack3; 
    }
    private void GoBack3()
    {
        _onDialogFinish -= GoBack3;
        StartCoroutine(TypeDialog(_dialogs[2].DialogText,_narratorBadText,3f,true));
        _onDialogFinish += GoBack4; 
    }
    private void GoBack4()
    {
        _onDialogFinish -= GoBack4;
        StartCoroutine(TypeDialog(_dialogs[3].DialogText,_narratorBadText,3f,true));
        StartCoroutine(BackToMenu());
    }
    #endregion

    #region GoneDeeper
    private void GoneDeeper()
    {
        _onDialogFinish -= GoneDeeper;
        StartCoroutine(TypeDialog(_dialogs[4].DialogText,_narratorGoodText,3f,true));
        _onDialogFinish += GoneDeeper2;
    }
    private void GoneDeeper2()
    {
        _onDialogFinish -= GoneDeeper2;
        StartCoroutine(TypeDialog(_dialogs[5].DialogText,_narratorGoodText,3f,true));
        _onDialogFinish += GoneDeeper3;
    }
    private void GoneDeeper3()
    {
        _onDialogFinish -= GoneDeeper3;
        StartCoroutine(TypeDialog(_dialogs[6].DialogText,_narratorGoodText,3f,true));
        StartCoroutine(BackToMenu());
    }
    #endregion

    public void ChangeFollow(bool newValue)
    {
        _followedNarrator = newValue;
    }
    private IEnumerator BackToMenu()
    {
        yield return new WaitForSecondsRealtime(15f);
        SceneManager.LoadScene("Main Menu");
    }
}
